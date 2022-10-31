using Newtonsoft.Json;
using ThemeGenerator;

string inFile;
string outFile;
string inPath;
string outPath;

inFile = "theme.json";
outFile = "theme.scss";

inPath = Path.Combine(Directory.GetCurrentDirectory(), inFile);
outPath = Path.Combine(Directory.GetCurrentDirectory(), outFile);

Utility.PrintInfo("Theme Generator");

if (args.Length > 0)
{
    if (args[0] == "-h" || args[0] == "-H")
    {
        Utility.PrintHelp();
        Environment.Exit(0);
    }
    else
    {
        inPath = args[0];
        if (!File.Exists(inPath))
        {
            Utility.PrintError($"{inPath} could not be located");
            Environment.Exit(0);
        }

        if (args.Length > 1)
        {
            outPath = Path.Combine(Directory.GetCurrentDirectory(), args[1]);
        }    
    }
    
}


Utility.PrintInfo($"INPUT  : {inPath}");
Utility.PrintInfo($"OUTPUT : {outPath}");

try
{
    var configuration = JsonConvert.DeserializeObject<ThemeConfiguration>(File.ReadAllText(inPath));
    if (configuration == null) Utility.PrintError("Configuration error");
    else
    {
        // Create palette based on Tailwind colors
        var palette = new Dictionary<string, string>();

        foreach (var tailwindColor in Theme.TailwindPalette)
        {
            if (tailwindColor.Variants != null)
            {
                foreach (var variant in tailwindColor.Variants)
                {
                    palette.Add($"${tailwindColor.Name}-{variant.Name}", $"{variant.Value}" );
                }    
            }
        }
            
        var colorGroups = new List<ColorGroup>();
        ColorDefinition cd;

        if (configuration.Tailwind != null && configuration.Tailwind.Colors != null)
        {
            if (configuration.Tailwind.Colors.Contains("all"))
            {
                foreach (var paletteColor in Theme.TailwindPalette)
                {
                    if (paletteColor.Name != null && paletteColor.Variants != null)
                    {
                        var colorGroup = new ColorGroup(paletteColor.Name, false, true);
                        foreach (var variant in paletteColor.Variants)
                        {
                            colorGroup.List.Add(new ColorDefinition($"{paletteColor.Name}-{variant.Name}", $"{paletteColor.Name}-{variant.Name}", true ));
                        }
                        colorGroups.Add(colorGroup);    
                    }
                }
            }
            else
            {
                foreach (var twc in configuration.Tailwind.Colors)
                {
                    var paletteColor = Theme.TailwindPalette.FirstOrDefault(p => p.Name == twc.ToLower());
                    if (paletteColor != null && paletteColor.Name != null && paletteColor.Variants != null)
                    {
                        var colorGroup = new ColorGroup(paletteColor.Name, false, true);
                        foreach (var variant in paletteColor.Variants)
                        {
                            colorGroup.List.Add(new ColorDefinition($"{paletteColor.Name}-{variant.Name}", $"{paletteColor.Name}-{variant.Name}", true ));
                        }
                        colorGroups.Add(colorGroup);
                    }
                }    
            }    
        }

        
            
        // Theme colors
        if (configuration.Theme != null)
        {
            foreach (var colorSource in configuration.Theme)
            {
                var custom = string.IsNullOrWhiteSpace(colorSource.Var);
                
                // Add missing TWC
                if (!custom && colorGroups.FirstOrDefault(cg => cg.Name == colorSource.Var) == null)
                {
                    if (colorSource.Var != null)
                    {
                        var paletteColor = Theme.TailwindPalette.FirstOrDefault(p => p.Name == colorSource.Var.ToLower());
                        if (paletteColor != null && paletteColor.Name != null && paletteColor.Variants != null)
                        {
                            var cg = new ColorGroup(paletteColor.Name, false, true);
                            foreach (var variant in paletteColor.Variants)
                            {
                                cg.List.Add(new ColorDefinition($"{paletteColor.Name}-{variant.Name}", $"{paletteColor.Name}-{variant.Name}", true ));
                            }
                            colorGroups.Add(cg);
                        }    
                    }
                    
                }

                if (colorSource.Name != null)
                {
                    var colorGroup = new ColorGroup(colorSource.Name, custom,  false);
                    colorGroups.Add(colorGroup);
                    if (!custom) // Tailwind class
                    {
                        cd = new ColorDefinition(colorSource.Name, $"{colorSource.Var}-500");
                        colorGroup.List.Add(cd);
                        palette.Add($"${cd.ClassName}", palette[$"${cd.VariableName}" ]);
                        cd = new ColorDefinition($"{colorSource.Name}-50", $"{colorSource.Var}-50");
                        palette.Add($"${cd.ClassName}", palette[$"${cd.VariableName}" ]);
                        colorGroup.List.Add(cd);
                        for (var i = 1; i < 10; i++)
                        {
                            cd = new ColorDefinition($"{colorSource.Name}-{i * 100}",
                                $"{colorSource.Var}-{i * 100}");
                            colorGroup.List.Add(cd);
                            palette.Add($"${cd.ClassName}", palette[$"${cd.VariableName}" ]);
                        }
                    }
                    else // Custom class
                    {
                        if (colorSource.H != null && colorSource.S != null && colorSource.L != null)
                        {
                            var hueLowerScale = (float)(colorSource.H.Base - colorSource.H.Min) / 5;
                            var hueUpperScale = (float)(colorSource.H.Max - colorSource.H.Base) / 4;

                            var satLowerScale = (float)(colorSource.S.Base - colorSource.S.Min) / 5;
                            var satUpperScale = (float)(colorSource.S.Max - colorSource.S.Base) / 4;

                            var lighterScale  = (float)(colorSource.L.Max - colorSource.L.Base) / 5;
                            var darkerScale = (float)(colorSource.L.Base - colorSource.L.Min) / 4;

                            cd = new ColorDefinition($"{colorSource.Name}", colorSource.H.Base, colorSource.S.Base,
                                colorSource.L.Base);
                            colorGroup.List.Add(cd);
                            palette.Add($"${cd.ClassName}", cd.ToRgbString());
                    
                            for (var i = 0; i < 10; i++)
                            {
                                var index = 5 - i;
                                int h;
                                int s;
                                int l;
                                if (index == 0)
                                {
                                    h = colorSource.H.Base;
                                    s = colorSource.S.Base;
                                    l = colorSource.L.Base;
                                }
                                else if (index > 0)
                                {
                                    h = colorSource.H.Base - (int)(index * hueLowerScale);
                                    s = colorSource.S.Base - (int)(index * satLowerScale);
                                    l = colorSource.L.Base + (int)(index * lighterScale);
                                }
                                else
                                {
                                    h = colorSource.H.Base - (int)(index * hueUpperScale);
                                    s = colorSource.S.Base - (int)(index * satUpperScale);
                                    l = colorSource.L.Base + (int)(index * darkerScale);
                                }

                                var variation = i == 0 ? "50" : (i * 100).ToString();
                                cd = new ColorDefinition($"{colorSource.Name}-{variation}", h, s, l);
                                colorGroup.List.Add(cd);
                                palette.Add($"${cd.ClassName}", cd.ToRgbString());    
                            }
                    
                        }
                    }
                }
                
            }
        }
        
            
            
        // OUTPUT
        await using StreamWriter fileStream = new(outPath);
        await fileStream.WriteLineAsync("// Generated by TCG");
        string line;
            
        foreach (var colorGroup in colorGroups)
        {
            var maxLength = colorGroup.Name.Length + 5;
            await fileStream.WriteLineAsync($"\r\n// {colorGroup.Name.ToUpper()} Color");
            foreach (var colorDefinition in colorGroup.List)
            {
                line = $"${colorDefinition.ClassName}".PadRight(maxLength) + ": ";
                if (colorGroup.Custom)
                {
                    var hslString = $"HSL({colorDefinition.Hsl.Item1.ToString()}, {colorDefinition.Hsl.Item2}%, {colorDefinition.Hsl.Item3}%);";
                    var rgbString = $"{colorDefinition.ToRgbString()};"; 

                    if (configuration.ColorModelIsHsl) line += $"{hslString} // {rgbString}";
                    else line += $"{rgbString} // {hslString}";
                }
                else
                {
                    var paletteColor = palette["$" + colorDefinition.VariableName];
                    if (configuration.GenerateRawValues || colorGroup.UseRawValue)
                    {
                        line += $"{paletteColor}; // {colorDefinition.ClassName}";    
                    }
                    else line +=  $"{colorDefinition.VariableName}; // {paletteColor}";
                }
                
                await fileStream.WriteLineAsync(line);
            }

            Dictionary<string, string> classMap = new Dictionary<string, string>()
            {
                { "bg", "background-color" },
                { "text", "color" },
                { "accent", "accent-color" },
                { "outline", "outline-color" },
            };

            if (configuration.Classes != null)
            {
                foreach (var colorClass in configuration.Classes)
                {
                    await fileStream.WriteLineAsync($"\r\n// {colorClass.ToUpper()} Classes for {colorGroup.Name.ToUpper()}");
                    maxLength = colorGroup.Name.Length + colorClass.Length + 6;
                    foreach (var colorDefinition in colorGroup.List)
                    {
                        var cName  = $"{colorClass}-{colorDefinition.ClassName}".PadRight(maxLength);
                        var cValue = configuration.GenerateRawValues ? $"{palette[$"${colorDefinition.ClassName}"]}" : $"${colorDefinition.ClassName}";
                        line = $".{cName} {{ {classMap[colorClass]}: {cValue} }}";
                        await fileStream.WriteLineAsync(line);
                    }
                }    
            }
            
        }
    }
}
catch (Exception exception)
{
    Utility.PrintError("An error occured");
    Utility.PrintError(exception);
}