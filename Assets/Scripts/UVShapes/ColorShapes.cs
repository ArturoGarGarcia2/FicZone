using System.Collections.Generic;
using UnityEngine;

public static class ColorShapes
{
    public static Dictionary<Colors, string> shapes = new Dictionary<Colors, string>()
    {
       { Colors.RED, "Ω" },
        { Colors.YELLOW, "X" },
        { Colors.BLUE, "α" },
        { Colors.GREEN, "β" },
        { Colors.PURPLE, "Σ" } 
    };

    public static Dictionary<string, Colors> shapesToColor = new Dictionary<string, Colors>()
    {
       { "Ω", Colors.RED },
        { "X", Colors.YELLOW },
        { "α", Colors.BLUE },
        { "β", Colors.GREEN },
        { "Σ", Colors.PURPLE }
    };
}
