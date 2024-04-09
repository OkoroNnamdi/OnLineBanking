using OnLineBanking.Core.Utility;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OnLineBanking.Core.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly OnlineBankDBContext _context;
        public CustomerRepository(OnlineBankDBContext BankDBContext) : base(BankDBContext)
        {
            _context = BankDBContext;
        }

        public async  Task<Customer> GetCustomer(string Id)
        {
            try
            {
                var customer = await _context.Customers.Where
                        (a => a.Accounts.Any(b => b.Id == Id))
                        .FirstOrDefaultAsync();
                return customer;
            }
            catch (Exception )
            {

                throw ;
            }
        }

        public async Task<IQueryable<Customer>> GetCustomers(int pageNo)
        {
            try
            {
                var customer = await _context.Customers.Include(a => a.AppUser).ToListAsync();
                var listOfCustomers = customer.AsQueryable();
                return listOfCustomers;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async  Task<List<Customer>> GetCustomersByBankBranch(string BankBranchId)
        {
            try
            {
                var customer = await _context.Customers.Where
               (c => c.Transactions.Any(a => a.BranchId == BankBranchId))
               .Include(b => b.AppUser).ToListAsync();
              
                return customer;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async  Task<Response<List<Customer>>> GetTopBankBranch(string BankBranchId)
        {

            try
            {
                var topCustomers = _context.Customers
                                       .Include(b => b.AppUser)
                                       .Select(a => new
                                       {
                                           Customer = a,
                                           TotalAmount = a.Transactions.Where(a => a.BranchId == BankBranchId)
                                           .Select(b => b.TransactionAmount).Sum()

                                       }).OrderByDescending(a => a.TotalAmount)
                                       .Select(a => a.Customer)
                                       .ToListAsync();
                return new Response<List<Customer>> { Data = await topCustomers };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
