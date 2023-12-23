using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
    public  interface IPaymentRepository: IGenericRepository<Payment>
    {
        Task<Manager> GetBranchManager(string managerId);
        Task<BankBranch> GetAllBankBranchTransaction(string BankId);
        Task<IQueryable<Payment>> GetAllCustomerTransactions(string hotelId, string customerId);
        Task<IQueryable<Payment>> GetAllTransactionForManager(string managerId);
        Task<IQueryable<Payment>> CustomerTransactions(string customerId);
    }
}
