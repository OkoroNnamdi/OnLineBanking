using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Sercice.Interfaces
{
    public  interface ICloudinaryService
    {
        Task<List<Dictionary<string, string>>> UploadAsync(IFormFile[] images);
        Task<string> UpdateUserPhotosAsync(string userId, IFormFile[] images);
        Task<string> UpdateManagerPhotosAsync(string userId, IFormFile[] images);
    }
}
