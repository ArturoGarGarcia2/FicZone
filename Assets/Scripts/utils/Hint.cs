using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Hint : MonoBehaviour
{
    public TextMeshProUGUI textoUI;
    // texto pared
    private int pared = 0;
    // puzzle maniquis
    private int sombrerosAgarrados = 0;
    private bool sombreroPadre = false;
    private bool sombreroMadre = false;
    private bool sombreroHija = false;
    private bool sombreroHijo = false;
    //puzzle cubos
    private int hojasAgarradas = 0;
    private bool hojaDer = false;
    private bool hojaIzq = false;
    // linterna
    private bool linternaAgarrada = false;
    // puertas
    private bool puertaMain = false;
    private bool puertaParentsRoom = false;
    private bool puertaSalon = false;
    // llave
    private bool llave = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textoUI.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnGrab(SelectEnterEventArgs args)
    {
        if( args.interactableObject.transform.tag == "Key" && !llave)
        {
            llave = true;
            textoUI.text = "¿Servira para las puertas cerradas?";
        }
        if (args.interactableObject.transform.tag == "hat")
        {
            if (args.interactableObject.transform.name == "daughters_hat" && !sombreroHija)
            {
                sombreroHija = true;
                sombrerosAgarrados++;
                textoUI.text = "¿Se podrá poner en los maniquís? " + sombrerosAgarrados + "/4";
            }
            if (args.interactableObject.transform.name == "sons_hat" && !sombreroHijo)
            {
                sombreroHijo = true;
                sombrerosAgarrados++;
                textoUI.text = "¿Se podrá poner en los maniquís? " + sombrerosAgarrados + "/4";
            }
            if (args.interactableObject.transform.name == "fathers_hat" && !sombreroPadre)
            {
                sombreroPadre = true;
                sombrerosAgarrados++;
                textoUI.text = "¿Se podrá poner en los maniquís? " + sombrerosAgarrados + "/4";
            }
            if (args.interactableObject.transform.name == "mothers_hat" && !sombreroMadre)
            {
                sombreroMadre = true;
                sombrerosAgarrados++;
                textoUI.text = "¿Se podrá poner en los maniquís? " + sombrerosAgarrados + "/4";
            }
        }
        if (args.interactableObject.transform.tag == "linterna" && !linternaAgarrada)
        {
            linternaAgarrada = true;
            textoUI.text = "¿Servirá para los dibujos de las paredes? ";
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
            textoUI.text = "¿Será parte de un código? " + hojasAgarradas + "/2";
        }
        if (args.interactableObject.transform.tag == "pomo")
        {
            if (!args.interactableObject.transform.GetComponent<XRKnob>().keyhole.hasKeyInside)
            {
                if (args.interactableObject.transform.parent.parent.parent.name == "LockedDoorMain" && !puertaMain)
                {
                    puertaMain = true;
                    textoUI.text = "¿Se necesitará una llave?";
                }
                if (args.interactableObject.transform.parent.parent.parent.name == "LockedDoorParentsRoom" && !puertaParentsRoom)
                {
                    puertaParentsRoom = true;
                    textoUI.text = "¿Se necesitará una llave?";
                }
                if (args.interactableObject.transform.parent.parent.parent.name == "LockedDoorSalon" && !puertaSalon)
                {
                    puertaSalon = true;
                    textoUI.text = "¿Se necesitará una llave?";
                }
            }
        }
        StopCoroutine("delay");
        StartCoroutine(delay());
    }
    public void PuzzleResuelto(string name)
    {
        if (name == "maniqui")
            textoUI.text = "Parece que hay algo en la mesa";
            
        if (name == "cubos")
            textoUI.text = "Al lado de la cama ha aparecido algo";
            
        if (name == "cables")
            textoUI.text = "Ve a la cocina";
            
        if (name == "codigo")
            textoUI.text = "¿Qué es lo que hay en la mesa?";
            
        if (name == "simon")
            textoUI.text = "Parece que hay algo sobre el escritorio.";
            
        if (name == "secuencia")
            textoUI.text = "Parece que hay algo a los pies de la cama.";
            
        StopCoroutine("delay");
        StartCoroutine(delay());
    }
    public void IluminarPared(string pistaPared)
    {
        if (pistaPared == "Contenedor texto0")
            pared++;
            
        if (pistaPared == "Contenedor texto1")
            pared++;
            
        if (pistaPared == "Contenedor texto2")
            pared++;
            
        if (pistaPared == "Contenedor texto3")
            pared++;
            
        if (pistaPared == "Contenedor texto4")
            pared++;
            
        if (pistaPared == "Contenedor texto5")
            pared++;
            
        if (pistaPared == "Contenedor texto6")
            pared++;
            
        if (pistaPared == "Contenedor texto7")
            pared++;
            
        textoUI.text = "" + pared + "/8";
        StopCoroutine("delay");
        StartCoroutine(delay());

        if(pared == 8)
        {
            textoUI.text = "En la tele parece que están las pistas.";
            StopCoroutine("delay");
            StartCoroutine(delay());
        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(3);
        textoUI.text = "";
    }
}
