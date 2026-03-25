using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GrabLayerFix : MonoBehaviour
{
    private int defaultLayer;
    private int grabbedLayer;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        defaultLayer = gameObject.layer;
        grabbedLayer = LayerMask.NameToLayer("Grabbed");

        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    public void OnGrab(SelectEnterEventArgs args)
    {
        gameObject.layer = grabbedLayer;
        if (args.interactorObject is XRSocketInteractor)
        {
            gameObject.layer = defaultLayer;
        }
    }

    public void OnRelease(SelectExitEventArgs args)
    {
        gameObject.layer = defaultLayer;        
    }
}