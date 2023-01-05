namespace The_Fountain_of_Objects;
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
        throw new NotImplementedException();
    }
}
