using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Service.IServices
{
   public interface ICurrencyConversion
    {
         Task<double> GetApiAsync(string currency);
    }
}
