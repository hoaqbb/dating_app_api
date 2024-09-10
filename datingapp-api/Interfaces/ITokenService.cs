using datingapp_api.Data.Entities;

namespace datingapp_api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
