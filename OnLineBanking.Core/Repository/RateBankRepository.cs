using OnLineBanking.Core.Domain;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class RateBankRepository : GenericRepository<Rating>, IRateBankRepository
    {
        private readonly OnlineBankDBContext _dbContext;
        public RateBankRepository(OnlineBankDBContext bankDbContext) : base(bankDbContext)
        {
            _dbContext = bankDbContext;
        }

        public async Task RateBank(Rating rating)
        {
            await AddAsync(rating);
            await _dbContext.SaveChangesAsync();
        }
    }
}
