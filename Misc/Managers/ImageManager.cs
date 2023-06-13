using Grafitist.Misc.Interfaces;

namespace Grafitist.Misc.Managers;

public class ImageManager : IImageManager
{
    public async Task<(string, byte[])?> ConvertImage(IFormFile? image, int productId)
    {
        if (image is null || image.Length == 0)
            return null;

        using (var stream = new MemoryStream())
        {
            await image.CopyToAsync(stream);
            return (Path.GetExtension(image.FileName).Trim('.'), stream.ToArray());
        }
    }

    private string GetRandomFileName()
    {
        return DateTime.Now.ToFileTimeUtc().ToString();
    }
}