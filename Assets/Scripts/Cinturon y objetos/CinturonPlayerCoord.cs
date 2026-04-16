using System;
using Unity.Mathematics;
using UnityEngine;

public class CinturonPlayerCoord : MonoBehaviour
{

    public GameObject jugador, configManager;

    private ControlesExternos controles;

    public float desfaseRotacion, tamaño, desfasePosicion;

    public int vecesGiradas, rectificarGiro;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        configManager = GameObject.FindWithTag("Config");

        jugador = GameObject.FindGameObjectWithTag("Player");

        controles = jugador.GetComponent<ControlesExternos>();

        this.gameObject.transform.position = jugador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        desfasePosicion = configManager.GetComponent<ConfigManager>().alturaCinturon;

        desfaseRotacion = configManager.GetComponent<ConfigManager>().rotacionCinturon;

        //this.gameObject.transform.rotation = quaternion.Euler(jugador.transform.rotation.x + controles.rotacionCabeza.x, jugador.transform.rotation.y + controles.rotacionCabeza.y + desfaseRotacion, jugador.transform.rotation.z + controles.rotacionCabeza.z);

        if (configManager.GetComponent<ConfigManager>().rotacionASaltos)
        {
            if (controles.GirarDer)
            {
                vecesGiradas++;
                desfaseRotacion += VicGenLib.Calc.Angles.NormalToEulerSingleAngle(45f * vecesGiradas);
            }

            if (controles.GirarIzq)
            {
                vecesGiradas--;
                desfaseRotacion += VicGenLib.Calc.Angles.NormalToEulerSingleAngle(45f * vecesGiradas);
            }
        }
        else
        {
            vecesGiradas = 0;
        }

        if (configManager.GetComponent<ConfigManager>().rectificarGiro)
        {
            rectificarGiro = -1;
        }
        else
        {
            rectificarGiro = 1;
        }
        
        this.gameObject.transform.SetLocalPositionAndRotation(jugador.transform.position + controles.posicionCabeza + new Vector3(0, -desfasePosicion -1.3f, 0), quaternion.Euler(jugador.transform.rotation.x, jugador.transform.rotation.y + controles.rotacionCabeza.y * rectificarGiro * (float)Math.PI  - desfaseRotacion, jugador.transform.rotation.z));

        tamaño = configManager.GetComponent<ConfigManager>().tamañoCinturón;

        this.gameObject.transform.localScale = new Vector3(1 * tamaño, 1 * tamaño, 1 * tamaño);
    }
}
