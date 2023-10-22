using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Utility
{
    public class TokenDetails : ITokenDetails
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenDetails(IHttpContextAccessor contextAccessor) 
        {
            _contextAccessor = contextAccessor;
        }
        public string GetId()
        {
          return  _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string GetRoles()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        public string GetUserEmail()
        {
          return  _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        }
    }
}
