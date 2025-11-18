namespace Platform.Domain.DataLayer;

/// <summary>
/// Data manager responsible for image file storage operations
/// </summary>
internal class ImageDataManager
{
    private readonly string _storagePath;
    private readonly string _baseUrl;

    public ImageDataManager(ConfigurationProviderBase configurationProvider)
    {
        _storagePath = configurationProvider.GetImageStoragePath();
        _baseUrl = configurationProvider.GetImageBaseUrl();
    }

    /// <summary>
    /// Saves an image file to the file system
    /// </summary>
    /// <param name="request">The image upload request containing file data</param>
    /// <returns>The result containing the URL and storage information</returns>
    public async Task<ImageUploadResult> SaveImageAsync(ImageUploadRequest request)
    {
        try
        {
            // Generate unique filename
            var extension = Path.GetExtension(request.FileName).ToLowerInvariant();
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(_storagePath, uniqueFileName);

            // Ensure directory exists
            var directory = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Save file to disk
            using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await request.FileStream.CopyToAsync(fileStream);
            }

            // Construct URL
            var url = $"{_baseUrl}/{uniqueFileName}";

            return new ImageUploadResult
            {
                Url = url,
                StoredFileName = uniqueFileName,
                UploadedAt = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            throw new ImageUploadException($"Failed to save image file: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes an image file from the file system
    /// </summary>
    /// <param name="fileName">The filename to delete</param>
    public async Task DeleteImageAsync(string fileName)
    {
        try
        {
            var fullPath = Path.Combine(_storagePath, fileName);
            
            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath));
            }
        }
        catch (Exception ex)
        {
            throw new ImageUploadException($"Failed to delete image file: {ex.Message}", ex);
        }
    }
}

