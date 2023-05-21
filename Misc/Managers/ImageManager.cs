using Grafitist.Misc.Interfaces;

namespace Grafitist.Misc.Managers;

public class ImageManager : IImageManager
{
    private readonly string _folderPath;

    public ImageManager(IConfiguration config)
    {
        _folderPath = config["ImageLocation"] ?? string.Empty;
    }

    public async Task<(string, long)> SaveImage(IFormFile? image, string rootPath, int productId)
    {
        if (image is null || image.Length == 0)
            return (string.Empty, 0);

        string name = $"{GetRandomFileName()}{Path.GetExtension(image.FileName)}";

        Console.WriteLine(name);
        Console.WriteLine(rootPath);

        CreateIfNotExist(Path.Combine(rootPath, _folderPath));
        CreateIfNotExist(Path.Combine(rootPath, _folderPath, productId.ToString()));

        var filePath = Path.Combine(rootPath, _folderPath, productId.ToString(), name);

        using (var stream = File.Create(filePath))
        {
            await image.CopyToAsync(stream);
        }

        return (Path.Combine(_folderPath, productId.ToString(), name), image.Length);
    }

    public bool DeleteImage(string rootPath, string name)
    {
        var filePath = Path.Combine(rootPath, name);

        if (!File.Exists(filePath))
            return false;
        File.Delete(filePath);
        return true;
    }

    private void CreateIfNotExist(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    private string GetRandomFileName()
    {
        return DateTime.Now.ToFileTimeUtc().ToString();
    }
}