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
    private int hojasAgarradas = 0;
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
            sombrerosAgarrados++;
            textoUI.text = "Se podran poner en los maniquis? " + sombrerosAgarrados + "/4";
        }
        if (args.interactableObject.transform.tag == "linterna")
        {
            textoUI.text = "Servira para estos dibujos extraños???? ";
        }
        if (args.interactableObject.transform.tag == "hojasCubo")
        {
            hojasAgarradas++;
            textoUI.text = "Sera parte de un codigo " + hojasAgarradas + "/2";
        }
    }
}
