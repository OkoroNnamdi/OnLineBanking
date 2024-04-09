using AspNetCoreHero.Results;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Service.IService
{
   public  interface IReviewService
   {
        Task<Response<Review>> UpdateReview(string Id, UpdateReviewDto updateReviewDto);

        Task<Response<AddReviewsDTO>> GetCustomerReviewAsync(AddReviewsDTO model, string customerId);
        Task<Response<string  >> AddReviewAsync(AddReviewsDTO model, string customerId);

        //basic comment.
        Task<Result<GetReviewsDTO>> GetBankReviews(string bankId);

        //Task<Response<GetReviewsDTO>> GetHotelReviews(string hotelId);
    }
}
