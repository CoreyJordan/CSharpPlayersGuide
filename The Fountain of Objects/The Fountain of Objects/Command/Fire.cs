using The_Fountain_of_Objects.Enviroment;

namespace The_Fountain_of_Objects.Command;
internal class Fire : ICommand
{
    public Dir Direction { get; set; }
    public Fire(Dir direction)
    {
        Direction = direction;
    }

    public void Execute(Game game)
    {
        var location = game.PC.Location;
        Location north = new(location.Row - 1, location.Col);
        Location south = new(location.Row + 1, location.Col);
        Location east = new(location.Row, location.Col + 1);
        Location west = new(location.Row, location.Col - 1);

        if (Direction == Dir.North && game.PC.Arrows > 0)
        {
            game.PC.Arrows--;
            if (game.Grid.GetRoomType(north) == Room.Storm ||
                game.Grid.GetRoomType(north) == Room.Amarok)
            {
                game.Grid.Map[north.Row, north.Col] = Room.Empty;
                Display.WriteLine("Success!", ConsoleColor.DarkGreen);
            }
        }
        else if (Direction == Dir.South && game.PC.Arrows > 0)
        {
            game.PC.Arrows--;
            if (game.Grid.GetRoomType(south) == Room.Storm ||
                game.Grid.GetRoomType(south) == Room.Amarok)
            {
                game.Grid.Map[south.Row, south.Col] = Room.Empty;
                Display.WriteLine("Success!", ConsoleColor.DarkGreen);
            }
        }
        else if (Direction == Dir.East && game.PC.Arrows > 0)
        {
            game.PC.Arrows--;
            if (game.Grid.GetRoomType(east) == Room.Storm ||
                game.Grid.GetRoomType(east) == Room.Amarok)
            {
                Display.WriteLine("Success!", ConsoleColor.DarkGreen);
                game.Grid.Map[east.Row, east.Col] = Room.Empty;
            }
        }
        else if (Direction == Dir.West && game.PC.Arrows > 0)
        {
            game.PC.Arrows--;
            if (game.Grid.GetRoomType(west) == Room.Storm ||
                game.Grid.GetRoomType(west) == Room.Amarok)
            {
                game.Grid.Map[west.Row, west.Col] = Room.Empty;
                Display.WriteLine("Success!", ConsoleColor.DarkGreen);
            }
        }
        else
        {
            Display.WriteLine("You are out of arrows.", ConsoleColor.Red);
        }
    }
}
