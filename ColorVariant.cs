namespace ThemeGenerator;

public class ColorVariant
{
    public string Name { get; set; }
    public string? Value { get; set; }

    public ColorVariant(string name, string? value)
    {
        Name = name;
        Value = value;
    }
}