using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.DTO
{
    public  class GetReviewsDTO
    {
        public string Comment { get; set; }
        public string BankId { get; set; }
        public string CustomerId { get; set; }
        public BankBranch Bank { get; set; }
        public Customer Customer { get; set; }
    }
}
