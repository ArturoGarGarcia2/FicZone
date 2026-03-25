using UnityEngine;


public class DrawerLimitStop : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    public Vector3 localAxis = Vector3.forward;
    public float min = 0f;
    public float max = 0.3f;
    public float tolerance = 0.000f;

    private Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
        localAxis = localAxis.normalized;
    }

    void Update()
    {
        if (!grab.isSelected)
        {
            grab.trackPosition = true;
            return;
        }

        Vector3 localOffset = transform.localPosition - startLocalPos;

        float pos = Vector3.Dot(localOffset, localAxis);

        bool atMax = pos >= (max - tolerance);
        bool atMin = pos <= (min + tolerance);

        grab.trackPosition = !(atMax || atMin);
    }
}