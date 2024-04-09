using AspNetCoreHero.Results;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly OnlineBankDBContext _context;
      //  private readonly BankDbContext _context;
        public AuthenticationRepository(UserManager<AppUser> userManager, 
            ITokenServices token, ITokenDetails tokenDetails, 
            IHttpContextAccessor httpContext,RoleManager<IdentityRole>roleManager,
            IMapper mapper,IEmailService emailService, OnlineBankDBContext context)
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
        public AuthenticationRepository(OnlineBankDBContext context)
        {
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

        public async Task<Result<LoginUserDTO>> Login(LoginDTO model)
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
        public async  Task<Result<string>> RefreshToken()
        {
            var currentToken = _httpContext.HttpContext.Request.Cookies["refresh-token"];
            var user = await _userManager.FindByIdAsync(_tokenDetails.GetId());

            var response = new Result<string>();
            response.Succeeded = false;
            //return await Result<string>.FailAsync();
            if (user == null || user.RefreshToken != currentToken)
            {
                response.Data = "Invalid refresh token";
            }
            else if (user.RefreshTokenExpiryTime < DateTime.Now)
            {
                response.Message = "Token Expired";
            }
            else
            {
                var UserModel = new UserModel
                {
                    Id = _tokenDetails.GetId(),
                    Email = _tokenDetails.GetUserEmail(),
                    Role = _tokenDetails.GetRoles()
                };

                response.Succeeded = true;
                response.Data = _token.CreateToken(UserModel);
                response.Message = "Successful refreshed token";
                var refreshToken = _token.SetRefreshToken();
                await SaveRefreshToken(user, refreshToken);
            }
            return response;
        }

        public async  Task<Result<string>> Register(RegisterUserDTO user)
        {
            try
            {
                // checks if a user with the specified email already exists in the system
                var checkUser = await _userManager.FindByEmailAsync(user.Email);
                if (checkUser != null)
                {
                    return await Result<string>.FailAsync("user already exist");
                }

                var names = user.FullName.Split(" ");


                var newUser = new AppUser()
                {
                    Email = user.Email,
                    FirstName = names[0],
                    LastName = names.Length > 1 ? names[1] : string.Empty,
                    UserName = user.Email,
                    PhoneNumber = user.Phone
                };

                //var newUser = _mapper.Map<AppUser>(user);


                var roles = await _roleManager.Roles.ToListAsync();
                //If there are no roles, it creates a new role with the name "User".
                if (roles.Count == 0) await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
                //create a new user in the system
                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (!result.Succeeded)
                {
                    return await Result<string>.FailAsync("user could not be created");
                }
                //If the user creation is successful, the function adds the new user to the "User"  role
                await _userManager.AddToRoleAsync(newUser, "Customer");
                //generates an email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                return new Result<string> { Succeeded = true, Data = token, Message = $"click the link sent to {user.Email} to confirm your email" };
            }
            catch (Exception ex)
            {

                return await Result<string>.FailAsync(ex.Message + "user could not be created, an error occured");
            }

        }

        public async  Task<Result<string>> ResetPassword(UpdatePasswordDTO model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return await Result<string>.FailAsync("an error occured while comfirming email");

                }
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

                if (!result.Succeeded)
                {
                    // return "The Provided Reset Token is Invalid or Has expired";
                    return await Result<string>.FailAsync("an error occured while comfirming email");
                }

                return await Result<string>.SuccessAsync("Password Reset Successfully");
            }
            catch (Exception ex)
            {

                return await Result<string>.FailAsync(ex.Message + "user could not be reset, an error occured");
            }
        }
        public async  Task<Result<string>> Confirmemail(string email, string token)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return await Result<string>.FailAsync("user does not exist");
                }
                var res = await _userManager.ConfirmEmailAsync(user, token);
                if (!res.Succeeded)
                {
                    return await Result<string>.FailAsync ("email has been confirmation failed");
                }
                return await Result<string>.SuccessAsync("email has been confirmed");
            }
            catch (Exception ex)
            {

                return await Result<string>.FailAsync(ex.Message + "email could not be confirmed, an error occured");
            }
        }
        public async  Task Signout()
        {
            var headers = _httpContext.HttpContext.Request.Headers;
            headers.Remove("Authorisation");
        }
    }
}
