using TMPro;
using UnityEngine;

public class ColorConTexto : MonoBehaviour
{
    public TextMeshPro texto;
    private MeshRenderer mesh;
    // se cogera del manager
    public bool daltonismo = false;

    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void SetColor(Color color)
    {
        mesh.material.color = color;
        texto.text = NombreColor(color);
    }

    private string NombreColor(Color color)
    {
        if (daltonismo == false) return "";
        if (color == Color.red) return ":P";
        if (color == Color.blue) return ":(";
        if (color == Color.green) return ":D";
        if (color == Color.yellow) return "XD";
        if (color == Color.purple) return ":/";

        return "?";
    }
}