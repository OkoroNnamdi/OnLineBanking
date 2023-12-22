using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Domain
{
    public  class Customer:BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public string State { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string BranchId { get; set; }
        public BankBranch BankBranch { get; set; }
        public ICollection<Account> Accounts { get ; set; } 
        public ICollection <Transaction> Transactions { get ; set; }
        public ICollection<Address> Address { get;}
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
