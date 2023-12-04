using OnlineBanking.Application;
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
        private readonly BankDbContext _db;
        private IAdminRepository _AdminRepository;
        private  IBankBranchRepository _BankBranchRepository;
        private ICustomerRepository _CustomerRepository;
        private IManagerRepository _ManagerRepository;
        private IManagerRequestRepository _RequestRepository;
        private IRateBankRepository _RankRepository;
        private IUpdateAppUserRepository _UpdateAppUserRepository;
        public UnitOfWork(BankDbContext db)
        {
            _db=db;
        }
        //public IHotelRepository hotelRepository =>
        //    _hotelRepository ??= new HotelRepository(_hotelDbContext);
        public IBankBranchRepository BankBranchRepository => _BankBranchRepository ??= new BankBranchRepository(_db);
        public ICustomerRepository CustomerRepository => throw new NotImplementedException();

        public IManagerRepository ManagerRepository => throw new NotImplementedException();

        public IManagerRequestRepository RequestRepository => throw new NotImplementedException();

        public IRateBankRepository RankRepository => throw new NotImplementedException();

        public IUpdateAppUserRepository UpdateAppUserRepository => throw new NotImplementedException();

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
