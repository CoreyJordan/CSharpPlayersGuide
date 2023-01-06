using The_Fountain_of_Objects.Enviroment;

namespace The_Fountain_of_Objects.Entities;
internal class Player
{
    public Location Location { get; set; }
    public int Arrows { get; set; }

    /// <summary>
    /// Creates a player object at the starting position.
    /// </summary>
    public Player(Location start, int arrows)
    {
        Location = start;
        Arrows = arrows;
    }
}
