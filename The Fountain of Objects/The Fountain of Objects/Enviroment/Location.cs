namespace The_Fountain_of_Objects.Enviroment;
internal record Location
{
    public int Row { get; set; }
    public int Col { get; set; }

    public Location(int row, int col)
    {
        Row = row;
        Col = col;
    }
}
