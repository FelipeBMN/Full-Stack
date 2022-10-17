using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetWebApi.Application.Contratos;
using dotnetWebApi.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetWebApi.API.Controllers
{
    [Authorize] // Configurado no startup.cs
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly ITokenService tokenService;

        public AccountController(IAccountService accountService,
                                ITokenService tokenService)
        {
            this.accountService = accountService;
            this.tokenService = tokenService;
        }

        [HttpGet("GetUser/{userName}")]
        public async Task<IActionResult> GetUser(string userName)
        {
            try
            {
                var user = await this.accountService.GetUserByUserNameAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao realizar GetUser. Erro: {ex.Message}");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (await this.accountService.UserExists(userDto.UserName))
                    return BadRequest("Usuário já existe");

                var creatUser = await this.accountService.CreatingAccountAsync(userDto);

                if (creatUser != null)
                    return Ok(creatUser);

                return BadRequest("Usuário não cadastrado, tente novamente mais tarde!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao realizar GetUser. Erro: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await this.accountService.GetUserByUserNameAsync(userLogin.UserName);
                if(user == null) return Unauthorized("Login invalido");

                var result = await this.accountService.CheckUserPasswordAsync(user, userLogin.Password);

                if(!result.Succeeded) return Unauthorized("Erro ao fazer login");

                return Ok(
                    new {
                        userName = user.UserName,
                        firstName = user.FirstName,
                        token = this.tokenService.CreateToken(user).Result    
                    }
                );

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao realizar GetUser. Erro: {ex.Message}");
            }
        }

    }
}