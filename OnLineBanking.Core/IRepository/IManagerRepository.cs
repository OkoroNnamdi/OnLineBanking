using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
    public  interface IManagerRepository:IGenericRepository <Manager>
    {
        Task<Manager> GetManager(string Id);

        Task<Manager> GetManagerPerBank(string Id);
    }
}
