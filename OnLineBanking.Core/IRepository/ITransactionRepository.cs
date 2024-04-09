using Microsoft.AspNetCore.Http;
using OnLineBanking.Core.Domain;
using AspNetCoreHero.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.Utility;

namespace OnLineBanking.Core.IRepository
{
  public  interface ITransactionRepository:IGenericRepository<Transaction>
   {
        Task<Result<string  >> CreateNewTransaction(TransactionDto transactions);
       Task<Result<List<TransactionDto>>> FindTransactionsByDate(DateTime dateTime);
        Task<Response<string > >MakeDeposit(DepositDto depositDto);
        Task<Result<string>> MakeWithdrawal(WithDrawDto withDrawDto);
        Task<Result<string>> MakeTransfer(TransferDto transferDto);
        Task<Result<string>> CheckAccountBalance(string accountNumber, string currency);
        Task< Result<List<TransactionDto>>> FindTransactions(string accountNumber, DateTime dateTime);
        Task<Result<TransactionDto>> FindTransaction(string ID, string accountNumber);
        Task<Result<List<TransactionDto>>> FindTransaction(string accountNumber);
    }
}
