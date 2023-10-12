﻿using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IServices
{
    public  interface IUnitOfWork
    {
        IAdminRepository AdminRepository { get; }
        IBankBranchRepository BankBranchRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IManagerRepository ManagerRepository { get; }
        IManagerRequestRepository RequestRepository { get; }
        IRateBankRepository RankRepository { get; }
        IUpdateAppUserRepository UpdateAppUserRepository { get; }
    }
}
