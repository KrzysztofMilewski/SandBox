using Infrastructure.Dtos;
using System.Web.Http;
using System.Web.Http.Results;

namespace SandBox.Infrastructure
{
    public static class ApiControllerExtensions
    {
        public static IHttpActionResult HandleServiceError(this ApiController controller, ResultDto resultDto)
        {
            if (resultDto.RequestStatus == RequestStatus.Error || resultDto.RequestStatus == RequestStatus.NotAuthorized)
                return new BadRequestErrorMessageResult(resultDto.Message, controller);
            else
                return new NotFoundResult(controller);
        }
    }
}