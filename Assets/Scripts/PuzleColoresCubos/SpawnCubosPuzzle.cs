using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;

public class SpawnCubosPuzzle : MonoBehaviour
{
    // variable de rueba para los daltonicos luego se cogera del manager
    [SerializeField] private bool daltonismo = false;

    // Prefabs por color
    public GameObject cuboRojo;
    public GameObject cuboVerde;
    public GameObject cuboAzul;
    public GameObject cuboAmarillo;
    public GameObject cuboMorado;

    public Transform posicionBaseTriangulo; // centro de la base del triangulo que sera de 4 3 2 1 y 1 total 11 cubos
    public Transform posicionBasePiramide; // centro de la base de la piramide que sera de 3x3 2x2 y 1 total 14 cubos
    private float separacion = 0.2f; // distancia entre cubos

    private Dictionary<GameObject, int> contadorColores;

    void Awake()
    {
        daltonismo = ConfigManager.instance.modoDaltonico;
    }

    private void Start()
    {
        //daltonismo = ConfigManager.instance.modoDaltonico;
        //GenerarTriangulo();
        GenerarContruccion();
    }
    public async void GenerarContruccion()
    {
        WaitForSeconds espera = new WaitForSeconds(5.5f);

        GameObject[] prefabs = { cuboRojo, cuboVerde, cuboAzul, cuboAmarillo, cuboMorado };
        if (!daltonismo)
        {
            foreach (GameObject pref in prefabs)
            {
                pref.GetComponentInChildren<TextMeshPro>().text = "";
            }
        }
        contadorColores = new Dictionary<GameObject, int>();
        foreach (GameObject p in prefabs)
            contadorColores[p] = 0;

        GenerarPiramide(prefabs);
        GenerarTriangulo(prefabs);
    }


    void GenerarPiramide(GameObject[] prefabs)
    {
        // Filas de la pir�mide: base 3x3, medio 2x2, cima 1
        int[] filas = { 3, 2, 1 }; // n�mero de cubos por lado de cada fila
        float altura = 0f;

        for (int f = 0; f < filas.Length; f++)
        {
            int cubosPorLado = filas[f];
            float offsetFila = (cubosPorLado - 1) * separacion / 2f; // para centrar la fila

            for (int x = 0; x < cubosPorLado; x++)
            {
                for (int z = 0; z < cubosPorLado; z++)
                {
                    GameObject prefab;
                    prefab = colorValido(prefabs);

                    float posX = posicionBasePiramide.position.x + x * separacion - offsetFila;
                    float posZ = posicionBasePiramide.position.z + z * separacion - offsetFila;
                    float posY = posicionBasePiramide.position.y + altura;

                    Vector3 pos = new Vector3(posX, posY, posZ);
                    Instantiate(prefab, pos, Quaternion.identity);
                }
            }

            altura += separacion; // subir a la siguiente fila
        }
    }
    void GenerarTriangulo(GameObject[] prefabs)
    {
        // Triangulo 4 3 2 1 1
        int[] filas = { 4, 3, 2, 1, 1 }; // cubos por fila
        float altura = 0f;

        for (int f = 0; f < filas.Length; f++)
        {
            int cubosFila = filas[f];
            float offsetFila = (cubosFila - 1) * separacion / 2f;

            for (int i = 0; i < cubosFila; i++)
            {
                GameObject prefab;
                prefab = colorValido(prefabs);

                float posX = posicionBaseTriangulo.position.x + i * separacion - offsetFila;
                float posZ = posicionBaseTriangulo.position.z;
                float posY = posicionBaseTriangulo.position.y + altura;

                Vector3 pos = new Vector3(posX, posY, posZ);
                Instantiate(prefab, pos, Quaternion.identity);
            }

            altura += separacion;
        }
    }
    GameObject colorValido(GameObject[] prefabs)
    {
        // Elegir color aleatorio respetando el l�mite de 5
        GameObject prefab = null;
        bool colorValido = false;

        while (!colorValido)
        {
            prefab = prefabs[Random.Range(0, prefabs.Length)];

            if (contadorColores[prefab] < 5)
            {
                contadorColores[prefab]++;
                colorValido = true;
            }
            // si ya hay 5 de ese color, vuelve a elegir
        }
        return prefab;
    }
}