using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
    public  interface IManagerRepository:IGenericRepositry <Manager>
    {
        Task<Manager> GetManager(string Id);

        Manager GetBanksByManager(string Id);
    }
}
