namespace The_Fountain_of_Objects.Senses;
internal class SenseStorm : IDescription
{
    public bool Tangent { get; set; }
    public SenseStorm(bool tangent)
    {
        Tangent = tangent;
    }

    public void DescribeSense(Game game)
    {
        if (!Tangent)
        {
            Display.WriteLine("You hear a rising growling groaning sound " +
                "appraoching a roar.",
                ConsoleColor.DarkYellow);
        }
        else if(Tangent)
        {
            Display.WriteLine("You can make out the distant sound of " +
                "whirling wind",
                ConsoleColor.Yellow);
        }
    }
}
