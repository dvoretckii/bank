using Shops.Exceptions;

namespace Shops.Entities;

public class Shop
{
    private Guid _id;
    private string _name;
    private string _adress;
    private ProductPack _productPack;

    public Shop()
    {
        _name = string.Empty;
        _adress = string.Empty;
        _id = Guid.Empty;
        _productPack = new ProductPack();
    }

    public Shop(string name, string adress, Guid id)
    {
        _name = name;
        _adress = adress;
        _id = id;
        _productPack = new ProductPack();
    }

    public string GetName()
    {
        return _name;
    }

    public Guid GetId()
    {
        return _id;
    }

    public ProductPack GetProductPack()
    {
        return _productPack;
    }

    public void SetShopProductPack(ProductPack productPack)
    {
        _productPack = productPack;
    }

    public void PutProduct(Product product, int count)
    {
        if (count < 0)
        {
            throw new CountException("Count must be > 0");
        }

        if (!_productPack.ProductExists(product.GetId()))
        {
            _productPack.AddProduct(product, count);
        }
        else
        {
            Dictionary<Guid, int> newCount = _productPack.GetCount();
            newCount[product.GetId()] += count;
            _productPack.SetCount(newCount);
        }
    }

    public void RemoveProduct(Product product, int count)
    {
        if (count < 0)
        {
            throw new CountException("Count must be > 0");
        }

        if (!_productPack.ProductExists(product.GetId()))
        {
            throw new ProductDoesNotExist("Product does not exist in shop");
        }

        Dictionary<Guid, int> newCount = _productPack.GetCount();
        newCount[product.GetId()] -= count;
        _productPack.SetCount(newCount);
    }

    public void PutProducts(Dictionary<Guid, Product> products, Dictionary<Guid, int> counts)
    {
        if (products.Count != counts.Count || !products.Keys.SequenceEqual(counts.Keys))
        {
            throw new IdException("Wrong pair of product and count");
        }

        foreach (KeyValuePair<Guid, Product> product in products)
        {
            PutProduct(product.Value, counts[product.Key]);
        }
    }

    public void RemoveProducts(Dictionary<Guid, Product> products, Dictionary<Guid, int> counts)
    {
        var removingProducts = new ProductPack();
        removingProducts.SetProducts(products);
        removingProducts.SetCount(counts);

        if (!removingProducts.IsIncluded(_productPack))
        {
            throw new ProductPackDoesNotExist("Not enough products to remove");
        }

        foreach (KeyValuePair<Guid, Product> removingProduct in removingProducts.GetProducts())
        {
            RemoveProduct(removingProduct.Value, counts[removingProduct.Key]);
        }
    }

    public void ChangeShopPrices(Dictionary<Guid, decimal> prices)
    {
        if (prices.Count != _productPack.GetProducts().Count || !prices.Keys.SequenceEqual(_productPack.GetProducts().Keys))
        {
            throw new IdException("Wrong pair of product and price");
        }

        _productPack.ChangePrices(prices);
    }

    public decimal CheckPrice(Product product)
    {
        if (!_productPack.ProductExists(product.GetId()))
        {
            throw new ProductDoesNotExist("No such product");
        }

        return _productPack.GetProducts()[product.GetId()].GetPrice();
    }

    public decimal CheckTotalPrice(ProductPack products)
    {
        if (!products.IsIncluded(_productPack))
        {
            return decimal.MaxValue;
        }

        decimal price = 0;
        foreach (KeyValuePair<Guid, Product> product in products.GetProducts())
        {
            price += CheckPrice(product.Value) * products.GetCount()[product.Key];
        }

        return price;
    }

    public Dictionary<Guid, decimal> ViewPrices()
    {
        var shopPrices = new Dictionary<Guid, decimal>();
        foreach (KeyValuePair<Guid, Product> product in _productPack.GetProducts())
        {
            shopPrices[product.Key] = CheckPrice(product.Value);
        }

        return shopPrices;
    }
}