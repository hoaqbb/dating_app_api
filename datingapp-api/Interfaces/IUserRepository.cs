using datingapp_api.Data.Entities;
using datingapp_api.DTOs;
using datingapp_api.Helpers;

namespace datingapp_api.Interfaces
{
    public interface IUserRepository
    {
        void Update(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<MemberDto> GetMemberByUsernameAsync(string username);
        Task<string> GetUserGender(string username);
    }
}
