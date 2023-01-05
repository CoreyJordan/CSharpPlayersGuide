using The_Fountain_of_Objects.Config;
using The_Fountain_of_Objects.Enviroment;

namespace The_Fountain_of_Objects.Command;
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
                " Pulling on the\nlever, you hear the dripping of water" +
                " slowly rise to a steady\nflow. You have activated the " +
                "fountain",
                ConsoleColor.Green);
        }
        else
        {
            Display.WriteLine(
                "You are not in the fountain room",
                ConsoleColor.Red);
        }
    }
}
