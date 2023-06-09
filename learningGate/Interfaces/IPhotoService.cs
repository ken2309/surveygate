﻿using CloudinaryDotNet.Actions;

namespace learningGate.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string publicUrl);
        Task<IFormFile> GetFormFileFromImageUrl(string imageUrl);
    }
}