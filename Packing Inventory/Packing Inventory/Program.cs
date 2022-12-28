

bool cont = true;
var pack = new Pack(5, 10, 20);

while (cont)
{
    Console.WriteLine($"The {pack} has {pack.ItemsCount}/{pack.MaxItems}" +
        $" items, {pack.CurrentVolume}/{pack.MaxVolume} space used, and " +
        $"is {pack.CurrentWeight}/{pack.MaxWeight} lbs.");
    Console.Write("\tWhat would you like to add?\n\t" +
        "1: Arrow\n\t" +
        "2: Bow\n\t" +
        "3: Rope\n\t" +
        "4: Water\n\t" +
        "5: Rations\n\t" +
        "6: Sword\n...");

    try
    {
        // Route user's choice to the appropriate Inventory Item.
        InventoryItem item = Console.ReadLine() switch
        {
            "1" => new Arrow(),
            "2" => new Bow(),
            "3" => new Rope(),
            "4" => new Water(),
            "5" => new Rations(),
            "6" => new Sword(),
            _ => throw new Exception("Invalid choice")
        };

        if (pack.Add(item))
        {
            SuccessMessage($"{item} added successfully.");
        }
        else
        {
            FailMessage($"Could not add {item}");
        }
    }
    catch
    {
        FailMessage("Hmm. Didn't quite catch that.");
    }
    Console.WriteLine();
    
}

void SuccessMessage(string prompt)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(prompt);
    Console.ForegroundColor = ConsoleColor.White;
}

void FailMessage(string prompt)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(prompt);
    Console.ForegroundColor = ConsoleColor.White;
}

/// <summary>
/// An object that can be placed inside a Pack.
/// </summary>
public class InventoryItem
{
    public float Weight { get; set; }
    public float Volume { get; set; }

    public InventoryItem(float weight, float volume)
    {
        Weight = weight;
        Volume = volume;
    }
}

/// <summary>
/// An specific object that can be placed inside a Pack.
/// </summary>
public class Arrow : InventoryItem
{
    public Arrow() : base(0.1F, 0.05F)
    {
    }

    /// <summary>
    /// Displays the object name.
    /// </summary>
    public override string ToString()
    {
        return "Arrow";
    }
}

/// <summary>
/// An specific object that can be placed inside a Pack.
/// </summary>
public class Bow : InventoryItem
{
    public Bow() : base(1F, 4F)
    {
    }

    /// <summary>
    /// Displays the object name.
    /// </summary>
    public override string ToString()
    {
        return "Bow";
    }
}

/// <summary>
/// An specific object that can be placed inside a Pack.
/// </summary>
public class Rope : InventoryItem
{
    public Rope() : base(1F, 1.5F)
    {
    }

    /// <summary>
    /// Displays the object name.
    /// </summary>
    public override string ToString()
    {
        return "Rope";
    }
}

/// <summary>
/// An specific object that can be placed inside a Pack.
/// </summary>
public class Water : InventoryItem
{
    public Water() : base(2F, 3F)
    {
    }

    /// <summary>
    /// Displays the object name.
    /// </summary>
    public override string ToString()
    {
        return "Water";
    }
}

/// <summary>
/// An specific object that can be placed inside a Pack.
/// </summary>
public class Rations : InventoryItem
{
    public Rations() : base(1F, 0.5F)
    {
    }

    /// <summary>
    /// Displays the object name.
    /// </summary>
    public override string ToString()
    {
        return "Rations";
    }
}

/// <summary>
/// An specific object that can be placed inside a Pack.
/// </summary>
public class Sword : InventoryItem
{
    public Sword() : base(5F, 3F)
    {
    }

    /// <summary>
    /// Displays the object name.
    /// </summary>
    public override string ToString()
    {
        return "Sword";
    }
}

/// <summary>
/// A container object that can hold Inventory Items and derived objects. Max 
/// item count, weight, and volume are established at creation and immutable.
/// </summary>
public class Pack
{
    public int MaxItems { get; }
    public float MaxWeight { get; }
    public float MaxVolume { get; }

    private InventoryItem[] _items;

    public int ItemsCount { get; private set; } = 0;
    public float CurrentWeight { get; private set; } = 0F;
    public float CurrentVolume { get; private set; } = 0F;

    public Pack(int maxItems, float maxWeight, float maxVolume)
    {
        MaxItems = maxItems;
        MaxWeight = maxWeight;
        MaxVolume = maxVolume;
        _items = new InventoryItem[MaxItems];
    }

    /// <summary>
    /// Attempt to insert an Inventory Item into the Pack. If capacity is
    /// exceeded, attempt will fail. 
    /// </summary>
    public bool Add(InventoryItem item)
    {
        bool added = true;
        if (ItemsCount >= MaxItems)
        {
            added = false;
            Console.WriteLine("No open slots in pack.");
        }
        else if (CurrentWeight + item.Weight > MaxWeight)
        {
            added = false;
            Console.WriteLine("Too heavy for pack.");
        }
        else if (CurrentVolume + item.Volume > MaxVolume)
        {
            added = false;
            Console.WriteLine("No room in pack.");
        }
        else
        {
            _items[ItemsCount] = item;
            ItemsCount++;
            CurrentVolume += item.Volume;
            CurrentWeight += item.Weight;
        }
        return added;
    }

    /// <summary>
    /// Displays the current contents of the pack.
    /// </summary>
    public override string ToString()
    {
        var contents = "Pack (";
        if (ItemsCount == 1)       
        {
            contents += _items[0].ToString() + ")";
        }
        else if (ItemsCount > 1)
        {
            for (int i = 0; i < ItemsCount - 1; i++)
            {
                contents += _items[i].ToString() + ", ";
            }
            contents += _items[ItemsCount - 1].ToString() + ")";
        }
        else
        {
            contents += "nothing)";
        }
        return contents;
    }
}