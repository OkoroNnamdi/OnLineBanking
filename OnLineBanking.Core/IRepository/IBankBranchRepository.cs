using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
    public  interface IBankBranchRepository:IGenericRepository<IBankBranchRepository >
    {
        void AddBankBranch(string Manager_ID, BankBranch bank);
        void UpdateAsync(BankBranch bank);
    }
}
