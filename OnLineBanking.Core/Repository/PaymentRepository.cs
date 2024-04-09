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

        public Task<IQueryable<Payment>> GetAllCustomerTransactions(string hotelId, string customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Payment>> GetAllTransactionForManager(string managerId)
        {
            throw new NotImplementedException();
        }

        public Task<Manager> GetBranchManager(string managerId)
        {
            throw new NotImplementedException();
        }
    }
}
