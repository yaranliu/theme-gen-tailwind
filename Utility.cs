using System.Drawing;

namespace ThemeGenerator;

public static class Utility
{
    private const ConsoleColor StdColor = ConsoleColor.Gray;
    private const ConsoleColor ErrorColor = ConsoleColor.Red;
    private const ConsoleColor InfoColor = ConsoleColor.Yellow;
    public static void PrintHelp()
    {
        var lines = new string[]
        {
            "Usage:",
            "---------------------",
            "ThemeGenerator <inFile.json> <outFile.scss>",
            "Default inFile : theme.json",
            "Default outFile: theme.scss",
            "",
            "inFile may be in any path, outFile will be created in the working directory.",
            "---------------------"
        };
        Console.ForegroundColor = InfoColor;
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
    }

    public static void PrintError(object error)
    {
        Console.ForegroundColor = ErrorColor;
        Console.WriteLine(error.ToString());
    }
    
    public static void PrintInfo(string message)
    {
        Console.ForegroundColor = InfoColor;
        Console.WriteLine(message);
    }

    public static void PrintMessage(string message)
    {
        Console.ForegroundColor = StdColor;
        Console.WriteLine(message);
    }
}