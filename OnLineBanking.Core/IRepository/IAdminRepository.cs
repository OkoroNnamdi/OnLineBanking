using AspNetCoreHero.Results;
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
        Task<Result <string >> CreateRole(RoleDTO role);
        Task <Result<string>> AddUserRole(string userId, Role role);
        Task<Result<string>> RemoveUserRole(string userId, Role role);
    }
}
