using The_Fountain_of_Objects;
using The_Fountain_of_Objects.Enviroment;

Game game = CreateGame();
game.Run();





Game CreateGame()
{
    Location start = new(0,0);
    Game game = new(GetMapSize(), start);

    return game;
}

int GetMapSize()
{
    int size = 0;
    while (size == 0)
    {
        Write("Select a dungeon size => small, medium, or large: ");
        string? mapChoice = ReadLine() ?? "";

        size = mapChoice switch
        {
            "small" => 4,
            "medium" => 6,
            "large" => 8,
            _ => 0
        };
    }
    return size;
}

