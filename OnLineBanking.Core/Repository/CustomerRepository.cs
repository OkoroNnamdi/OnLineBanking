using OnlineBanking.Application;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly BankDbContext _context;
        public CustomerRepository(BankDbContext bankDbContext) : base(bankDbContext)
        {
            _context = bankDbContext;
        }

        public Task<Customer> GetCustomer(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<Customer>> GetCustomers(int pageNo)
        {
            var customers = await _context.customer
              .Include(a => a.AppUser)
              .ToListAsync();

            var listOfCustomers = customers.AsQueryable();
            return listOfCustomers;
        }

        public Task<List<Customer>> GetCustomersByBankBranch(string BankBranchId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<Customer>>> GetTopBankBranch(string BankBranchId)
        {
            throw new NotImplementedException();
        }
    }
}
