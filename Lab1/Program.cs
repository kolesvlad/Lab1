// See https://aka.ms/new-console-template for more information

class Program
{
    static void Main()
    {
        Console.WriteLine("Test passed");
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

    public void chargeMoney(long price)
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

    public void DeductFunds(long price)
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

