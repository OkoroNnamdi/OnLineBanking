using OnLineBanking.Core.Domain;
using OnLineBanking.Core.IRepository;
using Raven.Client.Documents;
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

        public async Task< Manager> GetManagerPerBank(string Id)
        {
          var manager = _context.Managers.Include(x=>x.BankBranch).
                Where (x=>x.Id == Id).FirstOrDefaultAsync();
            return await manager;
        }

        public async Task<Manager> GetManager(string Id)
        {
         var manager= _context.Managers.FirstOrDefaultAsync(x => x.Id == Id);
            return await manager;
        }
    }
}
