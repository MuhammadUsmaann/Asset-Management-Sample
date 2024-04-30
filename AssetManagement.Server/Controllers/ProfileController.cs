using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using AssetManagement.Server.Infrastructure.Helpers;
using AssetManagement.Server.Infrastructure.BusinessLogic.Repositories;
using AssetManagement.Server.Infrastructure.ViewModels;
using AssetManagement.Server.Infrastructure.Exceptions;

namespace AssetManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController(IProfileRepo profileRepo) : ControllerBaseApi
    {
        private readonly IProfileRepo _profileRepo = profileRepo;

        [HttpPost("update-profile")]
        [ProducesResponseType(typeof(ResponseTypeView<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProfile(UpdateProfileView model, CancellationToken token = default)
        {
            if (ModelState.IsValid)
            {
                var result = await _profileRepo.UpdateProfileAsync(model, token);
                return CreateResponse(result);
            }
            throw new BadRequestHelperException(ExceptionEnum.USER, ExceptionErrorCodes.INVALID_VIEW_MODEL, ModelState);
        }

        [HttpPost("update-password")]
        [ProducesResponseType(typeof(ResponseTypeView<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordView model, CancellationToken token = default)
        {
            if (ModelState.IsValid)
            {
                var result = await _profileRepo.UpdatePasswordAsync(model, token);
                return CreateResponse(result);
            }
            throw new BadRequestHelperException(ExceptionEnum.USER, ExceptionErrorCodes.INVALID_VIEW_MODEL, ModelState);
        }
    }
}
