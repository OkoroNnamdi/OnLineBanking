using AutoMapper;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.Domain.DTO;
using OnLineBanking.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Infrastructure.Extensions.Automapper
{
    public  class MapInitializer:Profile
    {
        public Mapper regMapper { get; set; }
        public MapInitializer()
        {
            var regConfig = new MapperConfiguration(conf => conf.CreateMap<RegisterUserDTO, AppUser>());
            regMapper = new Mapper(regConfig);
            // reviewdto

            CreateMap<Review, AddReviewsDTO>().ReverseMap();
            CreateMap<GetReviewsDTO, GetReviewsDTO>().ReverseMap();
            //Review Maps
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
            //Bank
            CreateMap<BankBranch, UpdateBankDto>().ReverseMap();
            CreateMap<BankBranch, GetBankDto>().ReverseMap();
            CreateMap<BankBranch, GetBankDto>().ReverseMap();
            CreateMap<Rating, GetBankByRatingDto>().ReverseMap();
            CreateMap<BankBranch, AddBankDto>().ReverseMap();
            //Bank Account
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap < Account,CreateAccountDto >().ReverseMap();
            //Bank Transaction
            CreateMap<Transaction , TransactionDto>().ReverseMap();
        }
    }
}
