using Unity.Mathematics;
using UnityEngine;

public class CinturonPlayerCoord : MonoBehaviour
{

    public GameObject jugador, configManager;

    private ControlesExternos controles;

    public float desfaseRotacion, tamaño, desfasePosicion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        configManager = GameObject.Find("ConfigManager");

        jugador = GameObject.FindGameObjectWithTag("Player");

        controles = jugador.GetComponent<ControlesExternos>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log($"rotacion jugador {jugador.transform.rotation.eulerAngles.y}");

        desfasePosicion = configManager.GetComponent<ConfigManager>().alturaCinturon;

        this.gameObject.transform.position = jugador.transform.position + controles.posicionCabeza + new Vector3(0, desfasePosicion, 0);

        desfaseRotacion = configManager.GetComponent<ConfigManager>().rotacionCinturon;

        //this.gameObject.transform.rotation = quaternion.Euler(jugador.transform.rotation.x + controles.rotacionCabeza.x, jugador.transform.rotation.y + controles.rotacionCabeza.y + desfaseRotacion, jugador.transform.rotation.z + controles.rotacionCabeza.z);

        this.gameObject.transform.SetLocalPositionAndRotation(jugador.gameObject.transform.position, quaternion.Euler (jugador.transform.rotation.x, jugador.transform.rotation.y + controles.rotacionCabeza.y + desfaseRotacion, jugador.transform.rotation.z));

        tamaño = configManager.GetComponent<ConfigManager>().tamañoCinturón;

        this.gameObject.transform.localScale = new Vector3(1 * tamaño, 1 * tamaño, 1 * tamaño);
    }
}
