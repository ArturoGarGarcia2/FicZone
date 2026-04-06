using UnityEngine;

public class CablePort : MonoBehaviour
{
    public bool conected;
    public bool correct;
    public Colors color;

    public Light bulbLight;

    public CableHead currentCable;

    void Start()
    {
        bulbLight.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CableHead cable = other.GetComponent<CableHead>();
        if (cable == null) return;

        conected = true;
        currentCable = cable;


        if (cable.color == color)
            correct = true;
        else
            correct = false;
    }

    private void OnTriggerExit(Collider other)
    {
        CableHead cable = other.GetComponent<CableHead>();
        if (cable != null)
        {
            conected = false;
            correct = false;
            currentCable = null;
        }
    }

}