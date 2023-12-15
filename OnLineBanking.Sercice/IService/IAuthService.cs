using AspNetCoreHero.Results;
using OnLineBanking.Core.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Service.IService
{
    public interface IAuthService
    {
        Task<Result<LoginUserDTO>> Login(LoginDTO model);
        Task<Result<string>> Register(RegisterUserDTO user);
        Task<Result<string>> RefreshToken();
        public Task<Result<string>> ChangePassword(ChangePasswordDTO changePasswordDTO);
        public Task<Result<string>> ResetPassword(UpdatePasswordDTO resetPasswordDTO);
        public Task<Result<string>> ForgottenPassword(ResetPasswordDTO model);
        Task<Result<string>> Confirmemail(string email, string token);
        Task Signout();
    }
}
