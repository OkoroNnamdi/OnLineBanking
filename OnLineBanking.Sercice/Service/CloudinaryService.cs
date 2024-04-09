using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using OnLineBanking.Core;
using OnLineBanking.Sercice.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Service.Service
{
    public class CloudinaryService : ICloudinaryService
    {
        private Cloudinary _Cloudinary;
        private readonly OnlineBankDBContext _context;
        public CloudinaryService(Cloudinary  cloudinary,OnlineBankDBContext context)
        {
           _Cloudinary = cloudinary;
            _context = context;
        }
        public async Task<string> UpdateManagerPhotosAsync(string userId, IFormFile[] images)
        {
            if (images == null || images.Length == 0)
            {
                return "no image";
            }
            
            var user = await _context.Managers.Include(man => man.AppUser).Where(x => x.AppUser.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return "Couldnt find user";
            }
            await _Cloudinary.DeleteResourcesAsync(user.AppUser.Avatar);
            string avatar = "";
            foreach (var image in images)
            {
                var result = await _Cloudinary.UploadAsync(new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream())
                }).ConfigureAwait(false);
                avatar += result.Url;
            }
            if (avatar.Length == 0) return "Failed to upload";

            user.AppUser.Avatar = avatar;
            _context.Managers.Update(user);
            await _context.SaveChangesAsync();
            return "Updated";
        }

         public async Task<string> UpdateUserPhotosAsync(string userId, IFormFile[] images)
        {
            if (images == null || images.Length == 0)
            {
                return "no image";
            }
            var user = await _context.Customers.Include(cust => cust.AppUser).Where(x => x.AppUser.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return "Couldnt find user";
            }
            await _Cloudinary.DeleteResourcesAsync(user.AppUser.Avatar);

            string avatar = "";
            foreach (var image in images)
            {
                var result = await _Cloudinary.UploadAsync(new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream())
                }).ConfigureAwait(false);
                avatar += result.Url;
            }
            if (avatar.Length == 0) return "Failed to upload";

            user.AppUser.Avatar = avatar;
            _context.Customers.Update(user);
            await _context.SaveChangesAsync();
            return "Done";
        }

        public async Task<List<Dictionary<string, string>>> UploadAsync(IFormFile[] images)
        {
             var results = new List<Dictionary<string, string>>();

            if (images == null || images.Length == 0)
            {
                return null;
            }
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
            foreach (var image in images)
            {
                if (image.Length == 0)
                {
                    return null;
                }
                var result = await _Cloudinary.UploadAsync(new ImageUploadParams
                {
                    File = new FileDescription(image.FileName,
                       image.OpenReadStream()),
                }).ConfigureAwait(false);

                var imageProperties = new Dictionary<string, string>();
                foreach (var token in result.JsonObj.Children())
                {
                    if (token is JProperty prop)
                    {
                        imageProperties.Add(prop.Name, prop.Value.ToString());
                    }
                }

                results.Add(imageProperties);

            }

            return results;
        }

    }
    }

