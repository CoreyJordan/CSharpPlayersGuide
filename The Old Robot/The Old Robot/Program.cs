namespace OldRobot;

public class Program
{
    public static void Main()
    {
        var robby = new Robot();

        while (true)
        {
            int i = 0;
            while (i < 3)
            {
                try
                {
                    string? comm = GetCommand(i+1);
                    robby.Commands[i] = RouteCommand(comm);
                    i++;
                }
                catch
                {
                    Fail("Improper Command. Try again");
                }
            }

            robby.Run();

            Console.ReadLine();

        }

        string GetCommand(int commCount)
        {
            Console.WriteLine($"Select {commCount} of 3 commands:");
            Console.WriteLine("\tI: Power On\n" +
                              "\tO: Power off\n" +
                              "\tW: Move North\n" +
                              "\tA: Move West\n" +
                              "\tS: Move South\n" +
                              "\tD: Move East");
            return Console.ReadLine()!.ToUpper();
        }

        IRobotCommand RouteCommand(string command)
        {
            return command switch
            {
                "I" => new OnRobot(),
                "O" => new OffRobot(),
                "W" => new NorthCommand(),
                "A" => new WestCommand(),
                "D" => new EastCommand(),
                "S" => new SouthCommand(),
                _ => throw new Exception()
            };
        }

        void Fail(string fail)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(fail);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

public class Robot
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPowered { get; set; }
    public IRobotCommand?[] Commands { get; } = new IRobotCommand?[3];

    public void Run()
    {
        foreach (IRobotCommand? command in Commands)
        {
            command!.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}

public interface IRobotCommand
{
    void Run(Robot robot);
}

/// <summary>
/// Turn the robot on.
/// </summary>
public class OnRobot : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (!robot.IsPowered)
        {
            robot.IsPowered = true;
        }
    }
}

/// <summary>
/// Turn the robot off.
/// </summary>
public class OffRobot : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.IsPowered = false;
        }
    }
}

public class NorthCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.Y++;
        }
    }
}

public class SouthCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.Y--;
        }
    }
}

public class WestCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.X--;
        }
    }
}

public class EastCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.X++;
        }
    }
}