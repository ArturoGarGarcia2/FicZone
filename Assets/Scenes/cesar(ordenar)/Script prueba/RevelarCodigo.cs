using TMPro;
using UnityEngine;

public class RevelarCodigo : MonoBehaviour
{
    public Transform otraHoja;
    public TextMeshProUGUI textoHoja;
    public TextMeshProUGUI textoOtraHoja;
    public float distanciaActivacion = 0.2f;


    private void Update()
    {
        float distancia = Vector3.Distance(transform.position, otraHoja.transform.position);


        if (distancia < distanciaActivacion)
        {
            float visibilidad = 1 - (distancia / distanciaActivacion);
            ActualizarAlpha(visibilidad);
        }
        else
        {
            ActualizarAlpha(0);
        }
    }

    void ActualizarAlpha(float nivel)
    {
        Color c1 = textoHoja.color;
        c1.a = nivel;
        textoHoja.color = c1;

        Color c2 = textoOtraHoja.color;
        c2.a = nivel;
        textoOtraHoja.color = c2; 

    }
}
