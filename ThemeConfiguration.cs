namespace ThemeGenerator;

public class ThemeConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    public string TailwindColorPaletteFile { get; set; } = "";
    /// <summary>
    /// Generate HSL instead of RGB Values
    /// </summary>
    public string ColorModel { get; set; } = "hsl"; 
    

    public string[]? Classes { get; set; }
    /// <summary>
    /// Instead of variable names, use RGB/HSL values as final
    /// values e.g. $variable1: $variable2 -> $variable1: #FFFFFF
    /// </summary>
    public bool GenerateRawValues { get; set; } = true; 
    public ColorSource[]? Theme { get; set; }
    public TailwindConfiguration? Tailwind { get; set; }
    
    public bool ColorModelIsHsl => ColorModel.Trim().ToLower() == "hsl";
    public bool ColorModelIsRgb => ColorModel.Trim().ToLower() == "rgb";
}