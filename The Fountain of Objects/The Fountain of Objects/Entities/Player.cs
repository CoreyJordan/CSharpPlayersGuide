using The_Fountain_of_Objects.Enviroment;

namespace The_Fountain_of_Objects.Entities;
internal class Player
{
    public Location Location { get; set; }

    /// <summary>
    /// Creates a player object at the starting position.
    /// </summary>
    public Player(Location start)
    {
        Location = start;
    }
}
