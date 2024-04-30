using AssetManagement.Server.Infrastructure.BusinessLogic.Repositories;
using AssetManagement.Server.Infrastructure.Exceptions;
using AssetManagement.Server.Infrastructure.Helpers;
using AssetManagement.Server.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace AssetManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthenticationRepo authService) : ControllerBaseApi
    {
        private readonly IAuthenticationRepo _authService = authService;

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(ResponseTypeView<LoginResponseView>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login(LoginRequestView model, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(model);
                return CreateResponse(result);
            }
            throw new BadRequestHelperException(ExceptionEnum.USER, ExceptionErrorCodes.INVALID_VIEW_MODEL, ModelState);
            
        }

        [HttpGet]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email, CancellationToken cancellationToken = default)
        {
            var result = await _authService.UpdateForgotPasswordAsync(email);
            return CreateResponse(result);
        }
    }
}
