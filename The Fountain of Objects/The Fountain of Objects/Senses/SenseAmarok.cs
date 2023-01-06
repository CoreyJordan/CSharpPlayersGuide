namespace The_Fountain_of_Objects.Senses;
internal class SenseAmarok : IDescription
{
    public bool Tangent { get; set; }

    public SenseAmarok(bool tangent)
    {
        Tangent = tangent;
    }

    public void DescribeSense(Game game)
    {
        if (!Tangent)
        {
            Display.WriteLine("You catch the stench of an Amarok nearby.",
                ConsoleColor.DarkYellow);
        }
        else if (Tangent)
        {
            Display.WriteLine("There's something foul in the air.",
                ConsoleColor.Yellow);
        }
    }
}
