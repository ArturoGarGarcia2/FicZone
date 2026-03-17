using System.Collections.Generic;
using UnityEngine;

public class SpawnCubosPuzzle : MonoBehaviour
{
    public GeneradorColores generador;
    public Transform[] posiciones; // Tus 9 empties

    // Prefabs por color
    public GameObject cuboRojo;
    public GameObject cuboVerde;
    public GameObject cuboAzul;
    public GameObject cuboAmarillo;
    public GameObject cuboMorado;

    private List<Color> secuencia;

    private void Start()
    {
        generador.GenerarPuzzle();
        secuencia = generador.getSecuenciaCorrecta();
        Debug.Log("cubosSpawn :" +secuencia.Count);
        InstanciarCubos();
    }

    void InstanciarCubos()
    {
        for (int i = 0; i < secuencia.Count; i++)
        {
            Debug.Log(secuencia[i]);
            GameObject prefab = ObtenerPrefabPorColor(secuencia[i]);

            if (prefab != null)
            {
                Instantiate(prefab, posiciones[i].position, posiciones[i].rotation);
            }
            else
            {
                Debug.Log("No hay prefab para el color: " + secuencia[i]);
            }
        }
    }

    GameObject ObtenerPrefabPorColor(Color color)
    {
        if (color == Color.red) return cuboRojo;
        if (color == Color.green) return cuboVerde;
        if (color == Color.blue) return cuboAzul;
        if (color == Color.yellow) return cuboAmarillo;
        if (color == Color.purple) return cuboMorado;

        return null;
    }
}