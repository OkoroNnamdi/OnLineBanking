using Microsoft.EntityFrameworkCore;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly OnlineBankDBContext _bankDbContext;
        public ReviewRepository(OnlineBankDBContext bankDbContext) : base(bankDbContext)
        {
            _bankDbContext = bankDbContext;
        }

        public async Task<IEnumerable<Review>> GetCustomerReview(string Bank_ID  )
        {
            try
            {
                var customerView = await _bankDbContext.Reviews.Where 
                    (x=>x.BankbranchId == Bank_ID).ToListAsync();
                if (customerView == null)
                {
                    return null;
                }
                return customerView;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IQueryable<Review>> GetBankReviews(string BankId)
        {
            try
            {
                var query =  _bankDbContext.Reviews.AsNoTracking()
                         .Where(x => x.BankbranchId == BankId)
                         .Include(x => x.BankBranch)
                         .Include(x => x.Customer.AppUser)
                         .OrderBy(x => x.CreatedAt);

                return (IQueryable<Review>)await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async  Task  AddReview(string customer_Id, Review review)
        {
           review .CustomerId = customer_Id;
           await  AddAsync (review);
        }
    }
}
