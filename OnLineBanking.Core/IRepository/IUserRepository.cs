using OnLineBanking.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IRepository
{
    public  interface IUserRepository
    {
        Task<IEnumerable<AppUserDto>> GetUserAsynce(string? Role);
        Task<bool> DeleteUserAsync(string currentUserId, string userIdToDelete);
        Task<AppUserDtoForUpdate> UpdateUserDetails(string currentUserId, string userId, AppUserDtoForUpdate model);
    }
}
