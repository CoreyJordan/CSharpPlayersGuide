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
    public Grid(int mapSize, Location start, int pitQty)
    {
        // Create the game grid.
        Map = new RoomType[mapSize, mapSize];
        Border = new Edge[mapSize, mapSize];
        SetBorders(mapSize);

        // Set the entrance location.
        Map[start.Row, start.Col] = RoomType.Entrance;

        // Set the fountain location.
        Location fountain = SetRoom(mapSize);
        Map[fountain.Row, fountain.Col] = RoomType.Fountain;

        // Set the pit locations.
        for (int i = 0; i < pitQty; i++)
        {
            Location pit = SetRoom(mapSize);
            Map[pit.Row, pit.Col] = RoomType.Pit;
        }
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

    /// <summary>
    /// Compile a list of adjacent room types to the present location.
    /// </summary>
    public List<RoomType> GetAdjacentTypes(Location location, int size)
    {
        List<RoomType> types = new();

        // Collect all possible adjacent locations.
        List<Location> adjLocations = new()
        {
            new(location.Row - 1, location.Col),
            new(location.Row + 1, location.Col),
            new(location.Row, location.Col + 1),
            new(location.Row, location.Col - 1)
        };

        // Check if adjacent location actually on the grid.
        foreach (var adjLocation in adjLocations)
        {
            if (adjLocation.Row >= 0 && adjLocation.Row < size &&
                adjLocation.Col >= 0 && adjLocation.Col < size) 
            {
                types.Add(GetRoomType(adjLocation));
            }
        }
        return types;
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
