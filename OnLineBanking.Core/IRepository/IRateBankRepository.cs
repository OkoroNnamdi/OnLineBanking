using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
    public  interface IRateBankRepository:IGenericRepository <Rating>
    {
        Task RateBank(Rating Rating);
    }
}
