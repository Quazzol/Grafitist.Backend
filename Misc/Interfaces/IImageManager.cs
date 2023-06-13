namespace Grafitist.Misc.Interfaces;

public interface IImageManager
{
    public Task<(string, byte[])?> ConvertImage(IFormFile? image, int productId);
}