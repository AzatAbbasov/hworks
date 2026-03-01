using System;
using System.Collections.Generic;

#region DATA MODEL

public enum ServiceClass
{
    Economy,
    Business
}

public class TravelRequest
{
    public double Distance { get; set; }
    public int Passengers { get; set; }
    public ServiceClass Class { get; set; }
    public bool HasChildDiscount { get; set; }
    public bool HasSeniorDiscount { get; set; }
    public bool ExtraBaggage { get; set; }
}

#endregion

#region STRATEGY INTERFACE

public interface ICostCalculationStrategy
{
    decimal CalculateCost(TravelRequest request);
}

#endregion

#region STRATEGIES

public class PlaneCostStrategy : ICostCalculationStrategy
{
    public decimal CalculateCost(TravelRequest request)
    {
        decimal baseRate = 0.5m; // за 1 км
        decimal cost = (decimal)request.Distance * baseRate;

        if (request.Class == ServiceClass.Business)
            cost *= 1.8m;

        if (request.ExtraBaggage)
            cost += 50;

        return ApplyDiscounts(cost, request) * request.Passengers;
    }

    private decimal ApplyDiscounts(decimal cost, TravelRequest request)
    {
        if (request.HasChildDiscount)
            cost *= 0.7m;

        if (request.HasSeniorDiscount)
            cost *= 0.8m;

        return cost;
    }
}

public class TrainCostStrategy : ICostCalculationStrategy
{
    public decimal CalculateCost(TravelRequest request)
    {
        decimal baseRate = 0.3m;
        decimal cost = (decimal)request.Distance * baseRate;

        if (request.Class == ServiceClass.Business)
            cost *= 1.5m;

        return cost * request.Passengers;
    }
}

public class BusCostStrategy : ICostCalculationStrategy
{
    public decimal CalculateCost(TravelRequest request)
    {
        decimal baseRate = 0.2m;
        decimal cost = (decimal)request.Distance * baseRate;

        return cost * request.Passengers;
    }
}

#endregion

#region CONTEXT

public class TravelBookingContext
{
    private ICostCalculationStrategy _strategy;

    public void SetStrategy(ICostCalculationStrategy strategy)
    {
        _strategy = strategy;
    }

    public decimal Calculate(TravelRequest request)
    {
        if (_strategy == null)
            throw new InvalidOperationException("Стратегия не выбрана!");

        if (request.Distance <= 0 || request.Passengers <= 0)
            throw new ArgumentException("Неверные входные данные!");

        return _strategy.CalculateCost(request);
    }
}

#endregion

#region CLIENT

class Program
{
    static void Main()
    {
        TravelBookingContext context = new TravelBookingContext();
        TravelRequest request = new TravelRequest();

        Console.WriteLine("Введите расстояние (км):");
        request.Distance = double.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество пассажиров:");
        request.Passengers = int.Parse(Console.ReadLine());

        Console.WriteLine("Класс обслуживания: 1 - Economy, 2 - Business");
        request.Class = Console.ReadLine() == "2" ? ServiceClass.Business : ServiceClass.Economy;

        Console.WriteLine("Тип транспорта: 1 - Самолет, 2 - Поезд, 3 - Автобус");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                context.SetStrategy(new PlaneCostStrategy());
                break;
            case "2":
                context.SetStrategy(new TrainCostStrategy());
                break;
            case "3":
                context.SetStrategy(new BusCostStrategy());
                break;
            default:
                Console.WriteLine("Неверный выбор транспорта!");
                return;
        }

        try
        {
            decimal result = context.Calculate(request);
            Console.WriteLine($"Итоговая стоимость: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}

#endregion



#region INTERFACES

public interface IObserver
{
    string Name { get; }
    void Update(string stock, decimal price);
}

public interface ISubject
{
    void Subscribe(string stock, IObserver observer);
    void Unsubscribe(string stock, IObserver observer);
    void Notify(string stock, decimal price);
}

#endregion

#region SUBJECT

public class StockExchange : ISubject
{
    private Dictionary<string, List<IObserver>> _subscribers =
        new Dictionary<string, List<IObserver>>();

    public void Subscribe(string stock, IObserver observer)
    {
        if (!_subscribers.ContainsKey(stock))
            _subscribers[stock] = new List<IObserver>();

        _subscribers[stock].Add(observer);

        Console.WriteLine($"{observer.Name} подписался на {stock}");
    }

    public void Unsubscribe(string stock, IObserver observer)
    {
        if (_subscribers.ContainsKey(stock))
        {
            _subscribers[stock].Remove(observer);
            Console.WriteLine($"{observer.Name} отписался от {stock}");
        }
    }

    public void ChangePrice(string stock, decimal price)
    {
        Console.WriteLine($"\nЦена {stock} изменилась: {price}");
        Notify(stock, price);
    }

    public void Notify(string stock, decimal price)
    {
        if (!_subscribers.ContainsKey(stock))
            return;

        foreach (var observer in _subscribers[stock])
        {
            observer.Update(stock, price);
        }
    }
}

#endregion

#region OBSERVERS

public class Trader : IObserver
{
    public string Name { get; }

    public Trader(string name)
    {
        Name = name;
    }

    public void Update(string stock, decimal price)
    {
        Console.WriteLine($"Трейдер {Name} получил обновление: {stock} = {price}");
    }
}

public class TradingRobot : IObserver
{
    public string Name { get; }
    private decimal _threshold;

    public TradingRobot(string name, decimal threshold)
    {
        Name = name;
        _threshold = threshold;
    }

    public void Update(string stock, decimal price)
    {
        if (price < _threshold)
            Console.WriteLine($"Робот {Name}: Покупка {stock} по цене {price}");
        else
            Console.WriteLine($"Робот {Name}: Цена {stock} слишком высокая");
    }
}

#endregion

#region CLIENT

class Program
{
    static void Main()
    {
        StockExchange exchange = new StockExchange();

        Trader trader1 = new Trader("Azat");
        TradingRobot robot = new TradingRobot("RoboTrade", 100);

        exchange.Subscribe("AAPL", trader1);
        exchange.Subscribe("AAPL", robot);

        exchange.ChangePrice("AAPL", 90);
        exchange.ChangePrice("AAPL", 150);

        exchange.Unsubscribe("AAPL", trader1);

        exchange.ChangePrice("AAPL", 80);
    }
}

#endregion
