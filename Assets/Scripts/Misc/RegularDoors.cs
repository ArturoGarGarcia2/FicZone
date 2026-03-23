using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RegularDoors : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    [Header("IMPORTANTE")]
    [Tooltip("Dios santo, esto es importante. No sé por qué a veces le dan neuras. Mirar las dos animaciones y poner \"TRUE\" si la animación 2 funciona mejor")]
    public bool isSideways; // Esta idea es muy estúpida
    public XRGrabInteractable grab;
    bool isAlreadyOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
        }
        isAlreadyOpen = false;
    }

    public void OpenThisDoor(SelectEnterEventArgs args)
    {
        if (isAlreadyOpen) return;

        isAlreadyOpen = true;
        grab.enabled = false; 
        
        if (isSideways)
        {
            doorAnimator.SetTrigger("OpenDoorSideways");
        }
        else
        {
            doorAnimator.SetTrigger("OpenDoor");
        }
    }
}
