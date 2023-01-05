namespace The_Fountain_of_Objects.Senses;
internal class SensePit : IDescription
{
    public void DescribeSense(Game game)
    {                    
        Display.WriteLine("A draft of damp dank air brushes past, warning " +
            "you there could\nbe danger nearby",
            ConsoleColor.Yellow);
    }
}
