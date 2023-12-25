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
    public class AccountRepository :GenericRepository<Account> ,IBankAccounRepository
    {
        private readonly OnlineBankDBContext _context;
        public AccountRepository(OnlineBankDBContext context):base(context)
        {
            
        }
        public Task<Account> Authenticate(string AccounNumber, string password)
        {
            throw new NotImplementedException();
        }
        public Task<Account> Create(Account account, string password, string confirmPassword)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int accountId)
        {
            throw new NotImplementedException();
        }
        public Task<Account> GetAccountByAccountNumber(string AccountNumber)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Account>> GetAllAccounts()
        {
            throw new NotImplementedException();
        }
        public Task<Account> GetByEmail(string Email)
        {
            throw new NotImplementedException();
        }
        public Task<Account> Update(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
