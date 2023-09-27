using OnLineBanking.Core.DTO;
using OnLineBanking.Core.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
    public interface IAdminRepository
    {
        Task<bool> CreateRole(RoleDTO role);
        Task<bool> AddUserRole(string userId, Role role);
        Task<bool> RemoveUserRole(string userId, Role role);
    }
}
