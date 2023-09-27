using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Domain.DTO
{
    public  class UpdatePasswordDTO
    {
        [Required(ErrorMessage = "UserName is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "NewPassword is Required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Token is Required")]
        public string Token { get; set; }
    }
}
