using OnLineBanking.Core.Domain;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
    public interface IBankAccountRepository:IGenericRepository<Account>
    {
       Task<Response<String>> Authenticate(AuthenticateDto authenticate);
       Task<Response <AccountDto>> Create(CreateAccountDto createAccount);
        Task UpdateAsync(Account  bank);
    }
}
