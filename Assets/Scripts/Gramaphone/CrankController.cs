using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CrankController : MonoBehaviour
{
    public static CrankController Instance;


    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    public Transform pivot;
    public Vector3 rotationAxis = Vector3.forward;

    private Transform hand;
    private float previousAngle;

    public float sensitivity = 2f;

    private float smoothedDelta;
    public float smoothSpeed = 15f;

    [Header("NºVueltas")]
    public int completedTurns = 0;
    private float totalRotation = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Instance = this;  
        }
    }

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
        var interactor = args.interactorObject as XRBaseInteractor;
        hand = interactor.GetAttachTransform(grab);

        previousAngle = GetHandAngle();
    }

    void OnRelease(SelectExitEventArgs args)
    {
        hand = null;
    }

    void LateUpdate()
    {
        if (hand == null) return;

        float currentAngle = GetHandAngle();
        float rawDelta = Mathf.DeltaAngle(previousAngle, currentAngle);

        Vector3 localHandPos = pivot.InverseTransformPoint(hand.position);
        float radius = new Vector2(localHandPos.x, localHandPos.y).magnitude;

        if (radius < 0.05f) return; 

        float delta = rawDelta * sensitivity;

        delta = Mathf.Clamp(delta, -50f, 50f);

        smoothedDelta = Mathf.Lerp(smoothedDelta, delta, Time.deltaTime * smoothSpeed);
        delta = smoothedDelta;

        if (delta > 0f)
        {
            delta = 0f;
        }

        totalRotation += delta;

        int currentTurn = Mathf.FloorToInt(-totalRotation / 360f);
        
        if(currentTurn > completedTurns)
        {
            completedTurns = currentTurn;
            Debug.Log("Vueltas completas: " + completedTurns);
        }
        transform.Rotate(rotationAxis, delta, Space.Self);

        previousAngle = currentAngle;
    }

    float GetHandAngle()
    {
        Vector3 localHandPos = pivot.InverseTransformPoint(hand.position);
        return Mathf.Atan2(localHandPos.y, localHandPos.x) * Mathf.Rad2Deg;
    }
}