using UnityEngine;

public class CinturonPlayerCoord : MonoBehaviour
{
    public Transform jugador;
    public Transform camara;
    public ConfigManager configManager;

    private ControlesExternos controles;

    private int vecesGiradas = 0;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        configManager = GameObject.FindWithTag("Config").GetComponent<ConfigManager>();

        controles = jugador.GetComponent<ControlesExternos>();

        if (camara == null)
        {
            camara = Camera.main.transform;
        }
    }

    void Update()
    {
        float desfasePosicion = configManager.alturaCinturon;
        float desfaseRotacion = configManager.rotacionCinturon;
        float tamaño = configManager.tamañoCinturón;

        // -----------------------------
        // ROTACIÓN POR SALTOS (SNAP TURN)
        // -----------------------------
        if (configManager.rotacionASaltos)
        {
            if (controles.GirarDer)
            {
                vecesGiradas++;
            }

            if (controles.GirarIzq)
            {
                vecesGiradas--;
            }
        }
        else
        {
            vecesGiradas = 0;
        }

        float rotacionSnap = 45f * vecesGiradas;

        // -----------------------------
        // RECTIFICAR GIRO
        // -----------------------------
        int rectificarGiro = configManager.rectificarGiro ? -1 : 1;

        // -----------------------------
        // POSICIÓN TIPO CINTURÓN (CLAVE)
        // -----------------------------
        Vector3 posicion = camara.position;

        // Bajar a la altura de la cintura
        posicion.y -= (desfasePosicion + 1.5f);

        // Pequeño desplazamiento hacia atrás (muy importante para realismo)
        Vector3 forwardPlano = new Vector3(camara.forward.x, 0, camara.forward.z).normalized;
        posicion -= forwardPlano * 0.15f; // ajusta este valor si quieres más/menos retraso

        transform.position = posicion;

        // -----------------------------
        // ROTACIÓN SOLO EN Y
        // -----------------------------
        float rotY =
            camara.eulerAngles.y +
            (controles.rotacionCabeza.y * rectificarGiro) -
            desfaseRotacion +
            rotacionSnap +90;

        transform.rotation = Quaternion.Euler(0, rotY, 0);

        // -----------------------------
        // ESCALA
        // -----------------------------
        transform.localScale = Vector3.one * tamaño;
    }
}