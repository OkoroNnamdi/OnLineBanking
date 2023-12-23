using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace OnLineBanking.Core.Utility
{
    public static  class GenerateAccountNumber
    {
        public static string  AccountNumber()
        {
            Random random = new Random();
            var accountNumber = Convert.ToString(Math.Floor(random.NextDouble() * 9_000_000_000L + 1_000_000_000L));
            return accountNumber;
        }
    }
}
