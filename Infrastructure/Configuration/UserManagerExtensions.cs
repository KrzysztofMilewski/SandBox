using AutoMapper;
using Infrastructure.Dtos;
using Infrastructure.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public static class UserManagerExtensions
    {
        public static async Task<IdentityResult> AddProfileImageAsync<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string image)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (image == null)
                return new IdentityResult("No image specified");

            var user = await manager.FindByIdAsync(userId) as ApplicationUser;

            var colonIndex = image.IndexOf("image/");
            var semicolonIndex = image.IndexOf(';', colonIndex);
            var imageType = image.Substring(colonIndex, semicolonIndex - colonIndex);

            var commaIndex = image.IndexOf(",");
            var imageData = image.Substring(commaIndex + 1);


            user.ImageMimeType = imageType;
            user.ImageData = Convert.FromBase64String(imageData);

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

        public static async Task<ApplicationUserDto> GetUserDtoAsync<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            var user = await manager.FindByIdAsync(userId);
            return Mapper.Map<ApplicationUserDto>(user);
        }

        public static IEnumerable<ApplicationUserDto> FindUsersByNicknames<TUser, TKey>(this UserManager<TUser, TKey> manager, string nameQuery)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            var validUsers = ((IQueryable<ApplicationUser>)manager.Users).Where(u => u.Nickname.Contains(nameQuery));
            return Mapper.Map<IEnumerable<ApplicationUserDto>>(validUsers);
        }
    }
}
