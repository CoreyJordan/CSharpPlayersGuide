using The_Fountain_of_Objects.Command;
using The_Fountain_of_Objects.Entities;
using The_Fountain_of_Objects.Enviroment;
using The_Fountain_of_Objects.Senses;

namespace The_Fountain_of_Objects;
internal class Game
{
    public Grid Grid { get; }
    public Player Player { get; }
    public Fountain Fountain { get; }
    public bool GameOver { get; set; }
    public int GridSize { get; set; }

    /// <summary>
    /// Creates a new instance of the Founatain of Objects game using player
    /// starting choices.
    /// </summary>
    /// <param name="size">Grid size of the cavern. Grid is square.</param>
    /// <param name="start">Starting location to be passed to the grid</param>
    public Game(int size,
                Location start,
                int pitQty,
                int maelstromQty,
                int amaroksQty)
    {
        GridSize = size;
        Grid = new(GridSize, start, pitQty, maelstromQty, amaroksQty);
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

            // Tell the player what they sense in the current room.
            foreach (IDescription description in GetSenses())
            {
                description.DescribeSense(this);
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

    private List<IDescription> GetSenses()
    {
        List<IDescription> sense = new();

        var room = Grid.GetRoomType(Player.Location);
        var adjacentRooms = Grid.GetAdjacentTypes(Player.Location, GridSize);
        var tangentRooms = Grid.GetTangentTypes(Player.Location, GridSize);

        // Add the current room to the list of sensory inputs.
        if (room == Room.Empty) sense.Add(new SenseEmpty());
        else if (room == Room.Entrance) sense.Add(new SenseEntrance(true));
        else if (room == Room.Fountain) sense.Add(new SenseFountain(true));

        // Add the rooms directly adjacent to the current room.
        foreach (var adj in adjacentRooms)
        {
            if (adj == Room.Entrance) sense.Add(new SenseEntrance(false));
            else if (adj == Room.Fountain) sense.Add(new SenseFountain(false));
            else if (adj == Room.Pit) sense.Add(new SensePit());
            else if (adj == Room.Empty) sense.Add(new SenseEmpty());
            else if (adj == Room.Storm) sense.Add(new SenseStorm(false));
            else if (adj == Room.Amarok) sense.Add(new SenseAmarok(false));
        }

        // Add the room diaganally adjacent to the current room.
        foreach (var tanRoom in tangentRooms)
        {
            if (tanRoom == Room.Storm) sense.Add(new SenseStorm(true));
            else if (tanRoom == Room.Amarok) sense.Add(new SenseAmarok(true));
        }

        return sense;
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
        var room = Grid.GetRoomType(Player.Location);

        if (room == Room.Entrance && Fountain.Enabled)
        {
            Display.WriteLine("VICTORY! You have activated the Fountain of " +
                "Objects and escaped\nthe cavern with your life.",
                ConsoleColor.Green);
            ReadLine();
            GameOver = true;
        }
        else if (room == Room.Pit)
        {
            Display.WriteLine("DEATH! You have fallen into a pit trap",
                ConsoleColor.Red);
            ReadLine();
            GameOver = true;
        }
        else if (room == Room.Storm)
        {
            Display.WriteLine("The Maelstrom slams into you, sending you " +
                "cartwheeling several\nrooms away.",
                ConsoleColor.Red);

            // Maelstrom blows away to a new random location.
            Location maelstrom = Grid.SetRoom(GridSize);
            Grid.Map[maelstrom.Row, maelstrom.Col] = Room.Storm;
            Grid.Map[Player.Location.Row, Player.Location.Col] = Room.Empty;

            // Move the player north 1 room and east 2 rooms.
            List<ICommand> blownAway = new();
            blownAway.Add(new Move(Direction.North));
            blownAway.Add(new Move(Direction.East));
            blownAway.Add(new Move(Direction.East));
            foreach (var move in blownAway)
            {
                move.Execute(this);
            }
            ReadLine();
        }
        else if (room == Room.Amarok)
        {
            Display.WriteLine("DEATH! You have been torn apart by an amarok",
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
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                Write($"{Grid.Map[i, j], -10} ");

            }
            WriteLine();
            WriteLine();
        }
        WriteLine();
    }
}
