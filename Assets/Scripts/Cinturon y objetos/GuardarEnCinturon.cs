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

    public GameObject positionOffsetObject, positionOffsetInstance;

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

            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        }

        Guardar();

        if (this.gameObject.name == "CuboPrueba")

        Debug.Log(originalScale);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cinturon") && other.gameObject.GetComponent<Bolsillos>().espaciosDisponibles > 0)
        {

            bolsillo = other.gameObject;

            guardado = true;

            bolsillo.GetComponent<Bolsillos>().espaciosDisponibles--;
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
            bolsillo = other.gameObject;

            guardado = false; 

            bolsillo.GetComponent<Bolsillos>().espaciosDisponibles++;

            this.gameObject.transform.parent = null;

            Destroy(positionOffsetInstance);

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
            this.gameObject.transform.parent = bolsillo.gameObject.transform.parent.GetChild(2).transform;

            GameObject parentBolsillo = bolsillo.gameObject.transform.parent.GetChild(2).gameObject;

            if (adjustOffset)
            parentBolsillo.transform.position = positionOffset + bolsillo.gameObject.transform.parent.GetChild(0).gameObject.transform.position;

            if(adjustRotation)
            parentBolsillo.transform.localRotation = quaternion.Euler(VicGenLib.Calc.Angles.NormalToEulerAnglesf3(rotationAdjust.x, rotationAdjust.y, rotationAdjust.z));

            if(adjustScale)
            this.gameObject.transform.localScale = scaleAdjust + originalScale;

            this.gameObject.transform.localRotation = quaternion.Euler(0,0,0);

            this.gameObject.transform.localPosition = new Vector3(0,0,0);

            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }

        else
        {
            this.gameObject.transform.parent = null;

            this.gameObject.transform.localScale = originalScale;

            Destroy(positionOffsetInstance);
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
