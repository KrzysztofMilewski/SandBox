using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.Dtos;
using Microsoft.AspNet.Identity;
using SandBox.Infrastructure;
using System.Web.Http;

namespace SandBox.Controllers.Api
{
    public class EmailMessagesController : ApiController
    {
        private readonly IEmailMessageService _messageService;

        public EmailMessagesController(IEmailMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public IHttpActionResult SendEmail(EmailMessageDto messageDto)
        {
            messageDto.Sender = new ApplicationUserDto() { Id = User.Identity.GetUserId() };

            var result = _messageService.SendMessage(messageDto);

            return this.ReturnHttpResponse(result);
        }

        [HttpGet]
        public IHttpActionResult GetIncomingMessages()
        {
            var userId = User.Identity.GetUserId();

            var result = _messageService.GetIncomingMessages(userId);

            return this.ReturnHttpResponse(result);
        }

        [HttpGet]
        public IHttpActionResult GetOutcomingMessages()
        {
            var userId = User.Identity.GetUserId();

            var result = _messageService.GetOutcomingMessages(userId);

            return this.ReturnHttpResponse(result);
        }

        [HttpGet]
        public IHttpActionResult GetMessage(int id)
        {
            var userId = User.Identity.GetUserId();

            var result = _messageService.GetMessage(id, userId);

            return this.ReturnHttpResponse(result);
        }

        [HttpGet]
        public IHttpActionResult GetNumberOfUnreadMessages()
        {
            var userId = User.Identity.GetUserId();

            var result = _messageService.GetNumberOfUnreadMessages(userId);

            return this.ReturnHttpResponse(result);
        }
    }
}
