using Shops.Exceptions;

namespace Shops.Entities;

public class Product
{
    private string _name;
    private Guid _id;
    private decimal _price;

    public Product()
    {
        _name = string.Empty;
        _id = Guid.Empty;
        _price = decimal.Zero;
    }

    public Product(string name, Guid id, decimal price)
    {
        _name = name;
        _id = id;
        _price = price;
    }

    public static bool operator ==(Product product1, Product product2)
    {
        return product1.GetName() == product2.GetName() && product1.GetPrice() == product2.GetPrice();
    }

    public static bool operator !=(Product product1, Product product2)
    {
        return product1.GetName() != product2.GetName() || product1.GetPrice() != product2.GetPrice();
    }

    public string GetName()
    {
        return _name;
    }

    public Guid GetId()
    {
        return _id;
    }

    public decimal GetPrice()
    {
        return _price;
    }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            throw new PriceException("Price should be > 0");
        }

        _price = newPrice;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Product)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_name, _id, _price);
    }

    protected bool Equals(Product other)
    {
        return _name == other._name && _id.Equals(other._id) && _price == other._price;
    }
}