internal static class Display
{
    public static void WriteLine(string text, ConsoleColor color)
    {
        ForegroundColor = color;
        Console.WriteLine(text);
        ForegroundColor = ConsoleColor.Gray;
    }
}
