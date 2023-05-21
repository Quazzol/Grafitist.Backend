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

    public async Task CalculateDiscountedPrice(IHasCalculatableAmount amount)
    {
        if (amount is null)
            return;

        var product = await _productRepository.Get(amount.ProductId);
        var campaigns = await _campaignRepository.Get(new Pager { No = 1, Count = 100, OnlyActive = true });
        foreach (var campaign in campaigns)
        {

        }
        SetAmount(amount, int.MaxValue, Guid.Empty);
    }

    public async Task CalculateDiscountedPrices(IEnumerable<IHasCalculatableAmount>? amounts)
    {
        if (amounts is null)
            return;

        var products = await _productRepository.Get(amounts.Select(q => q.ProductId).Distinct());
        var campaigns = await _campaignRepository.Get(new Pager { No = 1, Count = 100, OnlyActive = true });

        foreach (var product in products)
        {
            SetAmount(amounts.Where(q => q.ProductId == product.Id), product.Price, Guid.Empty);
            foreach (var campaign in campaigns)
            {
                if (campaign.Type == Enums.CampaignType.Cart || campaign.Type == Enums.CampaignType.FreeShipping)
                    continue;

                SetAmount(amounts.Where(q => q.ProductId == product.Id), FindDiscountedPrice(campaign, CampaignType.Category, product.Item!.CategoryId, product.Price), campaign.Id);
                SetAmount(amounts.Where(q => q.ProductId == product.Id), FindDiscountedPrice(campaign, CampaignType.Item, product.ItemId, product.Price), campaign.Id);
                SetAmount(amounts.Where(q => q.ProductId == product.Id), FindDiscountedPrice(campaign, CampaignType.Product, product.Id, product.Price), campaign.Id);
            }
        }
    }

    public async Task<double> CalculateTotalDiscountedPrice(double totalAmount)
    {
        var campaigns = (await _campaignRepository.Get(CampaignType.Cart)).Where(q => q.MinimumPrice <= totalAmount);
        if (campaigns is null)
            return totalAmount;
        return (100 - campaigns.Max(q => q.Percent)) * totalAmount;
    }

    private void SetAmount(IHasCalculatableAmount amount, double discountedPrice, Guid campaignId)
    {
        if (amount.Amount < discountedPrice)
            return;
        amount.Amount = discountedPrice;
        amount.CampaignId = campaignId;
    }

    private void SetAmount(IEnumerable<IHasCalculatableAmount> amounts, double discountedPrice, Guid campaignId)
    {
        foreach (var amount in amounts)
        {
            SetAmount(amount, discountedPrice, campaignId);
        }
    }

    private double FindDiscountedPrice(CampaignModel campaign, CampaignType type, int typeId, double price)
    {
        if (campaign.Type == type && campaign.CampaignTypeId == typeId && campaign.MinimumPrice <= price)
        {
            var calculatedPrice = (100 - campaign.Percent) * price;
            if (calculatedPrice < price)
                return calculatedPrice;
        }
        return price;
    }
}