
using System.Xml;

internal class Program
{
    private static void Main(string[] args)
    {
        var myColor = new Color(240, 40, 40);
        var presetColor = Color.White;

        Console.WriteLine($"My color's channels are ({myColor.R}, {myColor.G}, {myColor.B})");
        Console.WriteLine($"The preset color's channels are ({presetColor.R}, {presetColor.G}, {presetColor.B})");
        Console.ReadLine();
    }
}

internal class Color
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }

    public static Color White { get; } = new(255, 255, 255);
    public static Color Black { get; } = new(0, 0, 0);
    public static Color Red { get; } = new(255,0,0);
    public static Color Orange { get; } = new(255,165, 0);
    public static Color Yellow { get; } = new(255, 255, 0);
    public static Color Green { get; } = new(0, 128, 0);
    public static Color Blue { get; } = new(0, 0, 255);
    public static Color Purple { get; } = new(128, 0, 128);




    public Color(byte red, byte green, byte blue)
    {
        R = LimitRange(red);
        G = LimitRange(green);
        B = LimitRange(blue);
    }



    private static byte LimitRange(byte input)
    {
        byte value;
        if (input < 0) value = 0;
        else if (input > 255) value = 255;
        else value = input;
        return value;  
    }
}