using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.BusinessLogic.Services;
using Infrastructure.DataAccess.Interfaces;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.Models;
using Infrastructure.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;

namespace SandBox.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<DbContext>().To<ApplicationDbContext>();

            _kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();

            _kernel.Bind<ICommentRepository>().To<CommentRepository>();
            _kernel.Bind<IPostRepository>().To<PostRepository>();
            _kernel.Bind<ISubscriptionRepository>().To<SubscriptionRepository>();
            _kernel.Bind<IEmailMessageRepository>().To<EmailMessageRepository>();

            _kernel.Bind<ICommentService>().To<CommentService>();
            _kernel.Bind<IPostService>().To<PostService>();
            _kernel.Bind<ISubscriptionService>().To<SubscriptionService>();
            _kernel.Bind<IEmailMessageService>().To<EmailMessageService>();
        }
    }
}