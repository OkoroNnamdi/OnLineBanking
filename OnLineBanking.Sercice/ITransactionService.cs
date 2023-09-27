using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Sercice
{
    public  interface ITransactionService
    {
        Task<Response<string>> CreateNewTransaction(Transaction transactions);
        Task<Response<string>> FindTransactionByDate(DateTime dateTime);
        Task<Response<string>> MakeDeposit(string accountNumber, double amount, string TransactionPin);
        Task<Response<string>> MakeWithdrawal(string accountNumber, double amount, string TransactionPin);
        Task<Response<string>> MakeTransfer(string fromAccount, string toWallet, double amount, string TransactionPin);
        Task<Response<string>> CheckAccountBalance(string accountNumber, string currency);
    }
}
