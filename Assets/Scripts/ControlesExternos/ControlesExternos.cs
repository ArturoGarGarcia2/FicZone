using UnityEngine;
using UnityEngine.XR;

public class ControlesExternos : MonoBehaviour
{

    UnityEngine.XR.InputDevice leftHand =InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    UnityEngine.XR.InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    UnityEngine.XR.InputDevice head =InputDevices.GetDeviceAtXRNode(XRNode.Head);

    [Header ("No tocar")]

    public Vector3 posicionCabeza;

    public Quaternion rotacionCabeza;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnEnable()
    {
        InputDevices.deviceConnected += DeviceConnected;
    }

    void DeviceConnected(InputDevice device)
    {
        if (device.characteristics.HasFlag(InputDeviceCharacteristics.HeadMounted))
        {
            head = device;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (head.isValid)
        Debug.Log("es valido");

        else 
        Debug.Log("No es valido");

        Vector3 posicionTemp;

        if(head.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out posicionTemp))
        {
            posicionCabeza = posicionTemp;
        }

        Debug.Log(posicionCabeza);

        Quaternion rotacionTemp;

        if (head.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out rotacionTemp))
        {
            rotacionCabeza = rotacionTemp;
        }

        Debug.Log(rotacionCabeza);
    }
}
