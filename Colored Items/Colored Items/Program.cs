Colored<Sword> blueSword = new(new Sword(), ConsoleColor.Blue);

Colored<Bow> redBow = new(new Bow(), ConsoleColor.Red);

Colored<Axe> greenAxe = new(new Axe(), ConsoleColor.Green);

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