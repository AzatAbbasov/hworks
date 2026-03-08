using System;
using System.Collections.Generic;

interface ICommand
{
    void Execute();
    void Undo();
}

class Light
{
    public void On()
    {
        Console.WriteLine("Свет включен");
    }

    public void Off()
    {
        Console.WriteLine("Свет выключен");
    }
}

class TV
{
    public void On()
    {
        Console.WriteLine("Телевизор включен");
    }

    public void Off()
    {
        Console.WriteLine("Телевизор выключен");
    }
}

class AirConditioner
{
    public void On()
    {
        Console.WriteLine("Кондиционер включен");
    }

    public void Off()
    {
        Console.WriteLine("Кондиционер выключен");
    }
}

class LightOnCommand : ICommand
{
    Light light;

    public LightOnCommand(Light light)
    {
        this.light = light;
    }

    public void Execute()
    {
        light.On();
    }

    public void Undo()
    {
        light.Off();
    }
}

class TVOnCommand : ICommand
{
    TV tv;

    public TVOnCommand(TV tv)
    {
        this.tv = tv;
    }

    public void Execute()
    {
        tv.On();
    }

    public void Undo()
    {
        tv.Off();
    }
}

class AirOnCommand : ICommand
{
    AirConditioner air;

    public AirOnCommand(AirConditioner air)
    {
        this.air = air;
    }

    public void Execute()
    {
        air.On();
    }

    public void Undo()
    {
        air.Off();
    }
}

class MacroCommand : ICommand
{
    ICommand[] commands;

    public MacroCommand(ICommand[] commands)
    {
        this.commands = commands;
    }

    public void Execute()
    {
        foreach (var command in commands)
        {
            command.Execute();
        }
    }

    public void Undo()
    {
        foreach (var command in commands)
        {
            command.Undo();
        }
    }
}

class RemoteControl
{
    Stack<ICommand> history = new Stack<ICommand>();

    public void Press(ICommand command)
    {
        command.Execute();
        history.Push(command);
    }

    public void Undo()
    {
        if (history.Count > 0)
        {
            ICommand cmd = history.Pop();
            cmd.Undo();
        }
        else
        {
            Console.WriteLine("Нет команды для отмены");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Command пример");

        Light light = new Light();
        TV tv = new TV();
        AirConditioner air = new AirConditioner();

        RemoteControl remote = new RemoteControl();

        remote.Press(new LightOnCommand(light));
        remote.Press(new TVOnCommand(tv));
        remote.Press(new AirOnCommand(air));

        remote.Undo();

        ICommand[] party =
        {
            new LightOnCommand(light),
            new TVOnCommand(tv),
            new AirOnCommand(air)
        };

        MacroCommand macro = new MacroCommand(party);

        Console.WriteLine("\nМакрокоманда:");
        macro.Execute();
    }
}