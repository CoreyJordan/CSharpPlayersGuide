Coordinate A = new(1, 2);
Coordinate B = new(0, 0);
Coordinate C = new(5, 5);
Coordinate D = new(0, 1);

Console.WriteLine($"A is next to B {A.IsAdjacent(B)} expected: false");
Console.WriteLine($"A is next to C {A.IsAdjacent(C)} expected: false");
Console.WriteLine($"A is next to D {A.IsAdjacent(D)} expected: true (diagonally)");
Console.WriteLine($"B is next to C {B.IsAdjacent(C)} expected: false");
Console.WriteLine($"B is next to D {B.IsAdjacent(D)} expected: true");
Console.WriteLine($"C is next to D {C.IsAdjacent(D)} expected: false");
Console.ReadLine();

public struct Coordinate
{
    public float X { get; }
    public float Y { get;}

    public Coordinate(float x, float y)
    {
        X = x;
        Y = y;
    }

    public bool IsAdjacent(Coordinate other)
    {
        bool isAdjacent = false;
        if ((other.X >= X - 1 && other.X <= X + 1) &&
            (other.Y >= Y - 1 && other.Y <= Y + 1))
        {
            isAdjacent = true;
        }
        return isAdjacent;
    }
}