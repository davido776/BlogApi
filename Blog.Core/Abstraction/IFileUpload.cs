using Blog.Models.Dtos.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Abstraction
{
    public interface IFileUpload
    {
        UploadPhotoRes UploadPhoto(IFormFile file);
        
    }
}
