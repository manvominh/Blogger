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
        private const int JWT_TOKEN_VALIDITY_MINS = 20;

        private IUnitOfWork _unitOfWork;

        public JwtAuthenticationManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<UserSession?> GenerateJwtToken(User user)
        {
            ///* Validating the User Credentials */
            if (user == null)
                return Task.FromResult<UserSession?>(null);
            List<string> roleNames = user.UserRoles.Select(s => s.Role.Name).ToList();
            /* Generating JWT Token */
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

			var claimEmailAddress = new Claim(ClaimTypes.Name, user.Email);
			var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id));
			var claimFirstName = new Claim("FirstName", user.FirstName);
			var claimLastName = new Claim("LastName", user.LastName);

			var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier, claimFirstName, claimLastName }, "jwtAuth");
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
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            try
            {
                //string abc = token.Replace("'", "");
                var key = Encoding.UTF8.GetBytes(JWT_SECURITY_KEY);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();
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
    }
}
