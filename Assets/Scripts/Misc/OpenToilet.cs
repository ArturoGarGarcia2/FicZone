using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class OpenToilet : MonoBehaviour
{
    public Animator Animator;
    public XRGrabInteractable grab;
    
    private bool isLidOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isLidOpen = false;
        grab.selectEntered.AddListener(OpenLid);
    }

    void OpenLid(SelectEnterEventArgs args)
    {
        if (isLidOpen) return;
        
        isLidOpen = true;
        grab.enabled = false; 
        Animator.SetTrigger("OpenLid");
    }
    
}
