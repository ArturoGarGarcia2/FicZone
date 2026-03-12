using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class puzzleManager : MonoBehaviour
{
    public GeneradorColores generador;
    private List<GameObject> secuenciaCorrecta;
    public XRSocketInteractor[] socket;

    private void Start()
    {
        secuenciaCorrecta = generador.getSecuenciaCorrecta();
    }
    public void ComprobarSecuencia()
    {
        for (int i = 0; i < socket.Length ; i++)
        {
            IXRSelectInteractable objetoCogido = socket[i].GetOldestInteractableSelected();
            if (objetoCogido != null)
            {
                Debug.Log("no hay cubo en la posicion " + i);
                return;
            }
            MeshRenderer colorCubo = objetoCogido.transform.GetComponent<MeshRenderer>();
            if(colorCubo.material.color != secuenciaCorrecta[i].GetComponent<MeshRenderer>().material.color)
            {
                Debug.Log("fallo en la posicion "+i);
                return;
            }
        }
        PuzzleResuelto();
    }
    private void PuzzleResuelto()
    {
        Debug.Log("puzzle resuelto");
    }
}
