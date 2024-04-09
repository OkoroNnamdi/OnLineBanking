using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.DTO
{
   public  class CreateAccountDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } =DateTime .UtcNow;
        public string ConfirmPassword { get; set; }
        public string Password { get; set; }
    }
}
