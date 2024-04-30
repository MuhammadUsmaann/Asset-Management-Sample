using AssetManagement.Server.Infrastructure.DB.Context;
using AssetManagement.Server.Infrastructure.DB.Models;
using AssetManagement.Server.Infrastructure.Exceptions;
using AssetManagement.Server.Infrastructure.Extensions;
using AssetManagement.Server.Infrastructure.Helpers;
using AssetManagement.Server.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetManagement.Server.Infrastructure.BusinessLogic.Repositories
{
    public interface IAuthenticationRepo
    {
        Task<LoginResponseView> LoginAsync(LoginRequestView entity, CancellationToken token = default);
        Task<string> UpdateForgotPasswordAsync(string email, CancellationToken cancellationToken = default);
    }

    public class AuthenticationRepo(AssetManagementDbContext context, IOptions<WebAppSettings> appSettings, SignInManager<UserProfile> signInManager) : IAuthenticationRepo
    {
        private readonly AssetManagementDbContext _dbContext = context;
        private readonly SignInManager<UserProfile> _signInManager = signInManager;
        private readonly WebAppSettings _appSettings = appSettings.Value;
        private readonly UserManager<UserProfile> _userManager;

        public async Task<LoginResponseView> LoginAsync(LoginRequestView model, CancellationToken canceltoken = default)
        {
            var user = await GetByEmailAsync(_dbContext, model.Email) ?? throw new KeyNotFoundHelperException(ExceptionEnum.USER, model.Email);

            var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!checkPassword.Succeeded)
            {
                throw new BadRequestHelperException(ExceptionEnum.USER, ExceptionErrorCodes.INVALID_PASSWORD, "Incorrect Password");
            }

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName ?? ""),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = GetToken(authClaims);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponseView
            {
                Token = jwtToken,
                Expiration = token.ValidTo,
                UserFullName = user.UserFullname,
                Email = user.Email,
                Id = user.Id,
                Role = user.RoleId
            };
        }
        public async Task<string> UpdateForgotPasswordAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await GetByEmailAsync(_dbContext, email) ?? throw new KeyNotFoundHelperException(ExceptionEnum.USER, email);
            var hashedPassword = MethodsExt.HashPassword("P@ssw0rd32!", MethodsExt.GenerateSalt());

            user.PasswordHash = hashedPassword;
            await _userManager.UpdateAsync(user);

            return "Mail has sent to the admin ";
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT.Secret));

            var token = new JwtSecurityToken(
                issuer: _appSettings.JWT.ValidIssuer,
                audience: _appSettings.JWT.ValidAudience,
                expires: DateTime.Now.AddDays(7),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static readonly Func<AssetManagementDbContext, string, Task<UserProfile>> GetByEmailAsync =
           EF.CompileAsyncQuery(
               (AssetManagementDbContext context, string id) =>
                   context.UserProfiles.FirstOrDefault(x => x.Email == id));
    }
}
