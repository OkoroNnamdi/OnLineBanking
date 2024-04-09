using Org.BouncyCastle.Crypto.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.DTO
{
     public class TransferDto
    {
      public   string Fromaccount;
      public  string ToAccount;
      public  double Amount;
      public string TransactionPin;
    }
}
