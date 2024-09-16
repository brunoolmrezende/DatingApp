using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController(IUserRepository userRepository) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllUsers()
        {
            var users = await userRepository.GetMembersAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser([FromRoute] string username)
        {
            var user = await userRepository.GetMemberAsync(username);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
