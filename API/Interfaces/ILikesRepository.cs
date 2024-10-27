using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike?> GetUserLike(int sourceUserId, int targetUserId);
        Task<PagedList<MemberDto>> GetUserLikes(LikeParams likeParams);
        Task<IEnumerable<int>> GetCurrentUserLikesId(int currentUserId);
        void Delete(UserLike like);
        void Add(UserLike like);
        Task<bool> SaveChanges();
    }
}
