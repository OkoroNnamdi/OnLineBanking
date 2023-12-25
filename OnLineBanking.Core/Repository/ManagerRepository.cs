using OnLineBanking.Core.Domain;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class ManagerRepository : GenericRepository<Manager>, IManagerRepository
    {
        private readonly OnlineBankDBContext _context;
        public ManagerRepository(OnlineBankDBContext context):base(context)
        {
            _context = context;
        }

        public Manager GetBanksByManager(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<Manager> GetManager(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
