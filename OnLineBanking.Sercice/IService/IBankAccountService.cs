using AspNetCoreHero.Results;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Sercice.Interfaces
{
    public  interface IBankAccountService
    {
        Task <Response<Account>>Authenticate(string AccounNumber, string password);
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Response<Account>> GetAccountByAccountNumber(string AccountNumber);
        Task<Response<Account>> Create(Account account, string password, string confirmPassword);
        Task<Response<Account>> Update(Account account);
        Task<Result<string >> Delete(int accountId);
        Task<Result<Account>> GetByEmail(string Email);
    }
}
