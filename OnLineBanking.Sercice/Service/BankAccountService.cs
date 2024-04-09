using AspNetCoreHero.Results;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using OnLineBanking.Core;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.IRepository;
using OnLineBanking.Core.IServices;
using OnLineBanking.Core.Utility;
using OnLineBanking.Sercice.Interfaces;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Service.Service
{
    public class BankAccountService : IBankAccountService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly OnlineBankDBContext _context;
        private IBankAccountRepository _accountRepository;
        public BankAccountService(IUnitOfWork unitOfWork, IMapper mapper,
            OnlineBankDBContext context,IBankAccountRepository accountRepository)
        {
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
            _context = context;
            _accountRepository = accountRepository;
        }
        public async Task<Response<string>> Authenticate(AuthenticateDto authenticate)
        {
            try
            {
                await _accountRepository.Authenticate(authenticate);
                return Response<string>.Success("Authentication Sucessful", "Sucess");
            }
            catch (Exception ex)
            {

                return Response<string>.Fail("Authentication Sucessful"+ ex.Message);
            }
        }

        public async Task<Response<AccountDto>> Create(CreateAccountDto createAccount)
        {
            try
            {
                var account = await _accountRepository.Create(createAccount);
                var mappedAccount = _mapper.Map<AccountDto>(account);
                return Response<AccountDto>.Success("Account created is sucessful", mappedAccount);
            }
            catch (Exception ex )
            {

                return Response<AccountDto>.Fail("Account created failed" + ex.Message);
            }
        }

        public async Task<Result<string>> Delete(string account_Id)
        {
            if (string.IsNullOrEmpty(account_Id))
            {
                return Result<string>.Fail("Please enter BankID");
            }
            var account = await _unitOfWork.BankAccounRepository.GetByIdAsync(x => x.Id == account_Id);
            if (account == null)
            {
                return new Result<string>
                {
                    Data = null,
                    Message = "Bank not found",
                    Succeeded = false
                };
            }
            await _unitOfWork.BankBranchRepository.DeleteAsync(account);
            await _unitOfWork.SaveChanges();
            return  Result<string>.Success("Bank deleted sucessful", account.AccountName );

        }
        public async Task<Response<AccountDto>> GetAccountByAccountNumber(string accountNumber)
        {
            try
            {
                var account = await _unitOfWork.BankAccounRepository.GetAllAsync(x => x.Account_Number == accountNumber);

                if (account == null)
                {
                    return new Response<AccountDto>()
                    {
                        StatusCode = 404,
                        Succeeded = false,
                        Data = null,
                        Message = "Account not found"
                    };
                }
                var mappedAccount = _mapper.Map<AccountDto>(account);
                return new Response<AccountDto>()
                {
                    Data = mappedAccount,
                    Succeeded = true,
                    StatusCode = 200,
                };
            }
            catch (Exception ex)
            {
                return  Response<AccountDto>.Fail("Account not found" + ex.Message);
            }
        }
        public async Task<Response<IEnumerable<AccountDto>>> GetAllAccounts()
        {
            try
            {
                var bankAccounts = await _unitOfWork.BankAccounRepository.GetAllAsync();
                var mapBanks = _mapper.Map < IEnumerable<AccountDto>>(bankAccounts);
                if (mapBanks == null)
                {
                    return new Response< IEnumerable <AccountDto >>
                    {
                        Data = null,
                        Message = "Bank not found",
                        StatusCode = 404,
                        Succeeded = false
                    };
                }
                return new Response<IEnumerable<AccountDto>>
                {
                    Data = mapBanks,
                    Message = "Sucessful",
                    StatusCode = 202,
                    Succeeded = true
                };
            }
            catch (Exception ex)
            {
                return new Response< IEnumerable < AccountDto >>
                {
                    Data = null,
                    Message = ex.Message,
                    StatusCode = 404,
                    Succeeded = false
                };
            }
        }

        public Task<Result<AccountDto>> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<AccountDto>> Update(AccountDto account, string id)
        {
            try
            {
                var updateAccount = _unitOfWork.BankAccounRepository.GetByIdAsync(x => x.Id == id);
                var mappedUpdate = _mapper.Map(account, updateAccount);
                if (mappedUpdate == null)
                {
                    return new Response<AccountDto>
                    {
                        StatusCode = 404,
                        Succeeded = false,
                        Data = null,
                        Message = "Bank account not found"
                    };
                }
                await _unitOfWork.BankAccounRepository.UpdateAsync(await mappedUpdate);
                await _unitOfWork.SaveChanges();
                return Response<AccountDto>.Success("Bank account updated sucessfully", account);
            }
            catch (Exception ex)
            {

                return Response<AccountDto>.Fail($"Bank account updated  failed {ex.Message}");
            }
        }

        
    }
}
