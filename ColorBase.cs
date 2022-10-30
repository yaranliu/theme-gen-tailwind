namespace ThemeGenerator;

public class ColorBase
{
    public string Name { get; set; } = "";
    public string Base { get; set; } = "";
    public int HueVariation { get; set; } = 10;
    public int SaturationVariation { get; set; } = 10;
    public int LighterVariation { get; set; } = 10;
    public int DarkerVariation { get; set; } = 10;
}