using Microsoft.EntityFrameworkCore;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public  class ManagerRequestRepository:GenericRepository<ManagerRequest>,IManagerRequestRepository
    {
        private readonly OnlineBankDBContext _dbContext;
        public ManagerRequestRepository(OnlineBankDBContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ManagerRequest> GetManagerRequestById(string managerId)
        {
            try
            {
                var managerRequest = await _dbContext.ManagerRequests.Where(manager => manager.Id == managerId).FirstOrDefaultAsync();
                return managerRequest;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
