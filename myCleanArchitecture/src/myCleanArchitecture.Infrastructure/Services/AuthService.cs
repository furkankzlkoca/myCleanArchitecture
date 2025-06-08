using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using myCleanArchitecture.Application.Interfaces.Repositories.Base;
using myCleanArchitecture.Application.Interfaces.Services;
using myCleanArchitecture.Infrastructure.Identity;
using myCleanArchitecture.Shared.FeatureModels.Authentication;
using myCleanArchitecture.Shared.FeatureModels.Authentication.Commands;
using myCleanArchitecture.Shared.Helpers.CustomModels;
using myCleanArchitecture.Shared.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace myCleanArchitecture.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly JWT _jwt;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                           RoleManager<AppRole> roleManager, IOptions<JWT> jwt, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<ObjectResult<AuthModel>> Login(LoginUserCommand command)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user is null)
                return new ObjectResult<AuthModel>(Meta.Unauthorized("Email adresi veya şifre hatalı"));

            var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
            if (!result.Succeeded)
                return new ObjectResult<AuthModel>(Meta.Unauthorized("Email adresi veya şifre hatalı"));

            var jwtToken = await CreateJwtToken(user);

            AuthModel authModel = new()
            {
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpiresOn = jwtToken.ValidTo,
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email
            };

            RefreshToken? activeRefreshToken = user.RefreshTokens.FirstOrDefault(x => !x.IsActive);
            if (activeRefreshToken is not null)
            {
                authModel.RefreshToken = activeRefreshToken.Token;
                authModel.RefreshTokenExpiration = activeRefreshToken.ExpiryDate;
            }
            else
            {
                var refreshToken = await GenerateRefreshToken(authModel.Token, user.Id);
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken.ExpiryDate;
            }
            return new ObjectResult<AuthModel>(Meta.Success(), authModel);
        }
        private async Task<JwtSecurityToken> CreateJwtToken(AppUser appUser)
        {
            IList<string> rolesList = await _userManager.GetRolesAsync(appUser);
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(appUser);

            List<Claim> roleClaims = [];
            roleClaims.AddRange(rolesList.Select(x => new Claim("roles", x)));

            List<Claim> permissionClaims = [];
            foreach (var item in rolesList)
            {
                AppRole? role = await _roleManager.FindByNameAsync(item);
                if (role is not null)
                    permissionClaims.AddRange(await _roleManager.GetClaimsAsync(role));
            }

            IList<Claim> foo = await _roleManager.GetClaimsAsync(_roleManager.Roles.First());

            var claims = new[]
            {
                new Claim("email",appUser.Email),
                new Claim("username",appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("uid",appUser.Id.ToString())
            }.Union(userClaims)
            .Union(roleClaims)
            .Union(permissionClaims)
            .Union(foo);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationDays ?? 1),
                signingCredentials: signinCredentials
                );
        }
        private async Task<RefreshToken> GenerateRefreshToken(string token, Guid userId)
        {
            var randomNumber = new byte[32];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);

            RefreshToken refreshToken = new RefreshToken()
            {
                UserId = userId,
                Token = Convert.ToBase64String(randomNumber),
                ExpiryDate = DateTime.Now.AddDays(_jwt.RefreshTokenDurationDays ?? 1)
            };
            _unitOfWork.Repository<RefreshToken>().Add(refreshToken);
            await _unitOfWork.SaveChangesAsync();
            return refreshToken;

        }
        public async Task<Result> RevokeTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == refreshToken));
            if (user is null)
                return new Result(Meta.BadRequest("Geçersiz Token"));

            var token = user.RefreshTokens.Single(x => x.Token == refreshToken);
            if (!token.IsActive)
                return new Result(Meta.BadRequest("Aktif olmayan Token"));

            token.IsRevoked = true;
            token.ExpiryDate = DateTime.Now;
            _unitOfWork.Repository<RefreshToken>().Update(token);
            await _unitOfWork.SaveChangesAsync();
            return new Result(Meta.Success("Token iptal edildi"));
        }

        public async Task<ObjectResult<AuthModel>> RefreshToken(RefreshTokenCommand command)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == command.RefreshToken));
            if (user is null)
                return new ObjectResult<AuthModel>(Meta.Unauthorized());

            var refreshToken = user.RefreshTokens.Single(x => x.Token == command.RefreshToken);
            if (!refreshToken.IsActive)
                return new ObjectResult<AuthModel>(Meta.Unauthorized());

            refreshToken.ExpiryDate = DateTime.Now;
            _unitOfWork.Repository<RefreshToken>().Update(refreshToken);

            var jwtToken = await CreateJwtToken(user);

            AuthModel authModel = new AuthModel();
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var newRefreshToken = await GenerateRefreshToken(authModel.Token, user.Id);

            authModel.IsAuthenticated = true;
            authModel.Email = user.Email;
            authModel.UserId = user.Id;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtToken.ValidTo;
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpiryDate;

            return new ObjectResult<AuthModel>(Meta.Success(), authModel);
        }

        public async Task<ObjectResult<AuthModel>> Register(RegisterUserCommand command)
        {
            var user = new AppUser { UserName = command.UserName, Email = command.Email };
            var result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
                return new ObjectResult<AuthModel>(Meta.Error());

            await _userManager.AddToRoleAsync(user, "User");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = "your_frontend_url/confirm-email?userId=" + user.Id + "&token=" + System.Web.HttpUtility.UrlEncode(token);


            return new ObjectResult<AuthModel>(Meta.Success());
        }
    }
}
