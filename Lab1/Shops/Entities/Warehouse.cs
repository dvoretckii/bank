using Shops.Exceptions;

namespace Shops.Entities;

public class Warehouse
{
    private Dictionary<Guid, Product> _productsInStock;
    private Dictionary<Guid, Shop> _availableShops;

    public Warehouse()
    {
        _availableShops = new Dictionary<Guid, Shop>();
        _productsInStock = new Dictionary<Guid, Product>();
    }

    public Warehouse(Dictionary<Guid, Product> productsInStock, Dictionary<Guid, Shop> availableShops)
    {
        _availableShops = availableShops;
        _productsInStock = productsInStock;
    }

    public Dictionary<Guid, Product> GetProducts()
    {
        return _productsInStock;
    }

    public Dictionary<Guid, Shop> GetShops()
    {
        return _availableShops;
    }

    public Product AddProduct(string name, decimal merchantPrice)
    {
        var id = Guid.NewGuid();
        var product = new Product(name, id, merchantPrice);
        _productsInStock.Add(id, product);
        return product;
    }

    public Shop AddShop(string name, string adress)
    {
        var id = Guid.NewGuid();
        var shop = new Shop(name, adress, id);
        _availableShops.Add(id, shop);
        return shop;
    }

    public void Delivery(Shop shop, Dictionary<Guid, Product> products, Dictionary<Guid, int> counts, Dictionary<Guid, decimal> prices)
    {
        if (products.Count != counts.Count || !products.Keys.SequenceEqual(counts.Keys))
        {
            throw new IdException("Wrong pair of product and count");
        }

        if (products.Count != prices.Count || !products.Keys.SequenceEqual(prices.Keys))
        {
            throw new IdException("Wrong pair of product and price");
        }

        shop.PutProducts(products, counts);
        shop.ChangeShopPrices(prices);
    }

    public Shop MinimalReceiptPrice(ProductPack productPack)
    {
        decimal minPrice = decimal.MaxValue;
        Guid id = Guid.Empty;
        foreach (KeyValuePair<Guid, Shop> shop in _availableShops.Where(shop => shop.Value.CheckTotalPrice(productPack) < minPrice))
        {
            id = shop.Key;
            minPrice = shop.Value.CheckTotalPrice(productPack);
        }

        if (minPrice == decimal.MaxValue)
        {
            throw new ProductPackDoesNotExist("No such products in shops");
        }

        return _availableShops[id];
    }
}