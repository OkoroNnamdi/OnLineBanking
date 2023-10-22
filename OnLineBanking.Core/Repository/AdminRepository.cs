using AspNetCoreHero.Results;
using Microsoft.AspNetCore.Identity;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.Enum;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public AdminRepository(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async  Task<Result<string>> AddUserRole(string userId, Role role)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null) return await Result<string>.FailAsync("User ID should not be empty");

                var result = await _userManager.AddToRoleAsync(user, role.ToString());
                if (!result.Succeeded)
                {
                    return await Result<string>.FailAsync("Role Addition failed");
                }
                return await Result<string>.SuccessAsync("Role Added sucessfully");
            }
            catch (Exception ex)
            {

                return await Result<string>.FailAsync(ex.Message + "Role Addition failed");
            }
        }

        public async Task<Result<string>>  CreateRole(RoleDTO role)
        {
            try
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = (role.RoleName).ToString(),
                };
                var result = await _roleManager.CreateAsync(identityRole);

                if (!result.Succeeded)
                {
                    return await Result<string>.FailAsync("Role failed to create");
                }
                return await Result<string>.SuccessAsync("Role created sucessfully");
            }
            catch (Exception ex)
            {

                return await Result<string>.FailAsync(ex.Message + "Role failed to create");
            }
        }

        public async Task<Result <string>> RemoveUserRole(string userId, Role role)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null) return await Result<string>.FailAsync("User ID should not be empty");


                var result = await _userManager.RemoveFromRoleAsync(user, role.ToString());

                if (!result.Succeeded)
                {
                    return await Result<string>.FailAsync("Role Removing failed");
                }
                return await Result<string>.SuccessAsync("Role Removed sucessfully");
            }
            catch (Exception ex)
            {

                return await Result<string>.FailAsync(ex.Message+ "Role Removing failed");
            }
        }
    }
}
