namespace Grafitist.Misc.Interfaces;

public interface IImageManager
{
    public Task<(string, long)> SaveImage(IFormFile? image, string rootPath, int productId);
    public bool DeleteImage(string rootPath, string name);
}