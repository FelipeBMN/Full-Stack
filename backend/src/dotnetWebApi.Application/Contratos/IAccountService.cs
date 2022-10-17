using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetWebApi.Application.Dtos;
using Microsoft.AspNetCore.Identity;

namespace dotnetWebApi.Application.Contratos
{
    public interface IAccountService
    {
        Task<bool> UserExists(string userName);
        Task<UserUpdateDto> GetUserByUserNameAsync(string userName);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDtop, string password);
        Task<UserDto> CreatingAccountAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateAccount (UserUpdateDto userUpdateDto);
    }
}