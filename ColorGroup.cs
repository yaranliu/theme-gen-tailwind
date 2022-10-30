namespace ThemeGenerator;

public class ColorGroup
{
    public string Name { get; set; }
    public List<ColorDefinition> List { get; set; }

    public bool Custom { get; set; }
    public bool UseRawValue { get; set; }
    public ColorGroup(string name, bool custom, bool raw)
    {
        Name = name;
        List = new List<ColorDefinition>();
        Custom = custom;
        UseRawValue = raw;
    }
}