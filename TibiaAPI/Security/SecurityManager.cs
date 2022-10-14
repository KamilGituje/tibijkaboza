using DB1;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TibiaAPI.Security;
using TibiaModels.BL;
using TibiaModels.BL.Security;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TibiaRepositories.BL
{
    public class SecurityManager
    {
        public SecurityManager(PubContext _context, JwtSettings _jwtSettings)
        {
            context = _context;
            jwtSettings = _jwtSettings;
        }
        private readonly PubContext context;
        private readonly JwtSettings jwtSettings;

        public async Task<List<UserClaim>> GetUserClaimsAsync(Guid userId)
        {
            var userClaims = (await context.Users.Include(u => u.UserClaims).FirstOrDefaultAsync(u => u.UserId == userId)).UserClaims.ToList();
            var allClaims = await context.UserClaims.ToListAsync();
            foreach(var claim in allClaims)
            {
                if (userClaims.Contains(claim))
                {
                    claim.ClaimValue = true;
                }
            }
            return allClaims;
        }

        public async Task<string> ValidateUserAsync(string userName, string password)
        {
            var token = string.Empty;
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName.ToLower() && u.Password == password);
            if(user != null)
            {
                var claims = await GetUserClaimsAsync(user.UserId);
                token = BuildJwtToken(claims, user.UserName, user.UserId);
            }

            return token;
        }

        protected string BuildJwtToken(List<UserClaim> claims, string userName, Guid userId)
        {
            var jwtClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userName", userName)
            };
            foreach(var claim in claims)
            {
                jwtClaims.Add(new Claim(claim.ClaimType, Convert.ToString(claim.ClaimValue)));
            }
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: jwtClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(jwtSettings.MinutesToExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)), SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<User> CreateUserAsync(User user)
        {
            if (await IsUserValidAsync(user))
            {
                user = await AddClaimsAsync(user);
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                user.Password = string.Empty;
            }
            else
            {
                user = null;
            }
            return user;
        }
        public async Task<bool> IsUserValidAsync(User user)
        {
            if(!string.IsNullOrWhiteSpace(user.UserName) && !string.IsNullOrWhiteSpace(user.Password))
            {
                if (!await context.Users.AnyAsync(u => u.UserName == user.UserName))
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<User> AddClaimsAsync(User user)
        {
            var claims = await context.UserClaims.ToListAsync();
            user.UserClaims = claims;
            return user;
        }
    }
}
