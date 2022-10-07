using Shops.Exceptions;

namespace Shops.Entities;

public class ProductPack
{
    private Dictionary<Guid, Product> _products;
    private Dictionary<Guid, int> _count;
    public ProductPack()
    {
        _products = new Dictionary<Guid, Product>();
        _count = new Dictionary<Guid, int>();
    }

    public ProductPack(Dictionary<Guid, Product> products, Dictionary<Guid, int> count)
    {
        if (products.Count != count.Count || !products.Keys.SequenceEqual(count.Keys))
        {
            throw new IdException("Wrong pair of product and count");
        }

        _products = products;
        _count = count;
    }

    public Dictionary<Guid, Product> GetProducts()
    {
        return _products;
    }

    public Dictionary<Guid, int> GetCount()
    {
        return _count;
    }

    public bool ProductExists(Guid id)
    {
        if (_products.ContainsKey(id))
        {
            return true;
        }

        return false;
    }

    public void AddProduct(Product product, int count)
    {
        if (ProductExists(product.GetId()))
        {
            throw new ProductAlreadyExists("Product already exists");
        }

        _products.Add(product.GetId(), product);
        _count.Add(product.GetId(), count);
    }

    public void SetProducts(Dictionary<Guid, Product> newList)
    {
        _products = newList;
    }

    public void SetCount(Dictionary<Guid, int> newList)
    {
        if (newList.Any(count => count.Value < 0))
        {
            throw new CountException("Count must be > 0");
        }

        _count = newList;
    }

    public void ChangePrices(Dictionary<Guid, decimal> listOfPrices)
    {
        if (listOfPrices.Count != _products.Count || !_products.Keys.SequenceEqual(listOfPrices.Keys))
        {
            throw new IdException("Wrong pair of product and price");
        }

        if (listOfPrices.Any(price => price.Value < 0))
        {
            throw new CountException("Price must be > 0");
        }

        foreach (KeyValuePair<Guid, decimal> price in listOfPrices.Where(price => ProductExists(price.Key)))
        {
            _products[price.Key].ChangePrice(price.Value);
        }
    }

    public bool IsIncluded(ProductPack bigPack)
    {
        foreach (KeyValuePair<Guid, Product> elem in _products)
        {
            if (!bigPack.ProductExists(elem.Key) && bigPack.GetCount()[elem.Key] < _count[elem.Key])
                return false;
        }

        return true;
    }
}