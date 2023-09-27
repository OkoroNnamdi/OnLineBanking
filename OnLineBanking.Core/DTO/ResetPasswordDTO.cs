using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Domain.DTO
{
    public  class ResetPasswordDTO

    {
        [Required (ErrorMessage ="Email address is required")]
        public string Email { get; set; }
    }
}
