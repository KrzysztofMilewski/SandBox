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
    }
}
