using AspNetCoreHero.Results;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.Enum;
using OnLineBanking.Core.IRepository;
using OnLineBanking.Core.Utility;
using OnLineBanking.Sercice;
using Org.BouncyCastle.Asn1.Esf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account = OnLineBanking.Core.Domain.Account;

namespace OnLineBanking.Core.Repository
{
    public class BankTransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly OnlineBankDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrencyCoversionService _currencyCoversion;
        public BankTransactionRepository(OnlineBankDBContext bankDbContext,IMapper mapper) : base(bankDbContext)
        {
            _dbContext = bankDbContext;
            _mapper = mapper;
        }

        public async  Task<Result<string>> CheckAccountBalance(string accountNumber, string currency)
        {
            try
            {
                var account = _dbContext.accounts.FirstOrDefault(c => c.Account_Number == accountNumber);
                if (account == null)
                    throw new ArgumentException("Bank account not found");
                var curr = _currencyCoversion.GetCurrencyApiAsync(currency);
                return await  Result<string>.SuccessAsync($"Your Balance {currency.ToUpper()} is {curr}");
            }
            catch (Exception ex )
            {

                return await  Result<string>.FailAsync (ex.Message );
            }
        }
        public async Task<Result<string >> CreateNewTransaction(TransactionDto transactions)
        {
            try
            {
               var mappedtransaction =  _mapper.Map<Transaction>(transactions);
                await _dbContext.Transaction.AddAsync(mappedtransaction);
                return await Result<string>.SuccessAsync("Transaction created sucessful");
            }
            catch (Exception ex)
            {

                return await  Result<string>.FailAsync($"Transaction creation failed{ex.Message}");
            }
        }

        public async Task<Result<TransactionDto>> FindTransaction(string ID, string accountNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(ID))
                    throw new ApplicationException("Invalid transaction credentials");
                if (string.IsNullOrEmpty(accountNumber))
                    throw new ApplicationException("Invalid transaction credential");
                var result = await _dbContext.Transaction.FirstOrDefaultAsync(x => x.Id == ID &&
                x.TransactionDestinationAccount == accountNumber);
                if (result == null)
                    throw new ApplicationException("Transaction not found");
                var mappedtransaction = _mapper.Map<TransactionDto>(result);
                return await Result<TransactionDto>.SuccessAsync(data:mappedtransaction);
            }
            catch (Exception ex)
            {

                return await  Result<TransactionDto>.FailAsync(ex.Message);
            }

        }

        public async Task<Result<List<TransactionDto>>> FindTransaction(string accountNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(accountNumber))
                    throw new ApplicationException("Invalid transaction credential");
                var transaction = await _dbContext.Transaction.Where(x =>
                x.TransactionDestinationAccount == accountNumber).ToListAsync();
                if (transaction == null)
                    throw new ApplicationException("Transaction not found");
                var mappedtransact = _mapper.Map<List<TransactionDto>>(transaction);
                return await Result<List<TransactionDto>>.SuccessAsync(data:mappedtransact);
            }
            catch (Exception ex) 
            {
                return await Result<List<TransactionDto>>.FailAsync(ex.Message);
            }
            
        }

        public Task<Result<List<TransactionDto>>> FindTransactions(string accountNumber, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<TransactionDto>>> FindTransactionsByDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        //public Task<Result<string>> FindTransactionByDate(DateTime dateTime)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Response<string >> MakeDeposit(DepositDto depositDto)
        {
            Transaction transaction = new Transaction ();
            Account distinationAccount;
            
            try
            {
                distinationAccount = await _dbContext.accounts.FirstOrDefaultAsync(x => x.Account_Number == depositDto.accountNumber);
                if (distinationAccount == null)
                      throw new ApplicationException("Bank account does not exist");
                if (string.IsNullOrEmpty(distinationAccount.Account_Number))
                    throw new ApplicationException("Invalid Account Number credentials");
                if (distinationAccount.Account_Number != depositDto.accountNumber)
                    throw new ApplicationException("Invalid transaction credential");
                if (depositDto.amount <= 0)
                    throw new ApplicationException("Invalid transaction amount!");
                distinationAccount.Amount += depositDto.amount;
                if ((_dbContext.Entry(distinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    return  new Response<string >() { Succeeded = true };
                }
                else
                {
                    transaction.TransactionStatus = TranStatus.failed;
                }
                Response<string>.Success("Deposit is sucessful", data: null);
            }
            catch (Exception ex)
            {
                return new Response<string >() { Message= ex.Message };
            }
            transaction.Accounts = null   ;
            transaction.TransactionType = Transtype.Deposit;
            transaction.TransactionDestinationAccount = depositDto.accountNumber;
            transaction.TransactionAmount = (decimal)depositDto.amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE => " +
               $"{JsonConvert.SerializeObject(transaction.TransactionSourceAccount)}" +
               $" TO DESTINATION ACCOUNT=>{JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} " +
               $"ON DATE => {transaction.TransactionDate}" +
               $" FOR AMOUNT => {JsonConvert.SerializeObject(transaction.TransactionAmount)}" +
               $" TRANSACTION TYPE =>{JsonConvert.SerializeObject(transaction.TransactionType)}" +
               $" TRANSACTION STATUS => {JsonConvert.SerializeObject(transaction.TransactionStatus)}";
            await _dbContext.Transaction.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
            return new Response<string >
            {
                Message=$"Amount:{distinationAccount.Amount } deposited sucessful at:{distinationAccount.AccountName }",
                Data = null ,
                Succeeded =true,
                 StatusCode =200
            };
        }
        public async Task<Result<string>> MakeTransfer(TransferDto transferDto)
        {
            Transaction transaction = new Transaction();
            Account sourceAccount;
            Account destinationAccount;
            try
            {
                sourceAccount = _dbContext.accounts.Where 
                    (x=>x.Account_Number ==transferDto.Fromaccount).FirstOrDefault();
                destinationAccount = _dbContext.accounts.Where 
                    (x=>x.Account_Number == transferDto.ToAccount).FirstOrDefault();
                if (sourceAccount == null || destinationAccount == null)
                    throw new ApplicationException("Account not found");
                if (transferDto.Amount <= 0)
                    throw new ApplicationException("Invalid transaction amount");
                if (sourceAccount.Account_Number != transferDto.ToAccount)
                    throw new ApplicationException("Invalid transaction credential");
                if (destinationAccount.Account_Number != transferDto.ToAccount)
                    throw new ApplicationException("Invalid transaction credentials");
                sourceAccount.Amount -=transferDto.Amount;
                destinationAccount.Amount +=transferDto.Amount;
                if (_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified
                    && _dbContext.Entry(destinationAccount).State == EntityState.Modified)
                {
                    transaction.TransactionStatus = TranStatus.Success;
                }
                else
                {
                    transaction.TransactionStatus = TranStatus.failed;

                }

            }
            catch(Exception ex)
            {
                return await Result<string>.FailAsync(ex.Message);
            }
            transaction.Accounts = null;
            transaction.TransactionType = Transtype.Transfer;
            transaction.TransactionDestinationAccount = transferDto.ToAccount;
            transaction.TransactionSourceAccount = transferDto.Fromaccount;
            transaction.TransactionAmount = (decimal)transferDto.Amount ;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE => " +
              $"{JsonConvert.SerializeObject(transaction.TransactionSourceAccount)}" +
              $" TO DESTINATION ACCOUNT=>{JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} " +
              $"ON DATE => {transaction.TransactionDate}" +
              $" FOR AMOUNT => {JsonConvert.SerializeObject(transaction.TransactionAmount)}" +
              $" TRANSACTION TYPE =>{JsonConvert.SerializeObject(transaction.TransactionType)}" +
              $" TRANSACTION STATUS => {JsonConvert.SerializeObject(transaction.TransactionStatus)}";
            await _dbContext.Transaction.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
            return Result<string>.Success($"Transfer transaction from:{sourceAccount.AccountName }to:" +
                $"{destinationAccount.AccountName } was sucessful");
        }

        public async Task<Result<string>> MakeWithdrawal(WithDrawDto withDrawDto)
        {
            Transaction transaction = new Transaction();
            Account sourceAccount;
            try
            {
                sourceAccount = await _dbContext.accounts.FirstOrDefaultAsync
                    (x => x.AccountName == withDrawDto.accountNumber);
                if (sourceAccount == null)
                    throw  new ApplicationException("Invalid Account Number credentials");
                if (withDrawDto .amount <0)
                    throw new ApplicationException("Invalid Amount");
                if (sourceAccount.Account_Number != withDrawDto.accountNumber)
                    throw new ApplicationException("Invalid Account Number credentials");
                if (withDrawDto.amount > sourceAccount.Amount)
                    throw new ApplicationException("Insufficient Amount");
                sourceAccount.Amount -= withDrawDto.amount;
                if(_dbContext.Entry (sourceAccount).State ==EntityState.Modified)
                {
                    transaction.TransactionStatus = TranStatus.Success;
                }
                else
                {
                    transaction.TransactionStatus = TranStatus.failed;
                }
            }
            catch(Exception ex)
            {
                return new Result<string>() { Message= ex.Message};
            }
            transaction.Accounts = null;
            transaction.TransactionType = Transtype.Withdrawal;
            transaction.TransactionDestinationAccount = withDrawDto.accountNumber;
            transaction.TransactionAmount = (decimal)withDrawDto.amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE => " +
              $"{JsonConvert.SerializeObject(transaction.TransactionSourceAccount)}" +
              $" TO DESTINATION ACCOUNT=>{JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} " +
              $"ON DATE => {transaction.TransactionDate}" +
              $" FOR AMOUNT => {JsonConvert.SerializeObject(transaction.TransactionAmount)}" +
              $" TRANSACTION TYPE =>{JsonConvert.SerializeObject(transaction.TransactionType)}" +
              $" TRANSACTION STATUS => {JsonConvert.SerializeObject(transaction.TransactionStatus)}";
            await _dbContext.Transaction.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
            return Result<string>.Success("Withdrawal transaction was sucessful");
        }
    }
}
