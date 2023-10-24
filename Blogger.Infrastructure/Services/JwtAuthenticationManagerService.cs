using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blogger.Infrastructure.Services
{
    public class JwtAuthenticationManagerService : IJwtAuthenticationManagerService
    {
        public const string JWT_SECURITY_KEY = "yPkCqn4kSWLtaJwXvN2jGzpQRyTZ3gdXkt7FeBJP";
        public const int JWT_TOKEN_VALIDITY_MINS = 20;

        public Task<UserSession?> GenerateJwtToken(User user)
        {
            ///* Validating the User Credentials */
            if (user == null)
                return Task.FromResult<UserSession?>(null);
            List<string> roleNames = user.UserRoles.Select(s => s.Role.Name).ToList();
            /* Generating JWT Token */
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
			var expiryTimeStamp = new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS).ToString());

			var claimEmailAddress = new Claim(ClaimTypes.Email, user.Email);
			var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id));
			var claimFirstName = new Claim("FirstName", user.FirstName);
			var claimLastName = new Claim("LastName", user.LastName);

			var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier, claimFirstName, claimLastName, expiryTimeStamp }, "jwtAuth");
            foreach (var role in user.UserRoles)
            {
                var claim = new Claim(ClaimTypes.Role, role.Role.Name);
                claimsIdentity.AddClaim(claim);
            }
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials,
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            /* Returning the User Session object */
            var userSession = new UserSession
            {
                FullName = $"{user.FirstName} {user.LastName}",
                RoleNames = roleNames,
                Token = token,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };
            return Task.FromResult<UserSession?>(userSession);
        }
        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            try
            {
                var key = Encoding.UTF8.GetBytes(JWT_SECURITY_KEY);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();
				var jwt = tokenHandler.ReadJwtToken(token);
				var expiredTime = jwt.Claims.First(c => c.Type == ClaimTypes.Expiration).Value;
                if(DateTime.Parse(expiredTime) < DateTime.Now)
                {
                    return null;
                }
				SecurityToken securityToken;
                //validating the token
                var principle = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                return principle;
            }
            catch (Exception ex)
            {
                //logging the error and returning null
                Console.WriteLine("Exception : " + ex.Message);
                return null;
            }
			
		}
		public static long GetTokenExpirationTime(string token)
		{
			var handler = new JwtSecurityTokenHandler();
			var jwtSecurityToken = handler.ReadJwtToken(token);
			var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
			var ticks = long.Parse(tokenExp);
			return ticks;
		}

		public static bool CheckTokenIsValid(string token)
		{
			var tokenTicks = GetTokenExpirationTime(token);
			var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;

			var now = DateTime.Now.ToUniversalTime();

			var valid = tokenDate >= now;

			return valid;
		}
	}
}
