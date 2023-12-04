using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
    public  interface ICustomerRepository:IGenericRepository <Customer>
    {
        Task<Response<List<Customer>>> GetTopBankBranch(string BankBranchId);
        Task<IQueryable<Customer>> GetCustomers(int pageNo);
        Task<List<Customer>> GetCustomersByBankBranch(string BankBranchId);
        Task<Customer> GetCustomer(string Id);
    }
}
