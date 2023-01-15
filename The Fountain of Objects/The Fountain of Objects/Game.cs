using The_Fountain_of_Objects.Command;
using The_Fountain_of_Objects.Entities;
using The_Fountain_of_Objects.Enviroment;
using The_Fountain_of_Objects.Senses;

namespace The_Fountain_of_Objects;

internal class Game
{
    public Grid Grid { get; }
    public Player PC { get; }
    public Enviroment.Fountain Fountain { get; }
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
                int amaroksQty,
                int arrows)
    {
        GridSize = size;
        Grid = new(GridSize, start, pitQty, maelstromQty, amaroksQty);
        PC = new(start, arrows);
        Fountain = new Enviroment.Fountain();
        GameOver = false;
    }

    /// <summary>
    /// Loops a single turn iteration until the game has ended.
    /// </summary>
    public void Run()
    {
        // Test(); // Displays the player location and a map of the grid
        var startTime = DateTime.Now;
        while (!GameOver)
        {
            // Tell the player where they are.
            PrintRoom(PC.Location);

            // Tell the player what they sense in the current room.
            foreach (IDescription description in GetSenses())
            {
                description.DescribeSense(this);
            }

            // The PLAYER acts here, get and execute player command.
            ICommand command = GetCommand();
            command.Execute(this);

            TimeSpan gameDuration = new();
            gameDuration = DateTime.Now - startTime;
            CheckState(gameDuration);
        }
    }

    private void PrintRoom(Location location)
    {
        Display.WriteLine(
            "---------------------------------------------------------------" +
            $"\nYou are in Room {location.Row},{location.Col}.\n" +
            $"You have {PC.Arrows} arrows remaining.",
            ConsoleColor.Cyan);
    }

    private List<IDescription> GetSenses()
    {
        List<IDescription> sense = new();

        var room = Grid.GetRoomType(PC.Location);
        var adjacentRooms = Grid.GetAdjacentTypes(PC.Location, GridSize);
        var tangentRooms = Grid.GetTangentTypes(PC.Location, GridSize);

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
        ICommand comm;
        do
        {
            validCommand = true;
            Write("What do you want to do? ");
            string? choice = ReadLine()?.ToLower() ?? "";

            if (choice.Contains("move n")) comm = new Move(Dir.North);
            else if (choice.Contains("move s")) comm = new Move(Dir.South);
            else if (choice.Contains("move e")) comm = new Move(Dir.East);
            else if (choice.Contains("move w")) comm = new Move(Dir.West);
            else if (choice.Contains("foun")) comm = new Activate(PC.Location);
            else if (choice.Contains("fire n")) comm = new Fire(Dir.North);
            else if (choice.Contains("fire s")) comm = new Fire(Dir.South);
            else if (choice.Contains("fire w")) comm = new Fire(Dir.West);
            else if (choice.Contains("fire e")) comm = new Fire(Dir.East);
            else
            {
                Display.WriteLine("Hmm. I didn't quite get that.",
                    ConsoleColor.Yellow);
                validCommand = false;
                // This is a placeholder command, not expected to ever reach.
                // The compiler doesn't see the loop until we get valid command
                comm = new Move(Dir.None);
            }
        } while (!validCommand);
        return comm;
    }

    private void CheckState(TimeSpan gameTime)
    {
        var room = Grid.GetRoomType(PC.Location);

        if (room == Room.Entrance && Fountain.Enabled)
        {
            Display.WriteLine("VICTORY! You have activated the Fountain of " +
                "Objects and escaped\nthe cavern with your life.",
                ConsoleColor.Green);
            GameOver = true;
        }
        else if (room == Room.Pit)
        {
            Display.WriteLine("DEATH! You have fallen into a pit trap",
                ConsoleColor.Red);
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
            Grid.Map[PC.Location.Row, PC.Location.Col] = Room.Empty;

            // Move the player north 1 room and east 2 rooms.
            List<ICommand> blownAway = new();
            blownAway.Add(new Move(Dir.North));
            blownAway.Add(new Move(Dir.East));
            blownAway.Add(new Move(Dir.East));
            foreach (var move in blownAway)
            {
                move.Execute(this);
            }
        }
        else if (room == Room.Amarok)
        {
            Display.WriteLine("DEATH You have been torn apart by an amarok",
                ConsoleColor.Red);
            GameOver = true;
        }

        if (GameOver)
        {
            WriteLine("Time elapsed: " + gameTime.ToString(@"mm\:ss"));
            ReadLine();
        }
    }

    //TEST
    private void Test()
    {
        WriteLine(PC.Location);
        WriteLine();
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                Write($"{Grid.Map[i, j],-10} ");
            }
            WriteLine();
            WriteLine();
        }
        WriteLine();
    }
}