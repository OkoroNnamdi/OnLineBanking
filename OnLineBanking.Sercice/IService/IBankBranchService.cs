using OnLineBanking.Core.DTO;
using OnLineBanking.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Service.IService
{
  public  interface IBankBranchService
    {
        Task<Response<List<GetBankDto>>> Getbanks();
        Task<Response<UpdateBankDto>> UpdateBank(UpdateBankDto update, string Id);
        Task<Response<GetBankDto>> GetBankById(string Id);
        Task<Response<List<GetBankByRatingDto>>> GetBankRating(string BranchName);
        Task<Response<string>> AddBank(string Manager_ID, AddBankDto addBankDto);
        Task<Response<string>> DeleteBankById(string id);
        Task<Response<List<GetBankByRatingDto>>> GetBankByState(string State);
        

    }
}
