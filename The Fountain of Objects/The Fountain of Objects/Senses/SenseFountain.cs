using The_Fountain_of_Objects.Config;

namespace The_Fountain_of_Objects.Senses;

internal class SenseFountain : IDescription
{
    public bool InRoom { get; set; }

    public SenseFountain(bool inRoom)
    {
        InRoom = inRoom;
    }

    public void DescribeSense(Game game)
    {
        if (InRoom && !game.Fountain.Enabled)
        {
            Display.WriteLine("From somwhere in the room you hear the steady" +
                " drip ... drip ...\ndrip of water. This must be the" +
                " fountain room.",
                ConsoleColor.Blue);
        }
        else if (InRoom && game.Fountain.Enabled)
        {
            Display.WriteLine("You are in the fountain room",
                ConsoleColor.Cyan);
        }
    }
}
