using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Domain
{
    public  class Rating:BaseEntity
    {
        public int Ratings { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string BankBranchId { get; set; }
        public BankBranch BankBranch { get; set; }
    }
}
