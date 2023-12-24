
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
   public  class BankBranchRepository: GenericRepository<BankBranch>, IBankBranchRepository
    {
        private readonly OnlineBankDBContext _context;
        public BankBranchRepository(OnlineBankDBContext context):base(context )
        {
            _context = context;
        }

        public Task AddAsync(IBankBranchRepository entity)
        {
            throw new NotImplementedException();
        }

        public void AddBankBranch(string Manager_ID, BankBranch bank)
        {
            throw new NotImplementedException();
        }

        public Task<List<IBankBranchRepository>> GetAllAsync(Expression<Func<IBankBranchRepository, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IBankBranchRepository> GetByIdAsync(string id, IBankBranchRepository Value)
        {
            throw new NotImplementedException();
        }

        public Task<IBankBranchRepository> GetByIdAsync(Expression<Func<IBankBranchRepository, bool>> filter = null, bool tracked = true)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(BankBranch bank)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<IBankBranchRepository>> IGenericRepository<IBankBranchRepository>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
