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
        Task<BankBranch> GetBranchManager(string managerId);
        Task<BankBranch> GetAllBankBranchTransaction(string BankId);
        Task<IQueryable<Payment>> GetAllCustomerTransactions(string bankId, string customerId);
        Task<IQueryable<Payment>> GetAllPaymentForManager(string managerId);
        Task<IQueryable<Payment>> CustomerTransactions(string customerId);
    }
}
