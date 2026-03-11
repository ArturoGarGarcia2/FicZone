using UnityEngine;

public class LightbulbColor : MonoBehaviour
{
    public Colors color;

    void Update()
    {
        switch (color)
        {
            case Colors.RED: transform.GetChild(1).GetComponent<Light>().color = Color.red; break;
            case Colors.GREEN: transform.GetChild(1).GetComponent<Light>().color = Color.green; break;
            case Colors.BLUE: transform.GetChild(1).GetComponent<Light>().color = Color.blue; break;
            case Colors.YELLOW: transform.GetChild(1).GetComponent<Light>().color = Color.yellow; break;
            case Colors.PURPLE: transform.GetChild(1).GetComponent<Light>().color = Color.purple; break;
            default: break;
        }
    }
}
