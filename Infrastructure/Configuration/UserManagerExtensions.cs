using Infrastructure.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Configuration
{
    public static class UserManagerExtensions
    {
        public static async Task<IdentityResult> AddProfileImageAsync<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, HttpPostedFileBase image)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (image == null)
                return new IdentityResult("No image specified");

            var user = await manager.FindByIdAsync(userId) as ApplicationUser;
            user.ImageMimeType = image.ContentType;
            user.ImageData = new byte[image.ContentLength];

            image.InputStream.Read(user.ImageData, 0, image.ContentLength);

            manager.Update(user as TUser);

            return IdentityResult.Success;
        }

        public static async Task<byte[]> GetProfileImageAsync<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            var user = await manager.FindByIdAsync(userId) as ApplicationUser;

            return user.ImageData ?? new byte[0];
        }
    }
}
