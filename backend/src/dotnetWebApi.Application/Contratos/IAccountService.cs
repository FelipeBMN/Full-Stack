using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetWebApi.Application.Contratos
{
    public interface IAccountService
    {
        Task<bool> UserExists(string userName);
        Task<UserUpdateDto> GetUserByUserNameAsync(string userName);
        Task<SingInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDtop, string password);
        Task<UserDto> CreatingAccountAsync(UnobservedTaskExceptionEventArgs userDto);
        Task<UserUpdateDto> UpdateAccount (UserUpdateDto userUpdateDto);
    }
}