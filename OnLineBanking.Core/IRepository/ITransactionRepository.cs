using Microsoft.AspNetCore.Http;
using OnLineBanking.Core.Domain;
using AspNetCoreHero.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
  public  interface ITransactionRepository:IGenericRepository<Transaction>
   {
        Task<Result<string>> CreateNewTransaction(Transaction transactions);
        Task<Result<string>> FindTransactionByDate(DateTime dateTime);
        Task<Result<string>> MakeDeposit(string accountNumber, double amount, string TransactionPin);
        Task<Result<string>> MakeWithdrawal(string accountNumber, double amount, string TransactionPin);
        Task<Result<string>> MakeTransfer(string fromaccount, string toAccount, double amount, string TransactionPin);
        Task<Result<string>> CheckAccountBalance(string accountNumber, string currency);
    }
}
