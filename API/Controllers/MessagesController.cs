using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class MessagesController(IUserRepository userRepository, IMessageRepository messageRepository, IMapper mapper) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUserName();

            if (username == createMessageDto.RecipientUsername.ToLower()) return BadRequest("Cannot send message to yourself");

            var sender = await userRepository.GetUserByUsernameAsync(username);
            var recipient = await userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername.ToLower());

            if (sender == null || recipient == null) return BadRequest("Cannot send message at this time");

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = username,
                RecipientUsername = createMessageDto.RecipientUsername.ToLower(),
                Content = createMessageDto.Content,
            };

            messageRepository.AddMessage(message);

            if (await messageRepository.SaveAllAssync()) return Created(string.Empty, mapper.Map<MessageDto>(message));

            return BadRequest("Failed to save message");

        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUserName();

            var messages = await messageRepository.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(messages);

            return Ok(messages);
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesThread(string username)
        {
            var currentUsername = User.GetUserName();

            return Ok(await messageRepository.GetMessagesThread(currentUsername, username));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var username = User.GetUserName();

            var message = await messageRepository.GetMessage(id);

            if (message == null) return BadRequest("Cannot delete this message");

            if (message.SenderUsername != username && message.RecipientUsername != username) return Forbid();

            if (message.SenderUsername == username) message.SenderDeletd = true;
            if (message.RecipientUsername == username) message.RecipientDeletd = true;

            if (message is { SenderDeletd: true, RecipientDeletd: true })
            {
                messageRepository.DeleteMessage(message);
            }

            if (await messageRepository.SaveAllAssync()) return Ok();

            return BadRequest("Problem deleting tthe message");
        }
    }
}
