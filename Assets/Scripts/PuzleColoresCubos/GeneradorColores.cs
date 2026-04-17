using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GeneradorColores : MonoBehaviour
{
    public GameObject[] cubosHojaA;
    public GameObject[] cubosHojaB;
    [SerializeField] private Color[] colores = {Color.red, Color.green, Color.blue, Color.yellow, Color.purple};
    // los simbolos son para los daltonicos y se pondran encima de cubo color
    //public Symbol[] simbolos = { cuadrado, hexagono, triangulo, circulo, pentagono};
    private List<Color> cubosPuzzle = new List<Color>();


    public bool daltonismo = false;
    private GameObject configManager;

    [Range(0, 1)] public float probabilidadDeCubo = 0.5f;

    private void Start()
    {
        configManager = GameObject.FindWithTag("Config");
        daltonismo = configManager.GetComponent<ConfigManager>().modoDaltonico;
        GenerarPuzzle();
    }
    public async void GenerarPuzzle()
    {        
        WaitForSeconds espera = new WaitForSeconds(5.5f);
        daltonismo = configManager.GetComponent<ConfigManager>().modoDaltonico;
        for (int i = 0; i < 9; i++)
        {
            Color colorAleatorio = colores[Random.Range(0, colores.Length)];
            if (Random.value > probabilidadDeCubo)
            {
                ConfigurarCubo(cubosHojaA[i], colorAleatorio, true);
                ConfigurarCubo(cubosHojaB[i], Color.clear, false);

            }
            else
            {
                ConfigurarCubo(cubosHojaA[i], Color.clear, false);
                ConfigurarCubo(cubosHojaB[i], colorAleatorio, true);
            }
            cubosPuzzle.Add(colorAleatorio);
        }
    }
    void ConfigurarCubo(GameObject cubo, Color Col, bool activo)
    {
        cubo.SetActive(activo);
        if (activo)
        {
            //cubo.GetComponent<MeshRenderer>().material.color = Col;
            cubo.GetComponent<ColorConTexto>().SetColor(Col, daltonismo);
        }
    }

    public List<Color> getSecuenciaCorrecta()
    {
        return cubosPuzzle;
    }
    public Color[] GetColores()
    {
        return colores;
    }
}
