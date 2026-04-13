using UnityEngine;

public class CablePort : MonoBehaviour
{
    public bool conected;
    public bool correct;
    public Colors color;
    public Renderer light;

    public CableHead currentCable;

    void Start()
    {
        TurnOffEmission();
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
            SetEmission(Color.green, 3f);
        }
        else
            correct = false;
    }

    private void OnTriggerExit(Collider other)
    {
        CableHead cable = other.GetComponent<CableHead>();
        if (cable != null)
        {
            TurnOffEmission();
            conected = false;
            correct = false;
            currentCable = null;
        }
    }

    void SetEmission(Color color, float intensity) // La intensidad debe estar a 3 o casi no se ve. Se podría subir bastante más pero entonces el brillo es blanco y queda feo.
    {
        Renderer rend = light;
        if (rend == null) return;

        Material mat = rend.material;

        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", color * intensity);
    }

    void TurnOffEmission()
    {
        Renderer rend = light;
        if (rend == null) return;

        Material mat = rend.material;

        mat.SetColor("_EmissionColor", Color.black);
    }

}