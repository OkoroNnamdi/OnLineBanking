using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Sercice
{
    public  interface ICurrencyCoversionService
    {
        Task<double> GetCurrencyApiAsync(string currency);
    }
}
