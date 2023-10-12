using AspNetCoreHero.Results;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineBanking.Application;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.Domain.DTO;
using OnLineBanking.Core.IRepository;
using OnLineBanking.Core.IServices;
using OnLineBanking.Core.Services;
using OnLineBanking.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class AuthenticationRepository :IAuthenticationRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServices _token;
        private readonly ITokenDetails _tokenDetails;
        private readonly IHttpContextAccessor _httpContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly BankDbContext _context;
      //  private readonly BankDbContext _context;
        public AuthenticationRepository(UserManager<AppUser> userManager, 
            ITokenServices token, ITokenDetails tokenDetails, 
            IHttpContextAccessor httpContext,RoleManager<IdentityRole>roleManager,
            IMapper mapper,IEmailService emailService,BankDbContext context)
        {
            _userManager = userManager;
            _token = token;
            _tokenDetails = tokenDetails;
            _httpContext = httpContext;
            _roleManager = roleManager;
            _mapper = mapper;
            _emailService = emailService;
            _context = context;
         
        }
        public string GetId() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        public async  Task<Result<string>> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var user = await _userManager.FindByIdAsync(GetId());
            if (user == null) return await Result<string>.FailAsync("you must be logged in to change password");
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);
            if (!result.Succeeded) return await Result<string>.FailAsync("Unable to change password: password should contain a Capital, number, character and minimum length of 8");

            return await Result<string>.SuccessAsync("password changed successfully");
        }

        public async  Task<Result<string>> ForgottenPassword(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return await Result<string>.FailAsync("The Email Provided is not associated with a user account");
            }

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var emailMsg = new EmailMessage(new string[] { user.Email }, "Reset your password", $"Please Follow the Link to reset your Password: https://localhost:7031/api/Auth/Reset-Update-Password?token={resetPasswordToken}");
            await _emailService.SendEmailAsync(emailMsg);
            return await Result<string>.SuccessAsync("A password reset Link has been sent to your email address");
        }

        public async  Task<Result<LoginUserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var userModel = new UserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = userRoles.FirstOrDefault()
                };

                var refreshToken = _token.SetRefreshToken();

                await SaveRefreshToken(user, refreshToken);

                var loginData = new LoginUserDTO
                {
                    Firstname = user.FirstName,
                    Lastname = user.LastName,
                    Email = user.UserName,
                    Roles = userRoles,
                    Token = _token.CreateToken(userModel),
                    RefreshToken = refreshToken.Refreshtoken
                };

                return new Result<LoginUserDTO>
                {
                    Succeeded = true,
                    Data = loginData,
                    Message = "Logged in successfully"
                };
            }
            else
            {
                return await Result<LoginUserDTO>.FailAsync("Wrong Credential");
            }

        }
        private async Task SaveRefreshToken(AppUser user, RefreshToken refreshToken)
        {
            user.RefreshToken = refreshToken.Refreshtoken;
            user.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();
        }
        public Task<Result<string>> RefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> Register(RegisterUserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> ResetPassword(UpdatePasswordDTO resetPasswordDTO)
        {
            throw new NotImplementedException();
        }

        public Task Signout()
        {
            throw new NotImplementedException();
        }
    }
}
