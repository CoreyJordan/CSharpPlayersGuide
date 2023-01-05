using System.Drawing;
using The_Fountain_of_Objects;
using The_Fountain_of_Objects.Enviroment;

Game game = CreateGame();
game.Run();



Game CreateGame()
{
    MapSize size = GetMapSize();

    int mapSize = SetMapSize(size);
    Location start = new(0,0);
    int pitQty = GetNumberofPits(size);
    int maelstromsQty = GetNumberofMaelstroms(size);

    Game game = new(mapSize, start, pitQty, maelstromsQty);

    return game;
}

MapSize GetMapSize()
{
    MapSize size = MapSize.None;
    while (size == MapSize.None)
    {
        Write("Select a dungeon size => small, medium, or large: ");
        string? mapChoice = ReadLine() ?? "";
        size = mapChoice switch
        {
            "small" => MapSize.Small,
            "medium" => MapSize.Medium,
            "large" => MapSize.Large,
            _=> MapSize.None
        };
    }
    return size;
}

int SetMapSize(MapSize choice)
{
    return choice switch
    {
        MapSize.Small => 4,
        MapSize.Medium => 6,
        MapSize.Large => 8,
        _ => 0
    };
}

int GetNumberofPits(MapSize choice)
{
    return choice switch
    {
        MapSize.Small => 1,
        MapSize.Medium => 2,
        MapSize.Large => 4,
        _=> 0
    };
}

int GetNumberofMaelstroms(MapSize choice)
{
    return choice switch
    {
        MapSize.Small => 1,
        MapSize.Medium => 1,
        MapSize.Large => 2,
        _ => 0
    };
}