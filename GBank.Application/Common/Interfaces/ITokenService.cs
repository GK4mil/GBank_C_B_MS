using GBank.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GBank.Infrastructure.Services
{
    public interface ITokenService
    {
        public Task<string> GenerateAccessToken(User user);
        public Task<(string, string)> GenerateRefreshToken(User user);
        public Task<IDictionary<string, object>> VerifyToken(string token);
    }
}