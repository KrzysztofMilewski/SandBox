using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.Dtos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SandBox.ViewModels;
using System.Web;
using System.Web.Mvc;

namespace SandBox.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        public ActionResult NewPost()
        {
            var viewModel = new PostFormViewModel()
            {
                ActionName = "Create",
                PageHeading = "Add new post"
            };

            return View("PostForm", viewModel);
        }

        public ActionResult EditPost(int id)
        {
            var result = _postService.GetSinglePost(id, User.Identity.GetUserId());

            if (result.RequestStatus != RequestStatus.Success)
                return new EmptyResult();

            var post = result.Data;

            var viewModel = new PostFormViewModel()
            {
                Contents = post.Contents,
                Id = post.Id,
                Title = post.Title,
                ActionName = "Edit",
                PageHeading = "Edit your post",
                PubliclyVisible = post.PubliclyVisible

            };

            return View("PostForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("PostForm", viewModel);

            var currentUserId = User.Identity.GetUserId();
            var postDto = new PostDto()
            {
                Contents = viewModel.Contents,
                Title = viewModel.Title,
                Publisher = new ApplicationUserDto() { Id = currentUserId },
                PubliclyVisible = viewModel.PubliclyVisible
            };

            var result = _postService.CreatePost(postDto);

            if (result.RequestStatus != RequestStatus.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View("PostForm");
            }
            else
                return RedirectToAction("MyPosts");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("PostForm", viewModel);

            var currentUserId = User.Identity.GetUserId();

            var postDto = new PostDto()
            {
                Contents = viewModel.Contents,
                Title = viewModel.Title,
                Id = viewModel.Id,
                Publisher = new ApplicationUserDto() { Id = currentUserId },
                PubliclyVisible = viewModel.PubliclyVisible
            };

            var result = _postService.EditPost(postDto);

            if (result.RequestStatus != RequestStatus.Success)
            {
                viewModel.ErrorMessage = result.Message;
                return View("PostForm");
            }
            else
                return RedirectToAction("MyPosts");
        }

        public ActionResult MyPosts()
        {
            var currentUser = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());

            ViewBag.Id = currentUser.Id;
            ViewBag.Nickname = currentUser.Nickname;

            return View();
        }
    }
}