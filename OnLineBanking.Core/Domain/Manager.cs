using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Domain
{
    public  class Manager:BaseEntity
    {
        public string BusinessEmail { get; set; }
        public string BusinessPhone { get; set; }
        public String  Address { get; set; }
        public string State { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Customer > Customers{ get; set; }
        public string BankBranchId { get; set; }
        public BankBranch BankBranch { get; set; }

    }
}
