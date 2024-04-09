using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IServices
{
    public  interface IUnitOfWork:IDisposable
    {
       
        IBankBranchRepository BankBranchRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IManagerRepository ManagerRepository { get; }
        IManagerRequestRepository RequestRepository { get; }
        IRateBankRepository RankRepository { get; }
        IUpdateAppUserRepository UpdateAppUserRepository { get; }
        IPaymentRepository paymentRepository { get; }
        IReviewRepository reviewRepository { get; }
        IBankAccountRepository BankAccounRepository { get; }
        ITransactionRepository transactionRepository { get; }
        IAdminRepository adminRepository { get; }
        IUserRepository userRepository { get; }
        IAuthenticationRepository authenticationRepository { get; }
        IWishlistRepository wishlistRepository { get; }
        IRateBankRepository rateBankRepository { get; }
        Task SaveChanges();
        void BeginTransaction();
        void Rollback();


    }
}
