using UnityEngine;

public class CablePort : MonoBehaviour
{
    public bool conected;
    public bool correct;
    public Colors color;
    public Renderer light;

    public Material lightBulb;
    public Material colorMaterial;

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
            TurnOnVisual(Color.green);
        }
        else
        {
            correct = false;
            TurnOnVisual(Color.red); // 👈 feedback visual
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

    void SetEmission(Color color, float intensity) // La intensidad debe estar a 3 o casi no se ve. Se podría subir bastante más pero entonces el brillo es blanco y queda feo.
    {
        Renderer rend = light;
        if (rend == null) return;

        Material mat = rend.material;

        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", color * intensity);
    }

    void TurnOnVisual(Color emissionColor)
    {
        if (light == null) return;

        // 👉 Material encendido (uno solo)
        if (colorMaterial != null)
        {
            light.material = colorMaterial;
        }

        // 👉 Emisión opcional
        Material mat = light.material;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", emissionColor * 3f);
    }

    void TurnOffVisual()
    {
        if (light == null) return;

        // 👉 Material base
        if (lightBulb != null)
        {
            light.material = lightBulb;
        }

        // 👉 Quitar emisión
        Material mat = light.material;
        mat.SetColor("_EmissionColor", Color.black);
    }
}