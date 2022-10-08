using Shops.Exceptions;

namespace Shops.Entities;

public class Person
{
    private string _name;
    private decimal _budget;

    public Person()
    {
        _name = string.Empty;
        _budget = decimal.Zero;
    }

    public Person(string name, decimal budget)
    {
        _name = name;
        _budget = budget;
    }

    public decimal GetBudget()
    {
        return _budget;
    }

    public void GoShopping(Shop shop, ProductPack productPack)
    {
        if (!productPack.IsIncluded(shop.GetProductPack()))
        {
            throw new ProductPackDoesNotExist("No this pack in this shop");
        }

        if (shop.CheckTotalPrice(productPack) > _budget)
        {
            throw new BudgetException("Not enough money");
        }

        _budget -= shop.CheckTotalPrice(productPack);
        shop.RemoveProducts(productPack.GetProducts(), productPack.GetCount());
    }
}