using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Domain.DTO
{
    public  class LoginUserDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public IList<string> Roles { get; set; }
        public string Token { get; set; }
         public string RefreshToken { get; set; }
        public string Email { get; set; }
    }
}
