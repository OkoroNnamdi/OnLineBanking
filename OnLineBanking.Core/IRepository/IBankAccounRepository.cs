using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
    public interface IBankAccounRepository:IGenericRepository<Account>
    {
       Task <Account> Authenticate(string AccounNumber, string password);
       Task <IEnumerable<Account>> GetAllAccounts();
       Task < Account> GetAccountByAccountNumber(string AccountNumber);
       Task < Account> Create(Account account, string password, string confirmPassword);
       Task<Account > Update(Account account);
       Task  Delete(int accountId);
       Task<Account> GetByEmail(string Email);
    }
}
