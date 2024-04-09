using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.DTO
{
    public  class AddReviewsDTO
    {
        [DataType(DataType.Text)]
        public string Comment { get; set; }
        public string Bankid { get; set; }
        public string CustomerId { get; set; }
        public BankBranch bank { get; set; }
    }
}
