using Infrastructure.Configuration;
using System.Web.Http;

namespace SandBox.Controllers.Api
{
    public class UsersController : ApiController
    {
        private readonly ApplicationUserManager _applicationUserManager;

        public UsersController(ApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        [HttpGet]
        public IHttpActionResult GetUsers(string nameQuery)
        {
            var result = _applicationUserManager.FindUsersByNicknames(nameQuery);
            return Ok(result);
        }
    }
}
