using OnLineBanking.Core;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.IRepository;
using OnLineBanking.Core.IServices;
using OnLineBanking.Core.Utility;
using OnLineBanking.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Service.Service
{
    
    public  class RateBankService:IRateBankService
    {
        private readonly IRateBankRepository _rateBankRepository;
        private readonly IBankBranchRepository _bankBranchRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public RateBankService(IRateBankRepository rateBankRepository,IBankBranchRepository 
            bankBranchRepository,IUnitOfWork unitOfWork,
            ICustomerRepository customerRepository) {
            _rateBankRepository = rateBankRepository;
            _bankBranchRepository = bankBranchRepository;
             _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
           
        }

        public async Task<Response<string>> RateBank(RateBankDTO rateBankDto)
        {
            try
            {
                var bank = await _bankBranchRepository.GetByIdAsync(x => x.Id == rateBankDto.BankId);
                if (bank == null)
                {
                    return new Response<string>
                    {
                        Data = rateBankDto.BankId,
                        Succeeded = false,
                        Message = "Bank Not Found",
                        StatusCode = 404
                    };
                }

                var customer = await _customerRepository.GetByIdAsync(x => x.Id == rateBankDto.CustomerId);
                if (customer == null)
                {
                    return new Response<string>
                    {
                        Data = rateBankDto.CustomerId,
                        Succeeded = false,
                        Message = "Customer Not Found",
                        StatusCode = 404
                    };
                }

                var newRating = new Rating()
                {
                    Ratings = rateBankDto.Rating,
                    CustomerId = rateBankDto.CustomerId,
                    Customer = customer,
                    BankBranchId = rateBankDto.BankId,
                    BankBranch = bank
                };
                await _unitOfWork.rateBankRepository.RateBank(newRating);
                return Response<string>.Success("Rated Successfully", bank.BranchName);
            }
            catch (Exception ex)
            {
                return Response<string>.Fail ($"Rating Failed {ex.Message }" );
              
            }
        }
    }
}
