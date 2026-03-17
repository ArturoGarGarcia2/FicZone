using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class puzzleManager : MonoBehaviour
{
    public GeneradorColores generador;
    private List<Color> secuenciaCorrecta;
    public XRSocketInteractor[] socket;
    public bool puzzleCuboColores = false;

    private void Start()
    {
        secuenciaCorrecta = generador.getSecuenciaCorrecta();
    }
    public void ComprobarSecuencia()
    {
        for (int i = 0; i < socket.Length ; i++)
        {
            //Debug.Log("posicion " + i + "antes de todo");
            IXRSelectInteractable objetoCogido = socket[i].GetOldestInteractableSelected();
            if (objetoCogido == null)
            {
                Debug.Log("no hay cubo en la posicion " + i);
                return;
            }
            MeshRenderer colorCubo = objetoCogido.transform.GetComponent<MeshRenderer>();
            if(colorCubo.material.color != secuenciaCorrecta[i])
            {
                Debug.Log("fallo en la posicion "+i + "color cubo: " + colorCubo.material.color + " color correcto: " + secuenciaCorrecta[i]);
                return;
            }
            //Debug.Log("posicion " + i + "despues de todo");
        }
        PuzzleResuelto();
    }
    private void PuzzleResuelto()
    {
        Debug.Log("puzzle resuelto");
        puzzleCuboColores = true;
    }
}
