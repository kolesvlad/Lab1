class Program
{
    static void Main()
    {
        // Ініціалізуємо перелік товарів
        var smartphones = new List<Item>
        {
            new Smartphone(price: 100, quantity: 10),
            new Smartphone(price: 200, quantity: 10),
            new Smartphone(price: 300, quantity: 10)
        };
        
        // Відображаємо ціни у зручному форматі
        DisplayPrices(smartphones);

        // Ініціалізація Customer конструктором
        var customer = new Customer(name: "John", availableFunds: 700);
        
        // Ініціалізація Cart конструктором
        var cart = new Cart(smartphones, Delivery.SelfPickUp);

        // Перевірка чи всі товари з кошику доступні для оплати криптою
        if (cart.IsEverythingCryptoSellable())
        {
            // Виконання купівлі
            Purchase(cart, customer, smartphones);
        }
    }

    private static void DisplayPrices(List<Item> items)
    {
        Console.WriteLine("Prices:");
        foreach (var item in items)
        {
            Console.WriteLine(item.GetDisplayPriceInDollars());
        }
    }

    private static void Purchase(Cart cart, Customer customer, List<Item> items)
    {
        var totalPrice = cart.CalculateTotalPrice();
        customer.ChargeMoney(totalPrice);   
        foreach (var smartphone in items)
        {
            smartphone.RemoveFromStock(1);
        }
        Console.WriteLine("Purchase succeeded!");
    }
}

interface ICryptoSellable
{
    bool IsBitcoinAccepted();
}

abstract class Item : ICryptoSellable
{
    public long Price { get; protected init; }
    public long Quantity { get; protected set; }

    public bool IsBitcoinAccepted()
    {
        return true;
    }

    public string GetDisplayPriceInDollars()
    {
        return "$" + Price / 100 + "," + Price % 100;
    }

    public void RemoveFromStock(long quantity)
    {
        this.Quantity -= quantity;
    }
}

class Smartphone : Item
{
    public Smartphone(long price, long quantity)
    {
        Price = price;
        Quantity = quantity;
    }
}

class Customer
{
    public string Name;
    private long _availableFunds;

    public Customer(string name, long availableFunds)
    {
        Name = name;
        _availableFunds = availableFunds;
    }

    public void ChargeMoney(long price)
    {
        if (IsEnoughFunds(price))
        {
            DeductFunds(price);
        }
        else
        {
            NullifyBalance();
        }
    }

    private void DeductFunds(long price)
    {
        _availableFunds -= price;
    }

    private bool IsEnoughFunds(long price)
    {
        return _availableFunds > price;
    }

    private void NullifyBalance()
    {
        _availableFunds = 0;
    }
}

enum Delivery
{
    SelfPickUp,
    Shipment
}

class Cart
{
    public List<Item> Items;
    public Delivery Delivery;

    public Cart(List<Item> items, Delivery delivery)
    {
        Items = items;
        Delivery = delivery;
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public long CalculateTotalPrice()
    {
        var totalPrice = 0L;
        foreach (var item in Items)
        {
            totalPrice += item.Price;
        }
        
        return totalPrice;
    }

    public bool IsEverythingCryptoSellable()
    {
        var isCryptoAccepted = true;
        foreach (var item in Items)
        {
            if (!item.IsBitcoinAccepted())
            {
                isCryptoAccepted = false;
                break;
            }
        }

        return isCryptoAccepted;
    }
}

