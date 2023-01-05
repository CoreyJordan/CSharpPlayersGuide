using The_Fountain_of_Objects.Config;

namespace The_Fountain_of_Objects.Command;
internal class Move : ICommand
{
    /// <summary>
    /// Direction to move the entity.
    /// </summary>
    public Direction Direction { get; }

    public Move(Direction direction)
    {
        Direction = direction;
    }

    public void Execute(Game game)
    {
        var border = game.Grid.GetBorder(game.Player.Location);

        if (Direction == Direction.North && border != Edge.Upper
            && border != Edge.CornerUpLeft && border != Edge.CornerUpRight)
        {
            game.Player.Location.Row -= 1;
        }
        else if (Direction == Direction.South && border != Edge.Lower
            && border != Edge.CornerLowLeft && border != Edge.CornerLowRight)
        {
            game.Player.Location.Row += 1;
        }
        else if (Direction == Direction.West && border != Edge.Left
            && border != Edge.CornerUpLeft && border != Edge.CornerLowLeft)
        {
            game.Player.Location.Col -= 1;
        }
        else if (Direction == Direction.East && border != Edge.Right
            && border != Edge.CornerUpRight && border != Edge.CornerLowRight)
        {
            game.Player.Location.Col += 1;
        }
        else
        {
            Display.WriteLine("Ouch, you ran into a wall.", ConsoleColor.Red);
        }
    }
}
