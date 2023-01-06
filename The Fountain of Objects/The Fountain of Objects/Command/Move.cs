namespace The_Fountain_of_Objects.Command;
internal class Move : ICommand
{
    /// <summary>
    /// Direction to move the entity.
    /// </summary>
    public Dir Direction { get; }

    public Move(Dir direction)
    {
        Direction = direction;
    }

    public void Execute(Game game)
    {
        var border = game.Grid.GetBorder(game.PC.Location);

        if (Direction == Dir.North && border != Edge.Upper
            && border != Edge.CornerUpLeft && border != Edge.CornerUpRight)
        {
            game.PC.Location.Row -= 1;
        }
        else if (Direction == Dir.South && border != Edge.Lower
            && border != Edge.CornerLowLeft && border != Edge.CornerLowRight)
        {
            game.PC.Location.Row += 1;
        }
        else if (Direction == Dir.West && border != Edge.Left
            && border != Edge.CornerUpLeft && border != Edge.CornerLowLeft)
        {
            game.PC.Location.Col -= 1;
        }
        else if (Direction == Dir.East && border != Edge.Right
            && border != Edge.CornerUpRight && border != Edge.CornerLowRight)
        {
            game.PC.Location.Col += 1;
        }
        else
        {
            Display.WriteLine("Ouch, you ran into a wall.", ConsoleColor.Red);
        }
    }
}
