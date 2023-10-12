using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.Domain.DTO;
using AspNetCoreHero.Results;

namespace OnLineBanking.Core.IRepository
{
    public interface IAuthenticationRepository
    {
         public Task<Result<LoginUserDTO>> Login(LoginDTO model);
         public  Task<Result <string> >Register(RegisterUserDTO user);
         public  Task<Result<string>> RefreshToken();
        public Task<Result<string>> ChangePassword(ChangePasswordDTO changePasswordDTO);
        public Task<Result<string >> ResetPassword(UpdatePasswordDTO resetPasswordDTO);
        public Task<Result<string>> ForgottenPassword(ResetPasswordDTO model);
        Task Signout();
    }
}
