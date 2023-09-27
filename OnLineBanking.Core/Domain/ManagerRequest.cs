using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Domain
{
    public  class ManagerRequest:BaseEntity
    {
        public string ManagerName { get; set; }
        public string BankBranchName { get; set; }
        public string HotelAddress { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool ConfirmationFlag { get; set; } = false;
        public DateTime ExpiresAt { get; set; } = DateTime.Now.AddDays(30);
    }
}
