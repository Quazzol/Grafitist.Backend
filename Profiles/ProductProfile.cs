using AutoMapper;
using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;
using Grafitist.Models.Product;

namespace Grafitist.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Source -> Target
        CreateMap<CategoryModel, CategoryDTO>();
        CreateMap<CategoryInsertDTO, CategoryModel>();
        CreateMap<CategoryUpdateDTO, CategoryModel>();
        CreateMap<ColorModel, ColorDTO>();
        CreateMap<ColorInsertDTO, ColorModel>();
        CreateMap<ColorUpdateDTO, ColorModel>();
        CreateMap<ItemModel, ItemDTO>();
        CreateMap<ItemInsertDTO, ItemModel>();
        CreateMap<ItemUpdateDTO, ItemModel>();
        CreateMap<MaterialModel, MaterialDTO>();
        CreateMap<MaterialInsertDTO, MaterialModel>();
        CreateMap<MaterialUpdateDTO, MaterialModel>();
        CreateMap<VariantModel, VariantDTO>();
        CreateMap<VariantInsertDTO, VariantModel>();
        CreateMap<VariantUpdateDTO, VariantModel>();
        CreateMap<ImageModel, ImageDTO>();
        CreateMap<ImageInsertDTO, ImageModel>();
        CreateMap<ImageUpdateDTO, ImageModel>();
        CreateMap<ProductModel, ProductDTO>();
        CreateMap<ProductInsertDTO, ProductModel>();
        CreateMap<ProductUpdateDTO, ProductModel>();
    }
}