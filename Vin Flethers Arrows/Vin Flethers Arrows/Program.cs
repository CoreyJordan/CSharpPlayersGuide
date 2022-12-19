
bool buyArrows = true;
while (buyArrows)
{
    // Get the user to make an arrow or choose presets.
    Console.WriteLine($"Design an arrow or choose a pre-made arrow.");
    Console.Write("1: Design, 2: Elite, 3: Beginner, or 4: Marksman...");
    string userChoice = Console.ReadLine();
    var userArrow = new Arrow();

    if (userChoice == "2") userArrow = Arrow.CreateEliteArrow();
    else if (userChoice == "3") userArrow = Arrow.CreateBeginnerArrow();
    else if (userChoice == "4") userArrow = Arrow.CreateMarksManArrow();
    else if (userChoice == "1")
    {
        // Get user to choose arrowhead.
        Console.Write($"Pick an arrowhead: 1: {Head.Wood}," +
        $" 2: {Head.Steel}, or 3: {Head.Obsidian}...");


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

        userArrow = new(arrowhead, shaftLengthCm, fletching);
    }
    Console.WriteLine($"A {userArrow.ShaftLength} cm arrow with a head made of " +
        $"{userArrow.ArrowHead} and {userArrow.Fletching} fletching will run you " +
        $"about {userArrow.GetCost():n0} gold.");
    Console.WriteLine();

    Console.Write("Buy another arrow? Y/N...");
    string answer = Console.ReadLine()!;

    if (answer.ToUpper() != "Y") buyArrows = false;
}



class Arrow
{
    public Head ArrowHead { get; }
    public int ShaftLength { get; }
    public Fletch Fletching { get; }

    public Arrow()
    {
        ArrowHead = Head.Unknown;
        ShaftLength = 0;
        Fletching = Fletch.Unknown;
    }

    public Arrow(Head arrowhead, int shaftLength, Fletch fletching)
    {
        ArrowHead = arrowhead;
        if (shaftLength < 60) ShaftLength = 60;
        else if (shaftLength > 100) ShaftLength = 100;
        else ShaftLength = shaftLength;
        Fletching = fletching;
    }

    /// <summary>
    /// Calculates the cost of an arrow based on its head material, shaft
    /// length, and fletching material.
    /// </summary>
    /// <returns>Float value of the arrow in gold.</returns>
    public float GetCost()
    {
        return GetHeadCost(ArrowHead) +
            GetShaftCost(ShaftLength) +
            GetFletchingCost(Fletching);
    }

    public static Arrow CreateEliteArrow()
    {
        return new Arrow(Head.Steel, 95, Fletch.Plastic);
    }

    public static Arrow CreateBeginnerArrow()
    {
        return new Arrow(Head.Wood, 75, Fletch.Goose);
    }

    public static Arrow CreateMarksManArrow()
    {
        return new Arrow(Head.Steel, 65, Fletch.Goose);
    }

    private static float GetHeadCost(Head arrowhead)
    {
        return arrowhead switch
        {
            Head.Steel => 10F,
            Head.Wood => 3F,
            Head.Obsidian => 5F,
            _ => 0F
        };
    }

    private static float GetShaftCost(int shaftLength)
    {
        return shaftLength * 0.05F;
    }

    private static float GetFletchingCost(Fletch fletching)
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