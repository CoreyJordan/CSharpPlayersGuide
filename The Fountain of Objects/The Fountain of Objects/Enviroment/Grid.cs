﻿namespace The_Fountain_of_Objects.Enviroment;
internal class Grid
{
    /// <summary>
    /// 2D grid of rooms by type.
    /// </summary>
    public Room[,] Map { get; set; }
    public Edge[,] Border { get; }

    /// <summary>
    /// Create a map of the chosen size and place starting positions.
    /// </summary>
    /// <param name="mapSize">The width and height of the map grid.</param>
    /// <param name="start">The starting position of the player.</param>
    public Grid(int mapSize,
                Location start,
                int pitQty,
                int maelQty,
                int amaroksQty)
    {
        // Create the game grid.
        Map = new Room[mapSize, mapSize];
        Border = new Edge[mapSize, mapSize];
        SetBorders(mapSize);

        // Set the entrance location.
        Map[start.Row, start.Col] = Room.Entrance;

        // Set the fountain location.
        Location fountain = SetRoom(mapSize);
        Map[fountain.Row, fountain.Col] = Room.Fountain;

        // Set the pit locations.
        for (int i = 0; i < pitQty; i++)
        {
            Location pit = SetRoom(mapSize);
            Map[pit.Row, pit.Col] = Room.Pit;
        }

        // Set Maelstrom locations.
        for (int i = 0; i < maelQty; i++)
        {
            Location maelstrom = SetRoom(mapSize);
            Map[maelstrom.Row, maelstrom.Col] = Room.Storm;
        }
        // Set Amaroks locations.
        for (int i = 0; i < amaroksQty; i++)
        {
            Location amarok = SetRoom(mapSize);
            Map[amarok.Row, amarok.Col] = Room.Amarok;
        }

    }

    /// <summary>
    /// Returns the room designation of a particular location.
    /// </summary>
    public Room GetRoomType(Location location)
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
    public List<Room> GetAdjacentTypes(Location location, int size)
    {
        List<Room> types = new();

        // Collect all possible adjacent locations.
        List<Location> adjLocations = new()
        {
            new(location.Row - 1, location.Col),
            new(location.Row + 1, location.Col),
            new(location.Row, location.Col + 1),
            new(location.Row, location.Col - 1)
        };

        // Check if adjacent location actually on the grid.
        foreach (var room in adjLocations)
        {
            if (room.Row >= 0 && room.Row < size &&
                room.Col >= 0 && room.Col < size) 
            {
                types.Add(GetRoomType(room));
            }
        }
        return types;
    }

    public List<Room> GetTangentTypes(Location location, int size)
    {
        List<Room> types = new();

        // Collect all possible tangent locations.
        List<Location> tanLocations = new()
        {
            new(location.Row - 1, location.Col - 1),
            new(location.Row - 1, location.Col + 1),
            new(location.Row + 1, location.Col - 1),
            new(location.Row + 1, location.Col + 1)
        };

        // Check list for existant rooms.
        foreach (var room in tanLocations)
        {
            if (room.Row >= 0 && room.Row < size &&
                room.Col >= 0 && room.Col < size)
            {
                types.Add(GetRoomType(room));
            }
        }
        return types;
    }

    /// <summary>
    /// Creates a random unique location.
    /// </summary>
    public Location SetRoom(int max)
    {
        var randomRoom = GetRandomRoom(max);
        while (GetRoomType(randomRoom) != Room.Empty)
        {
            randomRoom = GetRandomRoom(max);
        }
        return randomRoom;
    }

    private static Location GetRandomRoom(int max)
    {
        Random rand = new();
        return new(rand.Next(max), rand.Next(max));
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
