using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using learningGate.Helpers;
using learningGate.Interfaces;

namespace learningGate.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloundinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloundinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    // Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloundinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicUrl)
        {
            var publicId = publicUrl.Split('/').Last().Split('.')[0];
            var deleteParams = new DeletionParams(publicId);
            return await _cloundinary.DestroyAsync(deleteParams);
        }

        public async Task<IFormFile> GetFormFileFromImageUrl(string imageUrl)
        {
            using (var httpClient = new HttpClient())
            {
                // Download the image data
                var imageData = await httpClient.GetByteArrayAsync(imageUrl);

                // Create a memory stream from the image data
                using (var memoryStream = new MemoryStream(imageData))
                {
                    // Create an IFormFile from the memory stream
                    var fileName = Path.GetFileName(imageUrl);
                    return new FormFile(memoryStream, 0, imageData.Length, null, fileName);
                }
            }
        }
    }
}