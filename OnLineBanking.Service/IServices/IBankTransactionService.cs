using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Service.IServices
{
    public  interface IBankTransactionService
    {
        Task<Response<string >> CreateNewTransaction(Transaction transactions);
        Task<Response<string>> FindTransactionByDate(DateTime dateTime);
        Task<Response<string>> MakeDeposit(string walletNumber, double amount, string TransactionPin);
        Task<Response<string>> MakeWithdrawal(string walletNumber, double amount, string TransactionPin);
        Task<Response<string>> MakeTransfer(string fromWallet, string toWallet, double amount, string TransactionPin);
        Task<Response<string>> CheckWalletBalance(string walletNumber, string currency);
    }
}
