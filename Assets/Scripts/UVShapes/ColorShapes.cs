using System.Collections.Generic;
using UnityEngine;

public static class ColorShapes
{
    public static Dictionary<Colors, string> shapes = new Dictionary<Colors, string>()
    {
       { Colors.RED, ":P" },
        { Colors.YELLOW, "XD" },
        { Colors.BLUE, ":(" },
        { Colors.GREEN, ":D" },
        { Colors.PURPLE, ":/" } 
    };

    public static Dictionary<string, Colors> shapesToColor = new Dictionary<string, Colors>()
    {
       { ":P", Colors.RED },
        { "XD", Colors.YELLOW },
        { ":(", Colors.BLUE },
        { ":D", Colors.GREEN },
        { ":/", Colors.PURPLE }
    };
}