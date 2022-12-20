Color[] colors= {Color.Red, Color.Green, Color.Blue, Color.Yellow};
Rank[] ranks = { Rank.One, Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven, 
        Rank.Eight, Rank.Nine, Rank.Ten, Rank.Ampersand, Rank.Cash, Rank.Percent, Rank.Carrot};

foreach (var color in colors)
{
    foreach (var rank in ranks)
    {
        var card = new Card(color, rank);
        Console.WriteLine($"The {card.Color} {card.Rank}.");
    }
}
Console.ReadLine();

public class Card
{
    public Color Color { get; set; }
    public Rank Rank { get; set; }

    public Card(Color color, Rank rank)
    {
        Color = color;
        Rank = rank;
    }

    public bool IsFaceCard()
    {
        if (Rank == Rank.Ampersand || Rank == Rank.Carrot ||
            Rank == Rank.Cash || Rank == Rank.Percent)
        {
            return true;
        }
        else return false;
    }
}

public enum Color
{
    Red, 
    Green,  
    Blue, 
    Yellow
}

public enum Rank
{
    One, 
    Two, 
    Three, 
    Four, 
    Five, 
    Six, 
    Seven,
    Eight,
    Nine, 
    Ten, 
    Cash, 
    Percent, 
    Carrot, 
    Ampersand
}