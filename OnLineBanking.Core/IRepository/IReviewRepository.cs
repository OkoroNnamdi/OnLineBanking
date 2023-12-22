using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
    public interface IReviewRepository: IGenericRepository<Review>
    {
       Task< Review> AddReview(string review);

       Task <IQueryable<Review>> GetHotelReviews(string hotelId);
    }
}
