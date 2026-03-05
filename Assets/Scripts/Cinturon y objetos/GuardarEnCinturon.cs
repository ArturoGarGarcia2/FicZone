using System.Collections;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GuardarEnCinturon : MonoBehaviour
{

    public bool guardado;

    [Header ("No asignar nada en el bolsillo")]

    public GameObject bolsillo;

    public Vector3 positionOffset;

    public bool adjustOffset;

    public Vector3 scaleAdjust;

    public bool adjustScale;

    private Vector3 originalScale;

    public Vector3 rotationAdjust;

    public bool adjustRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (!guardado)
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;

        }

        Guardar();

        if (this.gameObject.name == "CuboPrueba")

        Debug.Log(originalScale);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cinturon"))
        {

            bolsillo = other.gameObject;

            guardado = true;
        }

        if (this.gameObject.name == "Linterna")
        {
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cinturon"))
        {
           guardado = false; 

            if (this.gameObject.name == "Linterna")
            {

            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            }



            //this.gameObject.transform.localScale = originalScale;

            
        }
    }

    private void Guardar()
    {
        if (guardado)
        {

            if (adjustOffset)
            this.gameObject.transform.position = bolsillo.gameObject.transform.position + positionOffset;

            else
            this.gameObject.transform.position = bolsillo.gameObject.transform.position;

            if(adjustRotation)
            this.gameObject.transform.rotation = quaternion.Euler(rotationAdjust.x, rotationAdjust.y, rotationAdjust.z);

            if(adjustScale)
            this.gameObject.transform.localScale = scaleAdjust;

            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }

        else
        {
            this.gameObject.transform.localScale = originalScale;
        }

    }

    //activar y desactivar componente XR Grab Interactable

    /*public void GrabWeapon(SelectEnterEventArgs grabData)
        {
            var rightHand = (grabData.interactorObject.interactionLayers & (1 << 1)) == 0;
            var handString = rightHand ? "Right" : "Left";
            Debug.Log($"GrabWeapon {handString} hand");
        }*/
}
