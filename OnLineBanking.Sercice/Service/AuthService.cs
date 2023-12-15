using AspNetCoreHero.Results;
using OnLineBanking.Core.Domain.DTO;
using OnLineBanking.Core.IRepository;
using OnLineBanking.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Service.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository=authenticationRepository;
        }
        public async Task<Result<string>> ChangePassword(ChangePasswordDTO model)
        {
            try
            {
                if (model.ConfirmNewPassword != model.NewPassword)
                     return await Result<string>.FailAsync("Password does not match");
                var response = await _authenticationRepository.ChangePassword(model);
                if (response.Succeeded)
                    return await Result<string>.SuccessAsync("Password change was sucessful");
                return await Result<string>.FailAsync("Password change failed");
            }
            catch (Exception ex)
            {

                return await Result<string>.FailAsync("Error Occurs while changing the password" + ex.Message);
            }
        }
        public async  Task<Result<string>> Confirmemail(string email, string token)
        {
            try
            {
                var response = await _authenticationRepository.Confirmemail(email, token);
                if (response.Succeeded)
                {
                    return await Result<string>.SuccessAsync("email verified successfully");
                }
                return await Result<string>.FailAsync("email not verified");
            }
            catch (Exception ex)
            {
                return await Result<string>.FailAsync("an error occured while verifying email"+ex.Message);

            }
        }
        public async  Task<Result<string>> ForgottenPassword(ResetPasswordDTO model)
        {
            var response = await _authenticationRepository.ForgottenPassword(model);
            return response;
            //if (response.Succeeded)
            //    return await Result<string>.SuccessAsync("Password changed sucessful");
            //return await Result<string>.FailAsync("Pass")
            
        }
        public async Task<Result<LoginUserDTO>> Login(LoginDTO model)
        {
            try
            {
                var result = await _authenticationRepository.Login(model);
                if (result.Succeeded)
                {
                    return new Result<LoginUserDTO>
                    {
                        Succeeded = true,
                        Data = result.Data ,
                        Message = "Logged in successfully"
                    };

                }
                else
                {
                    return new Result<LoginUserDTO>
                    {
                        Succeeded = false,
                        Data = result.Data,
                        Message = "Logged in failed"
                    };

                }
            }
            catch (Exception ex)
            {
                return new Result<LoginUserDTO>
                {
                    Succeeded = false,
                    Message = "error occur while trying to Logged in failed" + ex.Message
                };

            }
            
            
        }

        public async Task<Result<string>> RefreshToken()
        {
           var response = await  _authenticationRepository.RefreshToken();
            return response;
        }

        public async  Task<Result<string>> Register(RegisterUserDTO user)
        {
            try
            {
                var result = await _authenticationRepository.Register(user);
                //var response = new Result<string>();
                if (result.Succeeded)
                {
                    return new Result<string> { Succeeded = true, Data = result.Data, Message = result.Message };
                }
                else
                {
                    return await Result<string>.FailAsync("Failed to register, please change check the email, username and password.");
                }
            }
            catch (Exception ex)
            {

                return await Result<string>.FailAsync("error occurs while registering "+ex.Message );

            }
        }

        public async  Task<Result<string>> ResetPassword(UpdatePasswordDTO resetPasswordDTO)
        {
            try
            {
                var response = _authenticationRepository.ResetPassword(resetPasswordDTO);
                return await response;
            }
            catch (Exception ex)
            {

                return await Result<string>.FailAsync("error occurs while resetting the password" + ex.Message );
            }
        }

        public async Task Signout()
        {
           await   _authenticationRepository.Signout();
        }
    }
}
