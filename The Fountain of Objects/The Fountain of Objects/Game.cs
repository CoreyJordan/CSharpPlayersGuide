using The_Fountain_of_Objects.Command;
using The_Fountain_of_Objects.Entities;
using The_Fountain_of_Objects.Enviroment;
using The_Fountain_of_Objects.Senses;

namespace The_Fountain_of_Objects;
internal class Game
{
    public Grid Grid { get; set; }
    public Player Player { get; set; }
    public Fountain Fountain { get; set; }
    public bool GameOver { get; set; }

    /// <summary>
    /// Creates a new instance of the Founatain of Objects game using player
    /// starting choices.
    /// </summary>
    /// <param name="size">Grid size of the cavern. Grid is square.</param>
    /// <param name="start">Starting location to be passed to the grid</param>
    public Game(int size,
                Location start,
                int pitQty)
    {
        Grid = new(size, start, pitQty);
        Player = new(start);
        Fountain = new Fountain();
        GameOver = false;
    }

    /// <summary>
    /// Loops a single turn iteration until the game has ended.
    /// </summary>
    public void Run()
    {
        Test(); // Displays the player location and a map of the grid
        while (!GameOver)
        {
            // Tell the player where they are.
            PrintRoom(Player.Location);

            // Tell the player what they sense IN the current room.
            IDescription description = GetSenseOf(Player.Location);
            description.DescribeSense(this);

            // Tell the player what they sense from AROUND the current room.
            var adjLocations = Grid.GetAdjacentTypes(Player.Location,
                                                     Grid.Map.GetLength(0));
            foreach (var room in adjLocations)
            {
                IDescription nearby = GetSenseNear(room);
                nearby.DescribeSense(this);
            }

            // The PLAYER acts here, get and execute player command.
            ICommand command = GetCommand();
            command.Execute(this);

            CheckState();
        }
    }

    private void PrintRoom(Location location)
    {
        Display.WriteLine(
            "---------------------------------------------------------------" +
            $"\nYou are in Room {location.Row},{location.Col}.",
            ConsoleColor.Cyan);
    }

    private IDescription GetSenseOf(Location location)
    {
        var room = Grid.GetRoomType(location);
        switch (room)
        {
            case RoomType.Entrance:
                return new SenseEntrance(true);
            case RoomType.Fountain:
                return new SenseFountain(true);
            default:
                return new SenseEmpty();
        }
    }
    
    private IDescription GetSenseNear(RoomType room)
    {
        switch (room)
        {
            case RoomType.Entrance:
                return new SenseEntrance(false);
            case RoomType.Fountain:
                return new SenseFountain(false);
            case RoomType.Pit:
                return new SensePit();
            default:
                return new SenseEmpty();
        }
    }

    private ICommand GetCommand()
    {
        bool validCommand;
        ICommand command;
        do
        {
            validCommand = true;
            Write("What do you want to do? ");
            string? choice = ReadLine() ?? "";

            if (choice.ToLower().IndexOf("north") > -1)
            {
                command = new Move(Direction.North);
            }
            else if (choice.ToLower().IndexOf("south") > -1)
            {
                command = new Move(Direction.South);
            }
            else if (choice.ToLower().IndexOf("east") > -1)
            {
                command = new Move(Direction.East);
            }
            else if (choice.ToLower().IndexOf("west") > -1)
            {
                command = new Move(Direction.West);
            }
            else if (choice.ToLower().IndexOf("fountain") > -1)
            {
                command = new ActivateFountain(Player.Location);
            }
            else
            {
                Display.WriteLine("Hmm. I didn't quite get that.",
                    ConsoleColor.Yellow);
                validCommand = false;
                // This is a placeholder command, not expected to ever reach.
                // The compiler doesn't see the loop until we get valid command
                command = new Move(Direction.None);
            }

        } while (!validCommand);
        return command;
    }

    private void CheckState()
    {
        if (Grid.GetRoomType(Player.Location) == RoomType.Entrance &&
            Fountain.Enabled)
        {
            Display.WriteLine("VICTORY! You have activated the Fountain of " +
                "Objects and escaped\nthe cavern with your life.",
                ConsoleColor.Green);
            ReadLine();
            GameOver = true;
        }
        else if (Grid.GetRoomType(Player.Location) == RoomType.Pit)
        {
            Display.WriteLine("DEATH! You have fallen into a pit trap",
                ConsoleColor.Red);
            ReadLine();
            GameOver = true;
        }
    }

    //TEST
    private void Test()
    {
        WriteLine(Player.Location);
        WriteLine();
        for (int i = 0; i < Grid.Map.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.Map.GetLength(0); j++)
            {
                Write($"{Grid.Map[i, j], -10} ");

            }
            WriteLine();
            WriteLine();
        }
        WriteLine();
    }
}
