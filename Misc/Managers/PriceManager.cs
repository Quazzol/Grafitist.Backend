using Grafitist.Misc.Enums;
using Grafitist.Misc.Interfaces;
using Grafitist.Models.Campaign;
using Grafitist.Repositories.Campaign.Interfaces;
using Grafitist.Repositories.Product.Interfaces;

namespace Grafitist.Misc.Managers;

public class PriceManager : IPriceManager
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IProductRepository _productRepository;

    public PriceManager(ICampaignRepository campaignRepository, IProductRepository productRepository)
    {
        _campaignRepository = campaignRepository;
        _productRepository = productRepository;
    }

    public async Task SetDiscountedPrice(ITransactionLine transactionLine)
    {
        if (transactionLine is null)
            return;

        var product = await _productRepository.Get(transactionLine.ProductId);
        if (product is null)
            return;

        var campaigns = await _campaignRepository.Get(new Pager { No = 1, Count = 100, OnlyActive = true });
        SetDiscountedPrice(transactionLine, product.Price);
        SetCampaign(transactionLine, Guid.Empty);

        foreach (var campaign in campaigns)
        {
            if (campaign.Type == CampaignType.Category)
            {
                SetDiscountedPrice(transactionLine, GetDiscountedPrice(campaign, CampaignType.Category, product.CategoryId, product.Price));
                SetCampaign(transactionLine, campaign.Id);
            }
            else if (campaign.Type == CampaignType.Product)
            {
                SetDiscountedPrice(transactionLine, GetDiscountedPrice(campaign, CampaignType.Product, product.Id, product.Price));
                SetCampaign(transactionLine, campaign.Id);
            }
        }
    }

    public async Task SetDiscountedPrice(IEnumerable<ITransactionLine>? transactionLines)
    {
        if (transactionLines is null)
            return;

        var products = await _productRepository.Get(transactionLines.Select(q => q.ProductId).Distinct());
        var campaigns = await _campaignRepository.Get(new Pager { No = 1, Count = 100, OnlyActive = true });

        foreach (var product in products)
        {
            var filteredLines = transactionLines.Where(q => q.ProductId == product.Id).ToList();
            SetDiscountedPrice(filteredLines, product.Price);
            SetCampaign(filteredLines, Guid.Empty);

            foreach (var campaign in campaigns)
            {
                if (campaign.Type == CampaignType.Category)
                {
                    SetDiscountedPrice(filteredLines, GetDiscountedPrice(campaign, CampaignType.Category, product.CategoryId, product.Price));
                    SetCampaign(filteredLines, campaign.Id);
                }
                else if (campaign.Type == CampaignType.Product)
                {
                    SetDiscountedPrice(filteredLines, GetDiscountedPrice(campaign, CampaignType.Product, product.Id, product.Price));
                    SetCampaign(filteredLines, campaign.Id);
                }
            }
        }
    }

    public async Task SetDiscountedPrice(IProduct product)
    {
        if (product is null)
            return;

        var loadedProduct = await _productRepository.Get(product.ProductId);
        if (loadedProduct is null)
            return;

        var campaigns = await _campaignRepository.Get(new Pager { No = 1, Count = 100, OnlyActive = true });
        SetDiscountedPrice(product, loadedProduct.Price);

        foreach (var campaign in campaigns)
        {
            if (campaign.Type == CampaignType.Category)
            {
                SetDiscountedPrice(product, GetDiscountedPrice(campaign, CampaignType.Category, loadedProduct.CategoryId, product.Price));
            }
            else if (campaign.Type == CampaignType.Product)
            {
                SetDiscountedPrice(product, GetDiscountedPrice(campaign, CampaignType.Product, loadedProduct.Id, product.Price));
            }
        }
    }

    public async Task SetDiscountedPrice(IEnumerable<IProduct>? products)
    {
        if (products is null)
            return;

        var loadedProducts = await _productRepository.Get(products.Select(q => q.ProductId).Distinct());
        var campaigns = await _campaignRepository.Get(new Pager { No = 1, Count = 100, OnlyActive = true });

        foreach (var product in loadedProducts)
        {
            var filteredLines = products.Where(q => q.ProductId == product.Id).ToList();
            SetDiscountedPrice(filteredLines, product.Price);

            foreach (var campaign in campaigns)
            {
                if (campaign.Type == CampaignType.Category)
                {
                    SetDiscountedPrice(filteredLines, GetDiscountedPrice(campaign, CampaignType.Category, product.CategoryId, product.Price));
                }
                else if (campaign.Type == CampaignType.Product)
                {
                    SetDiscountedPrice(filteredLines, GetDiscountedPrice(campaign, CampaignType.Product, product.Id, product.Price));
                }
            }
        }
    }

    public async Task<double> CalculateTotalDiscountedPrice(double totalAmount)
    {
        var campaigns = (await _campaignRepository.Get(CampaignType.Cart)).Where(q => q.MinimumPrice <= totalAmount);
        if (campaigns is null)
            return totalAmount;
        return (100 - campaigns.Max(q => q.Percent)) * totalAmount / 100;
    }

    private void SetDiscountedPrice(IProduct product, double discountedPrice)
    {
        if (product.DiscountedPrice < discountedPrice)
            return;
        product.DiscountedPrice = discountedPrice;
    }

    private void SetDiscountedPrice(IEnumerable<IProduct> products, double discountedPrice)
    {
        foreach (var product in products)
        {
            SetDiscountedPrice(product, discountedPrice);
        }
    }

    private void SetCampaign(ITransactionLine transactionLine, Guid campaignId)
    {
        transactionLine.CampaignId = campaignId;
    }

    private void SetCampaign(IEnumerable<ITransactionLine> transactionLines, Guid campaignId)
    {
        foreach (var line in transactionLines)
        {
            SetCampaign(line, campaignId);
        }
    }

    private double GetDiscountedPrice(CampaignModel campaign, CampaignType type, int typeId, double price)
    {
        if (campaign.Type == type && campaign.CampaignTypeId == typeId && campaign.MinimumPrice <= price)
        {
            var calculatedPrice = (100 - campaign.Percent) * price / 100;
            if (calculatedPrice < price)
                return calculatedPrice;
        }
        return price;
    }
}