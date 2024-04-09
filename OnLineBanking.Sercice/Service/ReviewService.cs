using AspNetCoreHero.Results;
using AutoMapper;
using OnLineBanking.Core;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.IServices;
using OnLineBanking.Core.Utility;
using OnLineBanking.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Service.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly OnlineBankDBContext _context;

        public ReviewService(IUnitOfWork unitOfWork,IMapper mapper,OnlineBankDBContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;       
        }
        public async Task<Response<AddReviewsDTO>> GetCustomerReviewAsync(AddReviewsDTO model, string customerId)
        {
            var addReview = _mapper.Map<AddReviewsDTO>(model);
            await _unitOfWork.reviewRepository.GetCustomerReview(customerId);
            return Response<AddReviewsDTO>.Success("Ok", addReview);

        }
        public async  Task<Result<GetReviewsDTO>> GetBankReviews(string bankId)
        {
            try
            {
                var getReviews = _unitOfWork.reviewRepository.GetBankReviews(bankId);
                var data = _mapper.Map<GetReviewsDTO>(getReviews);
                return await  Result <GetReviewsDTO>.SuccessAsync ( data);
            }
            catch (Exception ex)
            {

                return await Result<GetReviewsDTO>.FailAsync(ex.Message);
            }
        }
        public async Task<Response<Review>> UpdateReview(string Id, UpdateReviewDto updateReviewDto)
        {
            try
            {
                var updateReview = await _unitOfWork.reviewRepository.GetByIdAsync(x => x.Id == Id);
                var mappedUpdate = _mapper.Map<Review>(updateReviewDto);

                if (updateReview == null)
                {
                    return new Response<Review>
                    {
                        StatusCode = 404,
                        Succeeded = false,
                        Data = null,
                        Message = "Hotel not found"
                    };
                }
                else
                {
                    _context.Update(updateReview);
                    await _unitOfWork.SaveChanges();
                    return Response<Review>.Success("Updated Successfully", updateReview);
                }

            }
            catch (Exception ex)
            {

                return Response<Review>.Fail(ex.Message);
            }
        }

        public async  Task<Response<string  >> AddReviewAsync(AddReviewsDTO model, string customerId)
        {
            try
            {
                var customer = _context.Customers.Where(x => x.Id == customerId).FirstOrDefault();
                if (customer == null)
                {
                    return new Response<string>
                    {
                        Data = customer.Id,
                        Succeeded = false,
                        StatusCode = 404,
                        Message = "Customer Not found"
                    };
                }
                var newBankReview = _mapper.Map<Review>(model);
                await _unitOfWork.reviewRepository.AddReview(customerId, newBankReview);
                await _unitOfWork.SaveChanges();
                return Response<string>.Success("Bank Review Sucessfully Added", newBankReview.Comment);

            }
            catch (Exception ex)
            {

                return Response<string>.Fail(ex.Message,404);

            }

        }
    }
    }

