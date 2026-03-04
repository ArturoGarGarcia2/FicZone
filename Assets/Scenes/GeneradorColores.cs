using UnityEngine;

public class GeneradorColores : MonoBehaviour
{
    public Color[] colores = {Color.red, Color.green, Color.blue, Color.yellow};

    private void Start()
    {
        MeshRenderer[] hijos = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer colorHijo in hijos)
        {
            Color colorAleatorio = colores[Random.Range(0, colores.Length)];

            colorHijo.material.color = colorAleatorio;
        }
    }
}
