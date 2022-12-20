var pointA = new Point(2, 3);
var pointB = new Point(-4, 0);

Console.WriteLine($"Point A rests at ({pointA.X}, {pointA.Y})");
Console.WriteLine($"Point B rests at ({pointB.X}, {pointB.Y})");

internal class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point()
    {
        X = 0;
        Y = 0;
    }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}