namespace ThemeGenerator;

public class ColorSource
{
    public string Name { get; set; }
    public string Var { get; set; }
    public HSLBase H { get; set; }
    public HSLBase S { get; set; }
    public HSLBase L { get; set; }
}

public class HSLBase 
{
    public int Base { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }
}