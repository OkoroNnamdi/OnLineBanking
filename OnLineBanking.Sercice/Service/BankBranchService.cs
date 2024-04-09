using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnLineBanking.Core;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.IServices;
using OnLineBanking.Core.Utility;
using OnLineBanking.Service.IService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Service.Service
{
    public class BankBranchService : IBankBranchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly OnlineBankDBContext _context;
        public BankBranchService(IUnitOfWork unitOfWork,IMapper mapper, OnlineBankDBContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async  Task<Response<string>> AddBank(string Manager_ID, AddBankDto addBankDto)
        {
            try
            {
                if (string.IsNullOrEmpty(Manager_ID))
                {
                    return Response<string>.Fail("Please enter Manager_ID");
                }
                var bankManager = await _context.Managers.SingleOrDefaultAsync(x => x.Id == Manager_ID);
                if (bankManager == null)
                {
                    return new Response<string>
                    {
                        Data = Manager_ID,
                        Succeeded = false,
                        StatusCode = 404,
                        Message = "Manager Not found"
                    };
                }

                var newBank = _mapper.Map<BankBranch>(addBankDto);
                _unitOfWork.BankBranchRepository.AddBankBranch(bankManager.Id, newBank);
                await _unitOfWork.SaveChanges();
                return Response<string>.Success("Bank Added Sucessful", bankManager.BankBranchId);
            }
            catch (Exception ex)
            {

                return Response<string>.Fail ("Bank addition failed"+ ex.Message );
            }
            
        }
        public async  Task<Response<string>> DeleteBankById(string bank_ID)
        {
            try
            {
                if (string.IsNullOrEmpty(bank_ID))
                {
                    return Response<string>.Fail("Please enter BankID");
                }
                var bank = await _unitOfWork.BankBranchRepository.GetByIdAsync(x => x.Id == bank_ID);
                if (bank == null)
                {
                    return new Response<string>
                    {
                        Data = null,
                        Message = "Bank not found",
                        StatusCode = 404,
                        Succeeded = false
                    };
                }
                await _unitOfWork.BankBranchRepository.DeleteAsync(bank);
                await _unitOfWork.SaveChanges();
                return Response<string>.Success("Bank deleted sucessful", bank.BranchName);
            }
            catch (Exception ex)
            {

                return Response<string>.Fail ("Bank failed to delete" + ex.Message );
            }
        }
        public async  Task<Response<GetBankDto>> GetBankById(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id)) return Response<GetBankDto>.Fail("Enter the ID");
                var bank = await _unitOfWork.BankBranchRepository.GetByIdAsync(x => x.Id == Id);
                var mapBank = _mapper.Map<GetBankDto>(bank);
                if (mapBank == null)
                {
                    return new Response<GetBankDto>
                    {
                        Data = null,
                        Message = "Bank not found",
                        StatusCode = 404,
                        Succeeded = false
                    };
                }
                return new Response<GetBankDto>()
                {
                    Data = mapBank,
                    Message = "Successful",
                    StatusCode = 202,
                    Succeeded = true
                };
            }
            catch (Exception ex)
            {

                return new Response<GetBankDto>
                {
                    Data = null,
                    Message =ex.Message ,
                    StatusCode = 404,
                    Succeeded = false
                };
            }
        }

        public async Task<Response<List<GetBankByRatingDto>>> GetBankByState(string State)
        {
            try
            {
                if (string.IsNullOrEmpty(State)) return new Response<List<GetBankByRatingDto>>
                {
                    Message = "Please specify the state",
                    Data = null,
                    Succeeded = false,
                    StatusCode = 100
                };
                var stateBank = await _unitOfWork.BankBranchRepository.GetAllAsync(x => x.BranchState == State);
                var mappedBanks = _mapper.Map<List<GetBankByRatingDto>>(stateBank);
                if (mappedBanks == null) return Response<List<GetBankByRatingDto>>.Fail($"Bank Not Found in {State}");
                return Response<List<GetBankByRatingDto>>.Success(State, mappedBanks);
            }
            catch (Exception ex)
            {
                return Response<List<GetBankByRatingDto>>.Fail(ex.Message)
                    ;
            }
        }

        public async Task<Response<List<GetBankByRatingDto>>> GetBankRating(string BranchName)
        {
            try
            {
                var bankRating = await _unitOfWork.BankBranchRepository.GetAllAsync(x => x.BranchName == BranchName);
                if (bankRating == null)
                {
                    return Response<List<GetBankByRatingDto>>.Fail("Bank doest not exist");
                }
                var mapperBank = _mapper.Map<List<GetBankByRatingDto>>(bankRating);
                if (mapperBank == null) return Response<List<GetBankByRatingDto>>.Fail($"Bank with {BranchName} not found");
                return Response<List<GetBankByRatingDto>>.Success(BranchName, mapperBank);
            }
            catch (Exception ex)
            {

                return Response<List<GetBankByRatingDto>>.Fail(ex.Message);
            }
        }

        public async Task<Response<List<GetBankDto>>> Getbanks()
        {
            try
            {
                var banks = await _unitOfWork.BankBranchRepository.GetAllAsync();
                var mapBanks = _mapper.Map<List<GetBankDto>>(banks);
                if (mapBanks == null)
                {
                    return new Response<List<GetBankDto>>
                    {
                        Data = null,
                        Message = "Bank not found",
                        StatusCode = 404,
                        Succeeded = false
                    };
                }
                return new Response<List<GetBankDto>>
                {
                    Data = mapBanks,
                    Message = "Sucessful",
                    StatusCode = 202,
                    Succeeded = true
                };
            }
            catch (Exception ex)
            {
                return new Response<List<GetBankDto>>
                {
                    Data = null,
                    Message = ex.Message ,
                    StatusCode = 404,
                    Succeeded = false
                };
            }
        }

        public async Task<Response<UpdateBankDto>> UpdateBank(UpdateBankDto update, string Id)
        {
            if (string.IsNullOrEmpty(Id)) return Response<UpdateBankDto>.Fail("Enter the Bank Id to Update");
           var updateBank = await _unitOfWork.BankBranchRepository.GetByIdAsync (x=> x.Id == Id);
           var mappedBank = _mapper.Map(update, updateBank);
            if (mappedBank == null) return new  Response<UpdateBankDto>
            {
                Data = null,
                Message = "Bank not found",
                StatusCode = 404,
                Succeeded = false
            };
            await _unitOfWork.SaveChanges();
            return Response<UpdateBankDto>.Success("Bank Sucessfully Updated", update);
               
        }
    }
}
