namespace GBankAdminService.Application.Common.Interfaces
{
    public interface IPasswordHashService
    {
        string Hash(string password);
        string Hash(string password, int iterations);
        bool Verify(string password, string hashedPassword);
    }
}