using datingapp_api.Data.Entities;

namespace datingapp_api.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
