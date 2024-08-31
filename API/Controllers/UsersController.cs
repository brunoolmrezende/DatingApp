using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController(DataContext context) : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            var users = await context.Users.ToListAsync();

            return Ok(users);
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<AppUser>> GetUserById([FromRoute] int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null)
                return NoContent();

            return Ok(user);
        }
    }
}
