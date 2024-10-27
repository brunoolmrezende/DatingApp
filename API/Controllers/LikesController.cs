using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LikesController(ILikesRepository likesRepository) : BaseApiController
    {
        [HttpPost("{targetUserId:int}")]
        public async Task<ActionResult> ToggleLike(int targetUserId)
        {
            var sourceUserId = User.GetUserId();

            if (sourceUserId == targetUserId) BadRequest("You cannot like yourself");

            var existingLike = await likesRepository.GetUserLike(sourceUserId, targetUserId);

            if (existingLike == null)
            {
                var newLike = new UserLike
                {
                    SourceUserId = sourceUserId,
                    TargetUserId = targetUserId,
                };

                likesRepository.Add(newLike);
            }
            else
            {
                likesRepository.Delete(existingLike);
            }

            if (await likesRepository.SaveChanges()) return Ok();

            return BadRequest("Failed to update like");
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<int>>> GetCurrentUserLikesId()
        {
            return Ok(await likesRepository.GetCurrentUserLikesId(User.GetUserId()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUserLikes([FromQuery] LikeParams likeParams)
        {
            likeParams.UserId = User.GetUserId();

            var users = await likesRepository.GetUserLikes(likeParams);

            Response.AddPaginationHeader(users);

            return Ok(users);
        }

    }
}
