
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
   public  class BankBranchRepository: GenericRepository<BankBranch>, IBankBranchRepository
    {
        private readonly OnlineBankDBContext _context;
        public BankBranchRepository(OnlineBankDBContext context):base(context )
        {
            _context = context;
        }
        public async  void AddBankBranch(string Manager_ID, BankBranch bank)
        {
            bank.Id = Manager_ID;
           await  AddAsync(bank);
        }
        public async Task UpdateAsync(BankBranch bank)
        {  
            _context.Entry(bank).State =EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
