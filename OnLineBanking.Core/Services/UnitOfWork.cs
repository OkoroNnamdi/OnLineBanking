
using OnLineBanking.Core.IRepository;
using OnLineBanking.Core.IServices;
using OnLineBanking.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Services
{
    public class UnitOfWork : IUnitOfWork

    {
        private readonly OnlineBankDBContext _db;

        private IAdminRepository _AdminRepository;
        private  IBankBranchRepository _BankBranchRepository;
        private ICustomerRepository _CustomerRepository;
        private IManagerRepository _ManagerRepository;
        private IManagerRequestRepository _RequestRepository;
        private IRateBankRepository _RankRepository;
        private IUpdateAppUserRepository _UpdateAppUserRepository;
        private IUserRepository _UserRepository;
        private IBankAccountRepository _BankAccounRepository;
        private IBankBranchRepository _BankBranchRepositoryBank;
        private IAuthenticationRepository _authenticationRepository;
        private IPaymentRepository _PaymentRepository;
        private IReviewRepository _reviewRepository;
        private ITransactionRepository _transactionRepository;
        private IWishlistRepository _wishlistRepository;
        public UnitOfWork(OnlineBankDBContext db)
        {
            _db=db;
        }
        private bool _disposed;
        public IBankBranchRepository BankBranchRepository => _BankBranchRepository ??= new BankBranchRepository(_db);
        public ICustomerRepository CustomerRepository => _CustomerRepository ??= new CustomerRepository(_db);

        public IManagerRepository ManagerRepository => _ManagerRepository ??= new ManagerRepository(_db);

        public IManagerRequestRepository RequestRepository => _RequestRepository??= new ManagerRequestRepository(_db);

        public IRateBankRepository RankRepository => _RankRepository??=new RateBankRepository(_db);

        public IUpdateAppUserRepository UpdateAppUserRepository => _UpdateAppUserRepository ??= new UpdateAppUserRepository(_db);

        public IPaymentRepository paymentRepository => _PaymentRepository??=new PaymentRepository(_db);
        public IReviewRepository reviewRepository =>_reviewRepository??=new ReviewRepository (_db);
        public ITransactionRepository transactionRepository =>
            _transactionRepository??=new BankTransactionRepository (_db);
        public IAdminRepository adminRepository => _AdminRepository??=new AdminRepository (_db);
        public IUserRepository userRepository => _UserRepository??= new UserRepository (_db);
        public IAuthenticationRepository authenticationRepository => 
            _authenticationRepository ??= new AuthenticationRepository(_db);
        public IBankAccountRepository BankAccounRepository =>
            _BankAccounRepository ??= new AccountRepository(_db);
        public IWishlistRepository wishlistRepository => _wishlistRepository ??=new WishlistRepository(_db);

        public IRateBankRepository rateBankRepository => throw new NotImplementedException();

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {

            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }

            _disposed = true;
        }
        public void Rollback()
        {
           _db.Database.RollbackTransaction();
        }

        public async  Task SaveChanges()
        {
           await _db.SaveChangesAsync();
        }
    }
}
