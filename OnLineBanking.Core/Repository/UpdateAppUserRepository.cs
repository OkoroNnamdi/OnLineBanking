using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class UpdateAppUserRepository : GenericRepository<AppUser>, IUpdateAppUserRepository
    {
        private readonly OnlineBankDBContext _dbContext;
        public UpdateAppUserRepository(OnlineBankDBContext bankDbContext) : base(bankDbContext)
        {
            _dbContext = bankDbContext;
        }

        public async  Task UpdateAsync(AppUser user)
        {
            _dbContext.Entry(user).State=EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
