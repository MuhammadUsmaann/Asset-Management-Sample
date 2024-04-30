using AssetManagement.Server.Infrastructure.DB.Context;
using AssetManagement.Server.Infrastructure.DB.Models;
using AssetManagement.Server.Infrastructure.Exceptions;
using AssetManagement.Server.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Server.Infrastructure.BusinessLogic.Repositories
{
    public interface IProfileRepo
    {
        Task<bool> UpdateProfileAsync(UpdateProfileView input, CancellationToken token = default);
        Task<bool> UpdatePasswordAsync(UpdatePasswordView input, CancellationToken token = default);
    }

    public class ProfileRepo(AssetManagementDbContext assetMngmntDBContext, UserManager<UserProfile> userManager) : IProfileRepo
    {
        private readonly AssetManagementDbContext _assetMngmntDBContext = assetMngmntDBContext;
        private readonly UserManager<UserProfile> _userManager = userManager;

        public async Task<bool> UpdatePasswordAsync(UpdatePasswordView input, CancellationToken token = default)
        {
            var user = await _assetMngmntDBContext.UserProfiles.FirstOrDefaultAsync() ?? throw new KeyNotFoundHelperException(ExceptionEnum.USER, input.Id.ToString());

            if (!string.IsNullOrEmpty(input.OldPassword) && !string.IsNullOrEmpty(input.NewPassword))
            {
                var changePassword = await _userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword);

                if (!changePassword.Succeeded)
                    throw new BadRequestHelperException(ExceptionEnum.USER, ExceptionErrorCodes.UNPROCESSABLE_ENTITY, changePassword.Errors);
            }

            return true;
        }

        public async Task<bool> UpdateProfileAsync(UpdateProfileView input, CancellationToken token = default)
        {
            var user = await _assetMngmntDBContext.UserProfiles.FirstOrDefaultAsync() ?? throw new KeyNotFoundHelperException(ExceptionEnum.USER, input.Id.ToString());

            user.UserFullname = input.UserFullname;
            user.UserName = input.Email;
            user.Email = input.Email;
            //user.RoleId = input.RoleId;
            
            var result = await _userManager.UpdateAsync(user);

            return true;
        }
    }
}
