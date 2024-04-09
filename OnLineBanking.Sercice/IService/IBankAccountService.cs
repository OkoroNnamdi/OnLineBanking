using AspNetCoreHero.Results;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.DTO;
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
        Task <Response<string>>Authenticate(AuthenticateDto authenticate);
        Task<Response<IEnumerable<AccountDto>>> GetAllAccounts();
        Task<Response<AccountDto>> GetAccountByAccountNumber(string accountNumber);
        Task<Response<AccountDto>> Create(CreateAccountDto createAccount);
        Task<Response<AccountDto>> Update(AccountDto account,string Id);
        Task<Result<string >> Delete(string accountId);
        Task<Result<AccountDto>> GetByEmail(string email);

    }
}
