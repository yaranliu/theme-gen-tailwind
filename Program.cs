using Newtonsoft.Json;
using ThemeGenerator;

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Theme Generator");
Console.ForegroundColor = ConsoleColor.White;

Console.WriteLine(args[0]);

var inFile = "theme.json";

var path = Path.Combine(Directory.GetCurrentDirectory(), inFile);
if (File.Exists(path))
{
    try
    {
        var configuration = JsonConvert.DeserializeObject<ThemeConfiguration>(File.ReadAllText(path));
        if (configuration == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Configuration error");
        }
        else
        {
            // Create palette based on Tailwind colors
            var palette = new Dictionary<string, string>();

            var tailwindPath = Path.Combine(Directory.GetCurrentDirectory(), configuration.TailwindColorPaletteFile);
            var tailwindLines = File.ReadLines(tailwindPath).ToArray();
            foreach (var tailwindLine in tailwindLines)
            {
                if (!string.IsNullOrWhiteSpace(tailwindLine))
                {
                    var ta = tailwindLine.Replace(';', ' ').Split(':');
                    palette.Add(ta[0].Trim(), ta[1].Trim());    
                }
            }
            
            var colorGroups = new List<ColorGroup>();
            ColorDefinition cd;

            var previousKey = "";
            if (configuration.Tailwind.Colors.Contains("all"))
            {
                var cg = new ColorGroup("", true, true);
                foreach (var p in palette)
                {
                    var key = p.Key[1..].Split("-")[0];
                    var cName = p.Key[1..];
                    if (key != previousKey) cg = new ColorGroup(p.Key, false, true);
                    cg.List.Add(new ColorDefinition($"{cName}", $"{cName}", true));
                    colorGroups.Add(cg);
                    previousKey = key;
                } 
            }
            else
            {
                foreach (var twc in configuration.Tailwind.Colors)
                {
                    if (palette.ContainsKey($"${twc}-50"))
                    {
                        var colorGroup = new ColorGroup(twc, false, true);
                        for (var i = 0; i < 10; i++)
                        {
                            var variation = i == 0 ? "50" : (i * 100).ToString();
                            colorGroup.List.Add(new ColorDefinition($"{twc}-{variation}", $"{twc}-{variation}", true));
                        }    
                        colorGroups.Add(colorGroup);
                    }
                
                }    
            }
            
            // Theme colors
            foreach (var colorSource in configuration.Theme)
            {
                var custom = string.IsNullOrWhiteSpace(colorSource.Var);
                
                // Add missing TWC
                if (!custom && colorGroups.FirstOrDefault(cg => cg.Name == colorSource.Var) == null)
                {
                    if (palette.ContainsKey($"${colorSource.Var}-50"))
                    {
                        var twcgGroup = new ColorGroup(colorSource.Var, false, true);
                        for (var i = 0; i < 10; i++)
                        {
                            var variation = i == 0 ? "50" : (i * 100).ToString();
                            twcgGroup.List.Add(new ColorDefinition($"{colorSource.Var}-{variation}", $"{colorSource.Var}-{variation}", true));
                        }    
                        colorGroups.Add(twcgGroup);
                    }
                }
                
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
            
            var outPath = Path.Combine(Directory.GetCurrentDirectory(), configuration.OutFile);
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
    catch (Exception exception)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("An error occured");
        Console.WriteLine(exception);
    }
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("colors.json file not found");
}