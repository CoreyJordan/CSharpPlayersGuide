namespace The_Fountain_of_Objects.Senses;
internal class SenseEntrance : IDescription
{
    public bool InRoom { get; set; }

    public SenseEntrance(bool inRoom)
    {
        InRoom = inRoom;
    }

    public void DescribeSense(Game game)
    {
        if (InRoom)
        {
            Display.WriteLine("You see light flooding into the cavern from beyond",
                              ConsoleColor.Cyan);
        }
    }
}
