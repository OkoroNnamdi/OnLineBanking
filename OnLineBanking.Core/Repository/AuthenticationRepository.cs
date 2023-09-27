using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.Domain.DTO;
using OnLineBanking.Core.IRepository;
using OnLineBanking.Core.IServices;
using OnLineBanking.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServices _token;
        private readonly ITokenDetails _tokenDetails;
        private readonly IHttpContextAccessor _httpContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        public AuthenticationRepository()
        {
            
        }
        public Task<Response<string>> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> ForgottenPassword(ResetPasswordDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<Response<LoginUserDTO>> Login(LoginDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> RefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(RegisterUserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<object> ResetPassword(UpdatePasswordDTO resetPasswordDTO)
        {
            throw new NotImplementedException();
        }

        public Task Signout()
        {
            throw new NotImplementedException();
        }
    }
}
