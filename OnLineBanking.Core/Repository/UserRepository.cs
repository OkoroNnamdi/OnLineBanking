using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineBanking.Application;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BankDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserRepository(BankDbContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;

        }
        public async  Task<bool> DeleteUserAsync(string currentUserId, string userIdToDelete)
        {
            // get the user making the request
            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            // get the user being deleted
            var userToDelete = await _userManager.FindByIdAsync(userIdToDelete);

            if (userToDelete == null)
            {
                return false;
            }
            // check if the current user has the necessary permissions to delete the user
            if (await _userManager.IsInRoleAsync(currentUser, "SuperAdmin"))
            {
                // SuperAdmin can delete any user
                await _userManager.DeleteAsync(userToDelete);
                return true;
            }
            else if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                // Admin can only delete Customers or themselves
                if (await _userManager.IsInRoleAsync(userToDelete, "Customer") || userToDelete.Id == currentUser.Id)
                {
                    await _userManager.DeleteAsync(userToDelete);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (await _userManager.IsInRoleAsync(currentUser, "Customer"))
            {
                // Customers can only delete themselves
                if (userToDelete.Id == currentUser.Id)
                {
                    await _userManager.DeleteAsync(userToDelete);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public async  Task<IEnumerable<AppUserDto>> GetUserAsynce(string Role)
        {
            var users = _context.Users;
            var list = new List<AppUser>();
            if (Role == null)
            {
                var res = _mapper.Map<IEnumerable<AppUserDto>>(users.ToList());
                if (res.Any())
                {
                    return res;
                }
                return null;

            }
            else
            {
                foreach (var user in users)
                {
                    if (await _userManager.IsInRoleAsync(user, Role))
                    {
                        list.Add(user);
                    }
                }
                var result = _mapper.Map<IEnumerable<AppUserDto>>(list);
                if (result.Any()) return result;
                return null;
            }

        }

        public async  Task<AppUserDtoForUpdate> UpdateUserDetails(string currentUserId, string userId, AppUserDtoForUpdate model)
        {
            // Retrieve the current user from the User.Identity object
            var currentUser = await _userManager.FindByIdAsync(currentUserId);

            // Retrieve the user to update from the database
            var userToUpdate = await _userManager.FindByIdAsync(userId);

            // Check if the user to update exists
            if (userToUpdate == null)
            {
                return null;
            }

            // Check if the current user has the required permissions to update the user
            if (!await _userManager.IsInRoleAsync(currentUser, "SuperAdmin") && currentUser.Id != userToUpdate.Id)
            {
                return null;
            }

            // Update the user's properties
            //userToUpdate.FirstName = model.FirstName;
            //userToUpdate.LastName = model.LastName;
            //userToUpdate.UserName = model.UserName;
            //userToUpdate.Avatar = model.Avatar;
            //userToUpdate.Gender = model.Gender;
            var users = _mapper.Map(model, userToUpdate);

            //Update the user's roles if the current user is a SuperAdmin
            if (await _userManager.IsInRoleAsync(currentUser, "SuperAdmin"))
            {
                var role = await _userManager.GetRolesAsync(users);
                await _userManager.RemoveFromRolesAsync(users, role);
                await _userManager.AddToRolesAsync(users, model.Role);
            }

            // Save the changes to the database

            _context.Users.Update(users);
            var roles = await _userManager.GetRolesAsync(users);
            _context.SaveChanges();

            // Create a response object with the updated user's details, including the roles
            var result = _mapper.Map<AppUserDtoForUpdate>(users);
            return result;
        }
    }
}
