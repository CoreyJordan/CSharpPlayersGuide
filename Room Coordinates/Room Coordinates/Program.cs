Coordinate A = new(1, 2);
Coordinate B = new(0, 0);
Coordinate C = new(5, 5);
Coordinate D = new(0, 1);
Coordinate E = new(5, 5);

Console.WriteLine($"A is next to B {A.IsAdjacent(B)} expected: false");
Console.WriteLine($"A is next to C {A.IsAdjacent(C)} expected: false");
Console.WriteLine($"A is next to D {A.IsAdjacent(D)} expected: true (diagonally)");
Console.WriteLine($"B is next to C {B.IsAdjacent(C)} expected: false");
Console.WriteLine($"B is next to D {B.IsAdjacent(D)} expected: true");
Console.WriteLine($"C is next to D {C.IsAdjacent(D)} expected: false");
Console.WriteLine($"A is next to A {A.IsAdjacent(A)} expected: false, self");
Console.WriteLine($"C is next to E {C.IsAdjacent(E)} expected: false, same coord");



Console.ReadLine();

public readonly struct Coordinate
{
    public float X { get; }
    public float Y { get;}

    public Coordinate(float x, float y)
    {
        X = x;
        Y = y;
    }

    // We could make this static and pass in both coordinates but it makes
    // sense to me to say "Is (This) coordinate next to the (other) coordinate.
    public bool IsAdjacent(Coordinate other)
    {
        bool isAdjacent = false;
        // The same coordinate is not adjacent to itself.
        if (other.X == X && other.Y == Y)
        {
            isAdjacent = false;
        }
        else if ((other.X >= X - 1 && other.X <= X + 1) &&
            (other.Y >= Y - 1 && other.Y <= Y + 1))
        {
            isAdjacent = true;
        }
        return isAdjacent;
    }
}