using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabLayerFix : MonoBehaviour
{
    private int defaultLayer;
    private int grabbedLayer;

    private void Awake()
    {
        defaultLayer = gameObject.layer;
        grabbedLayer = LayerMask.NameToLayer("Grabbed");
    }

    public void OnGrab()
    {
        gameObject.layer = grabbedLayer;
    }

    public void OnRelease()
    {
        gameObject.layer = defaultLayer;
    }
}