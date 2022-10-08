namespace Shops.Test;
using Shops.Entities;
using Xunit;
public class Service
{
    [Fact]
    public void CheckDelivery()
    {
        Warehouse warehouse = new Warehouse();
        Product apple = warehouse.AddProduct("apple", 20);
        Product bread = warehouse.AddProduct("bread", 30);
        Product table = warehouse.AddProduct("table", 100);
        Shop diksi = warehouse.AddShop("DIKSI", "SPB");
        ProductPack myPack = new ProductPack();
        myPack.AddProduct(apple, 30);
        myPack.AddProduct(bread, 40);
        myPack.AddProduct(table, 2);
        Dictionary<Guid, Product> products = new Dictionary<Guid, Product>();
        products[apple.GetId()] = apple;
        products[bread.GetId()] = bread;
        products[table.GetId()] = table;
        Dictionary<Guid, int> counts = new Dictionary<Guid, int>();
        counts[apple.GetId()] = 30;
        counts[bread.GetId()] = 40;
        counts[table.GetId()] = 2;
        Dictionary<Guid, decimal> prices = new Dictionary<Guid, decimal>();
        prices[apple.GetId()] = 20;
        prices[bread.GetId()] = 30;
        prices[table.GetId()] = 100;
        warehouse.Delivery(diksi, products, counts, prices);
        Assert.Equal(myPack.GetProducts(), diksi.GetProductPack().GetProducts());
        Assert.Equal(myPack.GetCount(), diksi.GetProductPack().GetCount());
    }

    [Fact]
    public void CheckChangesOfPrices()
    {
        Warehouse warehouse = new Warehouse();
        Product apple = warehouse.AddProduct("apple", 20);
        Product bread = warehouse.AddProduct("bread", 30);
        Product table = warehouse.AddProduct("table", 100);
        Shop diksi = warehouse.AddShop("DIKSI", "SPB");
        ProductPack myPack = new ProductPack();
        myPack.AddProduct(apple, 30);
        myPack.AddProduct(bread, 40);
        myPack.AddProduct(table, 2);
        Dictionary<Guid, Product> products = new Dictionary<Guid, Product>();
        products[apple.GetId()] = apple;
        products[bread.GetId()] = bread;
        products[table.GetId()] = table;
        Dictionary<Guid, int> counts = new Dictionary<Guid, int>();
        counts[apple.GetId()] = 30;
        counts[bread.GetId()] = 40;
        counts[table.GetId()] = 2;
        Dictionary<Guid, decimal> prices = new Dictionary<Guid, decimal>();
        prices[apple.GetId()] = 20;
        prices[bread.GetId()] = 30;
        prices[table.GetId()] = 100;
        warehouse.Delivery(diksi, products, counts, prices);
        prices[apple.GetId()] = 200;
        prices[bread.GetId()] = 300;
        prices[table.GetId()] = 1000;
        diksi.ChangeShopPrices(prices);
        Assert.Equal(prices, diksi.ViewPrices());
    }

    [Fact]
    public void CheckCheapestShop()
    {
        Warehouse warehouse = new Warehouse();
        Product apple = warehouse.AddProduct("apple", 20);
        Product bread = warehouse.AddProduct("bread", 30);
        Product table = warehouse.AddProduct("table", 100);
        Shop diksi = warehouse.AddShop("DIKSI", "SPB");
        Shop lenta = warehouse.AddShop("LENTA", "SPB");
        Dictionary<Guid, Product> products = new Dictionary<Guid, Product>();
        products[apple.GetId()] = apple;
        products[bread.GetId()] = bread;
        products[table.GetId()] = table;
        Dictionary<Guid, int> counts = new Dictionary<Guid, int>();
        counts[apple.GetId()] = 30;
        counts[bread.GetId()] = 40;
        counts[table.GetId()] = 2;
        Dictionary<Guid, decimal> prices = new Dictionary<Guid, decimal>();
        prices[apple.GetId()] = 20;
        prices[bread.GetId()] = 30;
        prices[table.GetId()] = 100;
        warehouse.Delivery(diksi, products, counts, prices);
        prices[apple.GetId()] = 200;
        prices[bread.GetId()] = 300;
        prices[table.GetId()] = 1000;
        warehouse.Delivery(lenta, products, counts, prices);
        ProductPack myPack = new ProductPack();
        myPack.AddProduct(apple, 30);
        myPack.AddProduct(bread, 40);
        myPack.AddProduct(table, 2);
        Assert.Equal("DIKSI", warehouse.MinimalReceiptPrice(myPack).GetName());
    }

    [Fact]
    public void CheckPurchase()
    {
        Warehouse warehouse = new Warehouse();
        Product apple = warehouse.AddProduct("apple", 20);
        Product bread = warehouse.AddProduct("bread", 30);
        Product table = warehouse.AddProduct("table", 100);
        Shop diksi = warehouse.AddShop("DIKSI", "SPB");
        ProductPack myPack = new ProductPack();
        myPack.AddProduct(apple, 1);
        myPack.AddProduct(bread, 1);
        myPack.AddProduct(table, 1);
        Dictionary<Guid, Product> products = new Dictionary<Guid, Product>();
        products[apple.GetId()] = apple;
        products[bread.GetId()] = bread;
        products[table.GetId()] = table;
        Dictionary<Guid, int> counts = new Dictionary<Guid, int>();
        counts[apple.GetId()] = 30;
        counts[bread.GetId()] = 40;
        counts[table.GetId()] = 2;
        Dictionary<Guid, decimal> prices = new Dictionary<Guid, decimal>();
        prices[apple.GetId()] = 20;
        prices[bread.GetId()] = 30;
        prices[table.GetId()] = 100;
        warehouse.Delivery(diksi, products, counts, prices);
        Person customer = new Person("IVAN", 200);
        customer.GoShopping(diksi, myPack);
        ProductPack shopPack = new ProductPack();
        shopPack.AddProduct(apple, 29);
        shopPack.AddProduct(bread, 39);
        shopPack.AddProduct(table, 1);
        Assert.Equal(50, customer.GetBudget());
        Assert.Equal(shopPack.GetCount(), diksi.GetProductPack().GetCount());
    }
}