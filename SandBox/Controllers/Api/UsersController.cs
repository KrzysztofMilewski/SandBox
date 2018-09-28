using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SandBox.Dtos;
using SandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SandBox.Controllers.Api
{
    [Authorize]
    public class UsersController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public UsersController()
        {
            _context = ApplicationDbContext.Create();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        public IHttpActionResult GetCurrentUser()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(currentUserId);
            var userDto = new UserDto()
            {
                Id = currentUserId,
                Nickname = currentUser.Nickname
            };
            return Ok(userDto);
        }
    }
}
