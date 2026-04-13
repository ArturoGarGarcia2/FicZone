using UnityEngine;

public class LightbulbColor : MonoBehaviour
{
    public Colors color;

    public Color GetUnityColor()
    {
        switch (color)
        {
            case Colors.RED: return Color.red;
            case Colors.GREEN: return Color.green;
            case Colors.BLUE: return Color.blue;
            case Colors.YELLOW: return Color.yellow;
            case Colors.PURPLE: return new Color(0.5f, 0f, 0.5f);
            default: return Color.white;
        }
    }
}