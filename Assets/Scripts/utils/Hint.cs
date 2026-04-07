using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Hint : MonoBehaviour
{
    public TextMeshProUGUI textoUI;
    private XRGrabInteractable grabInteractable;
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
            textoUI.text = "¡Objeto agarrado! llave " + args.interactableObject.transform.name;
        }
        if (args.interactableObject.transform.name == "Llave father")
        {
            textoUI.text = "¡Objeto agarrado! llave dormitorio";
        }
        if (args.interactableObject.transform.name == "Llave main")
        {
            textoUI.text = "¡Objeto agarrado! llave salida";
        }
        if (args.interactableObject.transform.name == "Llave living")
        {
            textoUI.text = "¡Objeto agarrado!";
        }

    }
}
