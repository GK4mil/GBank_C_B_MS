using GBank.Domain.Entities;
using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GBank.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secret = "Superlongsupersecret!";

        public async Task<string> GenerateAccessToken(User user)
        {
            return await Task.FromResult(new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(Encoding.ASCII.GetBytes(_secret))
                    .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds())
                    .AddClaim("username", user.Username)
                    .Issuer("GBank")
                    .Audience("access")
                    .Encode());
        }

        public async Task<(string, string)> GenerateRefreshToken(User user)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                Convert.ToBase64String(randomNumber);
            }

            var randomString = System.Text.Encoding.ASCII.GetString(randomNumber);

            string jwt = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(_secret)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(4).ToUnixTimeSeconds())
                .AddClaim("refresh", randomString)
                .AddClaim("username", user.Username)
                .Issuer("GBank")
                .Audience("refresh")
                .Encode();

            return await Task.FromResult((randomString, jwt));
        }

        public async Task<IDictionary<string, object>> VerifyToken(string token)
        {
            return await Task.FromResult(new JwtBuilder()
                 .WithSecret(_secret)
                 .MustVerifySignature()
                 .Decode<IDictionary<string, object>>(token));
        }

        public async Task<string> GetUsernameFromToken(string token)
        {
            token = token.Replace("Bearer ", String.Empty);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            return await Task.Run(()=>tokenS.Claims.First(claim => claim.Type == "username").Value);
        }
    }
}
