using AutoMapper;
using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;
using Grafitist.Misc.Interfaces;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Grafitist.Services.Product.Interfaces;

namespace Grafitist.Services.Product;

public class ImageService : IImageService
{
    private readonly IImageManager _imageManager;
    private readonly IImageRepository _repository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _environment;

    public ImageService(IImageManager imageManager, IImageRepository repository, IMapper mapper, IWebHostEnvironment environment)
    {
        _imageManager = imageManager;
        _repository = repository;
        _mapper = mapper;
        _environment = environment;
    }

    public async Task<ImageDTO?> Get(int id)
    {
        return _mapper.Map<ImageDTO?>(await _repository.Get(id));
    }

    public async Task<IEnumerable<ImageDTO?>> GetByProduct(int productId)
    {
        return _mapper.Map<IEnumerable<ImageDTO?>>(await _repository.GetByProduct(productId));
    }

    public async Task<ImageDTO?> Insert(ImageInsertDTO image)
    {
        var result = await _imageManager.SaveImage(image.Image, RootPath(), image.ProductId);
        if (result.Item1 is null || result.Item1.ToString() == string.Empty || result.Item2 == 0)
            return null;
        return _mapper.Map<ImageDTO>(await _repository.Insert(_mapper.Map<ImageModel>(image), result.Item1));
    }

    public async Task<ImageDTO?> Update(ImageUpdateDTO image)
    {
        return _mapper.Map<ImageDTO>(await _repository.Update(_mapper.Map<ImageModel>(image)));
    }

    public async Task Delete(int id)
    {
        var image = await Get(id);
        if (image is null || image.Name is null)
            return;

        _imageManager.DeleteImage(_environment.WebRootPath, image.Name);
        await _repository.Delete(id);
    }

    private string RootPath()
    {
        string path = _environment.WebRootPath;
        if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }
        return path;
    }
}