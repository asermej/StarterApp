namespace Platform.Domain;

public sealed partial class DomainFacade
{
    /// <summary>
    /// Uploads an image file
    /// </summary>
    /// <param name="request">The image upload request containing file data</param>
    /// <returns>The upload result with URL and metadata</returns>
    public async Task<ImageUploadResult> UploadImageAsync(ImageUploadRequest request)
    {
        return await ImageManager.UploadImageAsync(request);
    }

    /// <summary>
    /// Deletes an image file
    /// </summary>
    /// <param name="fileName">The filename to delete</param>
    public async Task DeleteImageAsync(string fileName)
    {
        await ImageManager.DeleteImageAsync(fileName);
    }
}

