
using Microsoft.EntityFrameworkCore;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly OnlineBankDBContext _bankDbContext;
        public PaymentRepository(OnlineBankDBContext bankDbContext) : base(bankDbContext)
        {
            _bankDbContext = bankDbContext;
        }

        public Task<IQueryable<Payment>> CustomerTransactions(string customerId)
        {
            throw new NotImplementedException();
        }

        public Task<BankBranch> GetAllBankBranchTransaction(string BankId)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Payment>> GetAllCustomerTransactions(string bankId, string customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Payment>> GetAllPaymentForManager(string managerId)
        {
            throw new NotImplementedException();
        }

        public async Task<BankBranch> GetBranchManager(string managerId)
        {
           var bank =await _bankDbContext.BankBranches.Include(c=>c.Manager )
                .Where(x=>x.ManagerId == managerId).FirstOrDefaultAsync ();
            return bank;
        }
    }
}
