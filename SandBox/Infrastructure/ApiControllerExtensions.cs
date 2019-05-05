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
            else if (resultDto.RequestStatus == RequestStatus.NotFound)
                return new NotFoundResult(controller);
            else
                return new OkNegotiatedContentResult<string>(resultDto.Message, controller);
        }

        public static IHttpActionResult HandleServiceError<T>(this ApiController controller, ResultDto<T> resultDto)
        {
            if (resultDto.RequestStatus == RequestStatus.Error || resultDto.RequestStatus == RequestStatus.NotAuthorized)
                return new BadRequestErrorMessageResult(resultDto.Message, controller);
            else if (resultDto.RequestStatus == RequestStatus.NotFound)
                return new NotFoundResult(controller);
            else
                return new OkNegotiatedContentResult<T>(resultDto.Data, controller);
        }
    }
}