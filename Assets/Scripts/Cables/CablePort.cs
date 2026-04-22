using UnityEngine;

public class CablePort : MonoBehaviour
{
    public bool conected;
    public bool correct;
    public Colors color;

    public Renderer light;

    public Material lightBulb;     
    public Material correctMaterial; 

    public CableHead currentCable;

    void Start()
    {
        TurnOffVisual();
    }

    private void OnTriggerEnter(Collider other)
    {
        CableHead cable = other.GetComponent<CableHead>();
        if (cable == null) return;

        conected = true;
        currentCable = cable;

        if (cable.color == color)
        {
            correct = true;
            TurnOnCorrect();   
        }
        else
        {
            correct = false;
            TurnOffVisual();   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CableHead cable = other.GetComponent<CableHead>();
        if (cable != null)
        {
            TurnOffVisual();
            conected = false;
            correct = false;
            currentCable = null;
        }
    }

    void TurnOnCorrect()
    {
        if (light == null) return;

        if (correctMaterial != null)
        {
            light.material = correctMaterial;
        }
    }

    void TurnOffVisual()
    {
        if (light == null) return;

        if (lightBulb != null)
        {
            light.material = lightBulb;
        }
    }
}