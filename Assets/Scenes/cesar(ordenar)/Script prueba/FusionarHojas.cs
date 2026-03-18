using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class FusionarHojas : MonoBehaviour
{
    private XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        socket.selectEntered.AddListener(AlAcoplar);
    }
    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(AlAcoplar);
    }

    void AlAcoplar(SelectEnterEventArgs args)
    {
        GameObject HojaDer = args.interactableObject.transform.gameObject;

        HojaDer.transform.SetParent(this.transform);

        if (HojaDer.TryGetComponent<XRGrabInteractable>(out var grab))
        {
            grab.enabled = false;
        }
        socket.enabled = false;
        Debug.Log("codigo acoplado");
    }
}
