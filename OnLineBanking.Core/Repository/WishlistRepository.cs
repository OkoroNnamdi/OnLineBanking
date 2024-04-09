using OnLineBanking.Core.Domain;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class WishlistRepository : GenericRepository<WishList>, IWishlistRepository
    {
        private readonly OnlineBankDBContext _dbContext;
        public WishlistRepository(OnlineBankDBContext bankDbContext) : base(bankDbContext)
        {
            _dbContext = bankDbContext;
        }
    }
}
