using The_Fountain_of_Objects.Config;

namespace The_Fountain_of_Objects.Enviroment;
internal class Grid
{
    /// <summary>
    /// 2D grid of rooms by type.
    /// </summary>
    public RoomType[,] Map { get; }
    public Edge[,] Border { get; }

    /// <summary>
    /// Create a map of the chosen size and place starting positions.
    /// </summary>
    /// <param name="mapSize">The width and height of the map grid.</param>
    /// <param name="start">The starting position of the player.</param>
    public Grid(int mapSize, Location start)
    {
        Map = new RoomType[mapSize, mapSize];
        Border = new Edge[mapSize, mapSize];
        Map[start.Row, start.Col] = RoomType.Entrance;
        var fountain = SetRoom(mapSize);
        Map[fountain.Row, fountain.Col] = RoomType.Fountain;
        SetBorders(mapSize);
    }

    /// <summary>
    /// Returns the room designation of a particular location.
    /// </summary>
    public RoomType GetRoomType(Location location)
    {
        return Map[location.Row, location.Col];
    }

    /// <summary>
    /// Returns which edge of the map a particular room is on, none if not on an 
    /// edge. Helps keep player on the grid.
    /// </summary>
    public Edge GetBorder(Location location)
    {
        return Border[location.Row, location.Col];
    }

    private Location GetRandomRoom(int max)
    {
        Random rand = new();
        return new(rand.Next(max), rand.Next(max));
    }

    private Location SetRoom(int max)
    {
        var randomRoom = GetRandomRoom(max);
        while (GetRoomType(randomRoom) != RoomType.Empty)
        {
            randomRoom = GetRandomRoom(max);
        }
        return randomRoom;
    }

    private void SetBorders(int size)
    {
        int left = 0;
        int right = size - 1;
        int upper = 0;
        int lower = size - 1;

        Border[upper, left] = Edge.CornerUpLeft;
        Border[lower, left] = Edge.CornerLowLeft;
        Border[upper, right] = Edge.CornerUpRight;
        Border[lower, right] = Edge.CornerLowRight;

        for (int i = 1; i < size - 1; i++)
        {
            Border[i, left] = Edge.Left;
            Border[i, right] = Edge.Right;
            Border[upper, i] = Edge.Upper;
            Border[lower, i] = Edge.Lower;
        }
    }
}
