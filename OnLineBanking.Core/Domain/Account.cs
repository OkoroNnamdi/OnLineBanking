using OnLineBanking.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Domain
{
    public  class Account:BaseEntity 
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string  AccountName { get;set; } = string.Empty;
        public double Amount { get; set; } = 0;
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public string Account_Number { get; set; }= string.Empty;
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public ICollection <Transaction > Transactions { get; set; }
        public ICollection <Customer> Customers { get; set; }
        public ICollection <Address >Addresses { get; set; }

        public Account()
        {
            Account_Number = GenerateAccountNumber.AccountNumber();
            AccountName =$"{FirstName } {LastName }";
        }
    }
}
