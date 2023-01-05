namespace The_Fountain_of_Objects.Senses;
internal class SenseEmpty : IDescription
{
    public bool InRoom { get; set; }
    public SenseEmpty(bool inRoom)
    {
        InRoom = inRoom;
    }

    public void DescribeSense(Game game)
    {
    }
}
