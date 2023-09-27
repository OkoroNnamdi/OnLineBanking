using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.Domain.DTO;

namespace OnLineBanking.Core.IRepository
{
    public interface IAuthenticationRepository
    {
        Task<Response<LoginUserDTO>> Login(LoginDTO model);
        Task<bool> Register(RegisterUserDTO user);
        Task<Response<string>> RefreshToken();
        public Task<Response<string>> ChangePassword(ChangePasswordDTO changePasswordDTO);
        public Task<object> ResetPassword(UpdatePasswordDTO resetPasswordDTO);
        public Task<Response<string>> ForgottenPassword(ResetPasswordDTO model);
        Task Signout();
    }
}
