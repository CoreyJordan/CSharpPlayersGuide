namespace The_Fountain_of_Objects;
internal class ActivateFountain : ICommand
{
    public Location PlayerLocation { get; set; }

    public ActivateFountain(Location playerLocation)
    {
        PlayerLocation = playerLocation;
    }

    public void Execute(Game game)
    {
        if (game.Grid.GetRoomType(PlayerLocation) == RoomType.Fountain)
        {
            game.Fountain.Enabled = true;
            Display.WriteLine("You grope in the dark until you find a lever." +
                " Pulling on the lever, you hear the dripping of water" +
                " slowly rise to a steady flow. You have activated the " +
                "fountain", ConsoleColor.Green);
        }
        else
        {
            Display.WriteLine(
                "You are not in the fountain room",
                ConsoleColor.Red);
        }
    }
}
