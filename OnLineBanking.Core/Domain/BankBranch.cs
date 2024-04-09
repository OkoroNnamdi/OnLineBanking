using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Domain
{
    public  class BankBranch:BaseEntity 
    {
        public string BranchEmail { get; set; }
        public string BranchPhone { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        public string BranchState { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BranchName { get; set; }
        public string BankBranchCode { get; set; }
        public string Location { get;set; }
        public string ManagerId { get; set; }
        public Manager Manager { get; set; }
        public ICollection<Customer> Customer { get; set; }
        public ICollection <Transaction > Transaction { get; set; }
        public ICollection<Account > Account { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        
    }
}
