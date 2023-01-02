Sword basicSword = new(Material.Iron, GemStone.None, 35, 6);
Sword steelSword = basicSword with { Material = Material.Steel };
Sword amberSword = basicSword with { GemStone = GemStone.Amber };

Console.WriteLine(basicSword);
Console.WriteLine(steelSword);
Console.WriteLine(amberSword);
Console.ReadLine();

public record Sword(Material Material,
                    GemStone GemStone,
                    float Length,
                    float CrossGuardWidth);

public enum Material
{
    Wood,
    Bronze,
    Iron,
    Steel,
    Binarium
}

public enum GemStone
{
    None,
    Emerald,
    Amber,
    Sapphire,
    Diamond,
    Bitstone
}