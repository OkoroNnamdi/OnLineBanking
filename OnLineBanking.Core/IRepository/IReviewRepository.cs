using Microsoft.EntityFrameworkCore.Query.Internal;
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
       Task<IEnumerable < Review>> GetCustomerReview(string Bank_ID);

       Task <IQueryable <Review>> GetBankReviews(string BankId);
         Task  AddReview(string customer_Id,Review review);
    }
}
