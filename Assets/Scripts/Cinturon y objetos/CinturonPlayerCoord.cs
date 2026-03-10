using Unity.Mathematics;
using UnityEngine;

public class CinturonPlayerCoord : MonoBehaviour
{

    public GameObject jugador;

    private ControlesExternos controles;

    public float desfaseRotacion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controles = jugador.GetComponent<ControlesExternos>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = jugador.transform.position + controles.posicionCabeza - new Vector3(0, 0.4f, 0);

        this.gameObject.transform.rotation = quaternion.Euler(jugador.transform.rotation.x + controles.rotacionCabeza.x, jugador.transform.rotation.y + controles.rotacionCabeza.y + desfaseRotacion, jugador.transform.rotation.z + controles.rotacionCabeza.z);
    }
}
