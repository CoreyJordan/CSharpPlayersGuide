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
                Location start)
    {
        Grid = new(size, start);
        Player = new(start);
        Fountain = new Fountain();
        GameOver = false;
    }

    /// <summary>
    /// Loops a single turn iteration until the game has ended.
    /// </summary>
    public void Run()
    {
        while (!GameOver)
        {
            PrintRoom(Player.Location);
            ICommand command = GetCommand();
            command.Execute(this);

            ReadLine(); //Testing stop point
        }
    }

    public ICommand GetCommand()
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
                Display.WriteLine("Hmm. I didn't quite get that.", ConsoleColor.Yellow);
                validCommand = false;
                // This is a placeholder command, not expected to ever reach.
                // The compiler doesn't see the loop until we get valid command
                command = new Move(Direction.None);
            }

        } while (!validCommand);
        return command;
    }

    private void PrintRoom(Location location)
    {
        Display.WriteLine(
            "----------------------------------------------------------------",
            ConsoleColor.Cyan);
        WriteLine($"You are in Room {location.X},{location.Y}.");
    }
}
