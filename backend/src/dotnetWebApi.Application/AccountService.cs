using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetWebApi.Application.Contratos;
using dotnetWebApi.Application.Dtos;
using dotnetWebApi.Domain.Identity;
using dotnetWebApi.Persistence.Contratos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace dotnetWebApi.Application
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;
        private readonly IUserPersist userPersist;
        public AccountService(
                                UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                IMapper mapper,
                                IUserPersist userPersist
                            )
        {
            this.userManager = userManager;
            this.userPersist = userPersist;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await this.userManager.Users.SingleOrDefaultAsync(user => user.UserName.ToLower() == userUpdateDto.UserName.ToLower());
                // caso queira bloquear o usuário colocar true;
                return await this.signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Error:{ex.Message}");
            }
        }

        public async Task<UserDto> CreatingAccountAsync(UserDto userDto)
        {
            try
            {
                var user = this.mapper.Map<User>(userDto);
                var result = await this.userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    var userToReturn = this.mapper.Map<UserDto>(user);
                    return userToReturn;
                }

                return null;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar criar conta. Error:{ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
            try
            {
                var user = await this.userPersist.GetUserByUserNameAsync(userName);

                if (user == null) return null;

                var useUpdateDto = this.mapper.Map<UserUpdateDto>(user);

                return useUpdateDto;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar usuário. Error:{ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await this.userPersist
                .GetUserByUserNameAsync(userUpdateDto.UserName);
                if(user == null) return null;

                this.mapper.Map(userUpdateDto, user);
                
                // Inicialmente deve ser realizado o reset  do password
                var token = await this.userManager.GeneratePasswordResetTokenAsync(user); // gerando um token e resetando este token
                var result = await this.userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                this.userPersist.Update<User>(user);

                if(await this.userPersist.SaveChangesAsync()){
                    var userRetorno = await this.userPersist.GetUserByUserNameAsync(user.UserName);

                    return this.mapper.Map<UserUpdateDto>(userRetorno);
                }

                return null;


            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar Atualizar conta. Error:{ex.Message}");
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            try
            {
                // AnyAsync - EntityFrameworkCore
                return await this.userManager.Users.AnyAsync(user => user.UserName == userName.ToLower());
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Error:{ex.Message}");
            }
        }
    }
}