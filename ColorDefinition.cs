namespace ThemeGenerator;

public record ColorDefinition
{
    public string ClassName { get; set; }
    public string VariableName { get; set; }
    
    public (int, int, int) Hsl { get; set; }

    public ColorDefinition(string className, string variableName, bool useRawValue = false)
    {
        ClassName = className;
        VariableName = variableName;
    }

    public ColorDefinition(string className, int hue, int saturation, int lightness)
    {
        // Custom = true;
        ClassName = className;
        VariableName = "";
        Hsl = (hue, saturation, lightness);
    }

    public (int, int, int) ToRgb()
    {
        return FromHslToRgb(Hsl.Item1, Hsl.Item2, Hsl.Item3);
    }

    public string ToRgbString()
    {
        var rgb = ToRgb();
        return $"#{rgb.Item1:X2}{rgb.Item2:X2}{rgb.Item3:X2}";
    }
    
    public static (int, int, int) FromHslToRgb(int hue, int saturation, int lightness)
    {
        var h = (float) hue;
        var s = (float) saturation / 100;
        var l = (float) lightness / 100;

        var c = (1 - Math.Abs(2 * l - 1)) * s;
        var hp = h / 60;
        var x = c * (1 - Math.Abs(hp % 2 - 1));

        (float, float, float) rgb1;
        switch (hp)
        {
            case >= 0 and < 1:
                rgb1 = (c, x, 0);
                break;
            case >= 1 and < 2:
                rgb1 = (x, c, 0);
                break;
            case >= 2 and < 3:
                rgb1 = (0, c, x);
                break;
            case >= 3 and < 4:
                rgb1 = (0, x, c);
                break;
            case >= 4 and < 5:
                rgb1 = (x, 0, c);
                break;
            case >= 5 and < 6:
                rgb1 = (c, 0, x);
                break;
            default: rgb1 = (0, 0, 0); break;
        }

        var m = l - c / 2;

        var rf = 255 * (rgb1.Item1 + m);
        var rg = 255 * (rgb1.Item2 + m);
        var rb = 255 * (rgb1.Item3 + m);

        return ((int)rf, (int)rg, (int)rb);

    }

    public static string FromHslToRgbString(int hue, int saturation, int lightness)
    {
        var rgb = FromHslToRgb(hue, saturation, lightness);
        return $"#{rgb.Item1:X2}{rgb.Item2:X2}{rgb.Item3:X2}";
    }
    

    public override string ToString()
    {
        return $"Class: {ClassName}, Variable: {VariableName}, Hsl: {Hsl}, Rgb: {ToRgb()}";
    }
}