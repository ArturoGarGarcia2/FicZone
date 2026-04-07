using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class puzzleCuboColoresManager : MonoBehaviour
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
            IXRSelectInteractable objetoCogido = socket[i].GetOldestInteractableSelected();
            if (objetoCogido == null)
            {
                return;
            }
            MeshRenderer colorCubo = objetoCogido.transform.GetComponent<MeshRenderer>();
            if(colorCubo.material.color != secuenciaCorrecta[i])
            {
                return;
            }
        }
        // para que el ultimo cubo encaje bien en su posicion
        StartCoroutine(EsperarYResolver());
    }
    private void PuzzleResuelto()
    {   
        puzzleCuboColores = true;
         GameManager.Instance.CompletePuzzle(3);
        for (int i = 0; i < socket.Length; i++)
        {

            IXRSelectInteractable objetoCogido = socket[i].GetOldestInteractableSelected();

            Transform t = objetoCogido.transform;
            if (objetoCogido != null)
            {
                XRGrabInteractable grab = objetoCogido.transform.GetComponent<XRGrabInteractable>();
                if (grab != null)
                {
                    grab.enabled = false;
                }

                Rigidbody rb = t.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }
            }

            socket[i].enabled = false;
        }
    }
    private IEnumerator EsperarYResolver()
    {
        yield return new WaitForSeconds(0.2f);
        PuzzleResuelto();
    }

}
