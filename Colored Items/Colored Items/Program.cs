Sword sword = new();
Colored<Sword> blueSword = new(sword, ConsoleColor.Blue);

Bow bow = new();
Colored<Bow> redBow = new(bow, ConsoleColor.Red);

Axe axe = new();
Colored<Axe> greenAxe = new(axe, ConsoleColor.Green);

blueSword.Display();
redBow.Display();
greenAxe.Display();

public class Sword { }
public class Bow { }
public class Axe { }

public class Colored<T>
{
    public T Item { get; }
    public ConsoleColor ItemColor { get; }

    public Colored(T item, ConsoleColor itemColor)
    {
        Item = item;
        ItemColor = itemColor;
    }

    public void Display()
    {
        Console.ForegroundColor = ItemColor;
        Console.WriteLine(Item.ToString());
        Console.ForegroundColor = ConsoleColor.White;
    }
}