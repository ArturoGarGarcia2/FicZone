using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Hint : MonoBehaviour
{
    public TextMeshProUGUI textoUI;
    private XRGrabInteractable grabInteractable;

    private int sombrerosAgarrados = 0;
    bool sombreroPadre = false;
    private bool sombreroMadre = false;
    private bool sombreroHija = false;
    bool sombreroHijo = false;
    private int hojasAgarradas = 0;
    bool hojaDer = false;
    bool hojaIzq = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnGrab(SelectEnterEventArgs args)
    {
        if( args.interactableObject.transform.tag == "Key")
        {
            textoUI.text = "Servira para las puertas cerradas";
        }
        if (args.interactableObject.transform.tag == "hat")
        {
            if (args.interactableObject.transform.name == "daughters_hat" && !sombreroHija)
            {
                sombreroHija = true;
                sombrerosAgarrados++;
            }
            if (args.interactableObject.transform.name == "sons_hat" && !sombreroHijo)
            {
                sombreroHijo = true;
                sombrerosAgarrados++;
            }
            if (args.interactableObject.transform.name == "fathers_hat" && !sombreroPadre)
            {
                sombreroPadre = true;
                sombrerosAgarrados++;
            }
            if (args.interactableObject.transform.name == "mothers_hat" && !sombreroMadre)
            {
                sombreroMadre = true;
                sombrerosAgarrados++;
            }
            textoUI.text = "Se podran poner en los maniquis? " + sombrerosAgarrados + "/4";
        }
        if (args.interactableObject.transform.tag == "linterna")
        {
            textoUI.text = "Servira para estos dibujos extraños???? ";
        }
        if (args.interactableObject.transform.tag == "hojasCubo")
        {
            if (args.interactableObject.transform.name == "papelDer" && !hojaDer)
            {
                hojaDer = true;
                hojasAgarradas++;
            }
            if (args.interactableObject.transform.name == "papelIzq" && !hojaIzq)
            {
                hojaIzq = true;
                hojasAgarradas++;
            }
            textoUI.text = "Sera parte de un codigo " + hojasAgarradas + "/2";
        }
        if (args.interactableObject.transform.tag == "pomo")
        {
            textoUI.text = "Se necesita una llave";
        }
    }
}
