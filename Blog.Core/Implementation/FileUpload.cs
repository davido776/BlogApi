using Blog.Core.Abstraction;
using Blog.Models.Dtos.Response;
using Blog.Models.settings;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Implementation
{
    public class FileUpload : IFileUpload
    {
        private readonly CloudinaryConfig _config;
        private readonly Cloudinary _cloudinary;

        public FileUpload(IOptions<CloudinaryConfig> config)
        {
            _config = config.Value;
            Account account = new Account(
                _config.CloudName,
                _config.ApiKey,
                _config.ApiSecret
             );

            _cloudinary = new Cloudinary(account);
        }


        public UploadPhotoRes UploadPhoto(IFormFile file) 
        {

            var imageUploadResult = new ImageUploadResult();
            using (var fs = file.OpenReadStream())
            {
                var imageUploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, fs),
                    Transformation = new Transformation().Width(300).Height(300)
                .Crop("fill").Gravity("face")
                };
                imageUploadResult = _cloudinary.Upload(imageUploadParams);
            }
            var publicId = imageUploadResult.PublicId;
            var avatarUrl = imageUploadResult.Url.ToString();
            var result = new UploadPhotoRes
            {
                PublicId = publicId,
                AvatarUrl = avatarUrl
            };
            return result;
        }
    }
}
