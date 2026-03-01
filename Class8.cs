using System;
using System.Collections.Generic;

#region Strategy Interface
public interface IPaymentStrategy
{
    void Pay(decimal amount);
}
#endregion

#region Concrete Strategies
public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Оплата {amount} тенге банковской картой выполнена.");
    }
}

public class PayPalPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Оплата {amount} тенге через PayPal выполнена.");
    }
}

public class CryptoPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Оплата {amount} тенге с помощью криптовалюты выполнена.");
    }
}
#endregion

#region Context
public class PaymentContext
{
    private IPaymentStrategy _strategy;

    public void SetStrategy(IPaymentStrategy strategy)
    {
        _strategy = strategy;
    }

    public void ExecutePayment(decimal amount)
    {
        if (_strategy == null)
        {
            Console.WriteLine("Стратегия оплаты не выбрана!");
            return;
        }

        _strategy.Pay(amount);
    }
}
#endregion

#region Client
class Program
{
    static void Main()
    {
        PaymentContext context = new PaymentContext();

        Console.WriteLine("Введите сумму оплаты:");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine("Выберите способ оплаты:");
        Console.WriteLine("1 - Банковская карта");
        Console.WriteLine("2 - PayPal");
        Console.WriteLine("3 - Криптовалюта");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                context.SetStrategy(new CreditCardPayment());
                break;
            case "2":
                context.SetStrategy(new PayPalPayment());
                break;
            case "3":
                context.SetStrategy(new CryptoPayment());
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                return;
        }

        context.ExecutePayment(amount);

        Console.ReadLine();
    }
}
#endregion




#region Interfaces
public interface IObserver
{
    void Update(string currency, decimal rate);
}

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify(string currency, decimal rate);
}
#endregion

#region Subject
public class CurrencyExchange : ISubject
{
    private List<IObserver> _observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void SetRate(string currency, decimal rate)
    {
        Console.WriteLine($"\nКурс {currency} изменен на {rate}");
        Notify(currency, rate);
    }

    public void Notify(string currency, decimal rate)
    {
        foreach (var observer in _observers)
        {
            observer.Update(currency, rate);
        }
    }
}
#endregion

#region Observers
public class Bank : IObserver
{
    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"Банк получил обновление: {currency} = {rate}");
    }
}

public class ExchangeOffice : IObserver
{
    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"Обменный пункт обновил курс: {currency} = {rate}");
    }
}

public class Investor : IObserver
{
    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"Инвестор анализирует новый курс: {currency} = {rate}");
    }
}
#endregion

#region Client
class Program
{
    static void Main()
    {
        CurrencyExchange exchange = new CurrencyExchange();

        IObserver bank = new Bank();
        IObserver office = new ExchangeOffice();
        IObserver investor = new Investor();

        exchange.Attach(bank);
        exchange.Attach(office);
        exchange.Attach(investor);

        exchange.SetRate("USD", 470);
        exchange.SetRate("EUR", 510);

        exchange.Detach(investor);

        exchange.SetRate("RUB", 5);

        Console.ReadLine();
    }
}
#endregion
