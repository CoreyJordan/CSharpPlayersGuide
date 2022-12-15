
bool buyArrows = true;
while (buyArrows)
{

    // Get user to choose arrowhead.
    Console.Write($"Pick an arrowhead: 1: {Head.Wood}," +
        $" 2: {Head.Steel}, or 3: {Head.Obsidian} ...");

    Head arrowhead = Console.ReadLine() switch
    {
        "1" => Head.Wood,
        "2" => Head.Steel,
        "3" => Head.Obsidian,
        _ => Head.Unknown
    };
    Console.WriteLine();

    // Get the user to enter the shaft length.
    int shaftLengthCm;
    while (true)
    {
        Console.Write($"Enter the desired shaft length: (Between 60 and 100 cm) ...");
        if (int.TryParse(Console.ReadLine(), out shaftLengthCm))
        {
            Console.WriteLine();
            break;
        }
    }

    // Get the user to choose the fletching.
    Console.Write($"Pick the fletching: 1: {Fletch.Goose}, 2: {Fletch.Plastic}," +
        $" or 3: {Fletch.Turkey} ... ");
    Fletch fletching = Console.ReadLine() switch
    {
        "1" => Fletch.Goose,
        "2" => Fletch.Plastic,
        "3" => Fletch.Turkey,
        _ => Fletch.Unknown
    };
    Console.WriteLine();

    Arrow userArrow = new(arrowhead, shaftLengthCm, fletching);

    Console.WriteLine($"A {userArrow.GetShaftLength()} cm arrow with a head made of " +
        $"{userArrow.GetArrowHead()} and {userArrow.GetFletching()} fletching will run you" +
        $"about {userArrow.GetCost():n0} gold.");
    Console.WriteLine();

    Console.Write("Buy another arrow? Y/N...");
    string answer = Console.ReadLine()!;
    if (answer.ToUpper() != "Y")
    {
        buyArrows = false;
    }
}



class Arrow
{
    private Head _arrowhead;
    private int _shaftLength;
    private Fletch _fletching;

    public Arrow()
    {
        _arrowhead = Head.Unknown;
        _shaftLength = 0;
        _fletching = Fletch.Unknown;
    }

    public Arrow(Head arrowhead, int shaftLength, Fletch fletching)
    {
        _arrowhead = arrowhead;
        if (shaftLength < 60) _shaftLength = 60;
        else if (shaftLength > 100) _shaftLength = 100;
        else _shaftLength = shaftLength;
        _fletching = fletching;
    }

    public Head GetArrowHead() => _arrowhead;
    public int GetShaftLength() => _shaftLength;
    public Fletch GetFletching() => _fletching;

    /// <summary>
    /// Calculates the cost of an arrow based on its head material, shaft
    /// length, and fletching material.
    /// </summary>
    /// <returns>Float value of the arrow in gold.</returns>
    public float GetCost()
    {
        return GetHeadCost(_arrowhead) +
            GetShaftCost(_shaftLength) +
            GetFletchingCost(_fletching);
    }

    static float GetHeadCost(Head arrowhead)
    {
        return arrowhead switch
        {
            Head.Steel => 10F,
            Head.Wood => 3F,
            Head.Obsidian => 5F,
            _ => 0F
        };
    }

    static float GetShaftCost(int shaftLength)
    {
        return shaftLength * 0.05F;
    }

    static float GetFletchingCost(Fletch fletching)
    {
        return fletching switch
        {
            Fletch.Goose => 3F,
            Fletch.Plastic => 10F,
            Fletch.Turkey => 5F,
            _ => 0F
        };
    }
}

enum Head
{ Unknown,
    Steel,
    Wood,
    Obsidian
}

enum Fletch
{
    Unknown,
    Plastic,
    Turkey,
    Goose
}