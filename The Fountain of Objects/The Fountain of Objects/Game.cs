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

    /// <summary>
    /// Creates a new instance of the Founatain of Objects game using player
    /// starting choices.
    /// </summary>
    /// <param name="size">Grid size of the cavern. Grid is square.</param>
    /// <param name="start">Starting location to be passed to the grid</param>
    public Game(int size,
                Location start,
                int pitQty,
                int maelstromQty)
    {
        Grid = new(size, start, pitQty, maelstromQty);
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
            var mapSize = Grid.Map.GetLength(0);
            foreach (var description in GetSense(mapSize))
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

    private List<IDescription> GetSense(int mapSize)
    {
        List<IDescription> sense = new();

        // Add the current room to the list of sensory inputs.
        var room = Grid.GetRoomType(Player.Location);
        var adjacentRooms = Grid.GetAdjacentTypes(Player.Location, mapSize);
        var tangentRooms = Grid.GetTangentTypes(Player.Location, mapSize);

        if (room == RoomType.Empty) sense.Add(new SenseEmpty());
        else if (room == RoomType.Entrance) sense.Add(new SenseEntrance(true));
        else if (room == RoomType.Fountain) sense.Add(new SenseFountain(true));

        foreach (var adjRoom in adjacentRooms)
        {
            if (adjRoom == RoomType.Entrance) sense.Add(new SenseEntrance(false));
            else if (adjRoom == RoomType.Fountain) sense.Add(new SenseFountain(false));
            else if (adjRoom == RoomType.Pit) sense.Add(new SensePit());
            else if (adjRoom == RoomType.Empty) sense.Add(new SenseEmpty());
            else if (adjRoom == RoomType.Maelstrom) sense.Add(new SenseMaelstrom(false));
        }

        foreach (var tanRoom in tangentRooms)
        {
            if (tanRoom == RoomType.Maelstrom) sense.Add(new SenseMaelstrom(true));
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
