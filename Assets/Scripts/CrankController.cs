using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CrankController : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    public Transform pivot;
    public Vector3 rotationAxis = Vector3.forward;

    private Transform hand;
    private float previousAngle;

    void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        hand = args.interactorObject.transform;
        previousAngle = GetHandAngle();
    }

    void OnRelease(SelectExitEventArgs args)
    {
        hand = null;
    }

    void Update()
    {
        if (hand == null) return;

        float currentAngle = GetHandAngle();
        float delta = Mathf.DeltaAngle(previousAngle, currentAngle);

        float sensitivity = 1.5f;

        transform.Rotate(rotationAxis, delta * sensitivity, Space.Self);

        previousAngle = currentAngle;
    }

    float GetHandAngle()
    {
        Vector3 localHandPos = pivot.InverseTransformPoint(hand.position);
        return Mathf.Atan2(localHandPos.y, localHandPos.x) * Mathf.Rad2Deg;
    }
}