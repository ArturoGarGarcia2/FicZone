using TMPro;
using UnityEngine;

public class ColorConTexto : MonoBehaviour
{
    public TextMeshPro texto;
    private MeshRenderer mesh;
    // se cogera del manager

    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void SetColor(Color color, bool daltonismo)
    {
        mesh.material.color = color;
        texto.text = NombreColor(color, daltonismo);
    }

    private string NombreColor(Color color, bool daltonismo)
    {


        if(daltonismo)
        {
            if (color == Color.red) return ":P";
            if (color == Color.blue) return ":(";
            if (color == Color.green) return ":D";
            if (color == Color.yellow) return "XD";
            if (color == Color.purple) return ":/";
        }
        else
            return "";
                    

        return "?";
    }
    
}