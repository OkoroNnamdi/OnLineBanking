using OnLineBanking.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.DTO
{
   public  class TransactionDto
    {
        public string TransactionUniqueReference { get; set; }
        [Required]
        public TranStatus TransactionStatus { get; set; }
        [Required]
        public Transtype TransactionType { get; set; }
        public bool IsSuccessful => TransactionStatus.Equals(TranStatus.Success);
        [Required]
        public string TransactionSourceAccount { get; set; }
        [Required]
        public string TransactionDestinationAccount { get; set; }
        public string TransactionParticulars { get; set; }
        [Required]
        public decimal TransactionAmount { get; set; }

    }
}
