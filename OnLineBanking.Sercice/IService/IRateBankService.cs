using OnLineBanking.Core.DTO;
using OnLineBanking.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Service.IService
{
   public  interface IRateBankService
   {
        Task<Response<string>> RateBank(RateBankDTO rateHotelDto);
    }
}
