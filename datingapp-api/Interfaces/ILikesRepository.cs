using datingapp_api.Data.Entities;
using datingapp_api.DTOs;
using datingapp_api.Helpers;

namespace datingapp_api.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike>  GetUserLike(int sourceUserId, int likedUserId);
        Task<User> GetUserWithLikes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}
