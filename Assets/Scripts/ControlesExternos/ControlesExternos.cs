using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class ControlesExternos : MonoBehaviour
{

    UnityEngine.XR.InputDevice leftHand;
    UnityEngine.XR.InputDevice rightHand;
    UnityEngine.XR.InputDevice head;

    [Header ("No tocar")]

    public Vector3 posicionCabeza;

    public Quaternion rotacionCabeza;

    public bool GirarDer, GirarIzq;
    
    public Vector2 GiroVect;

    public float TurnDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {

        leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        head = InputDevices.GetDeviceAtXRNode(XRNode.Head);

    }

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

        if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right))
        {
            rightHand = device;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GirarDer == true)
        {
            GirarDer = false;
        }
        if (GirarIzq == true)
        {
            GirarIzq = false;
        }

        TurnDelay -= Time.deltaTime;

        

        Vector3 posicionTemp;

        if(head.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out posicionTemp))
        {
            posicionCabeza = posicionTemp;
        }


        Quaternion rotacionTemp;

        if (head.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out rotacionTemp))
        {
            rotacionCabeza = rotacionTemp;
        }

        Vector2 GiroVectTemp;

        if (TurnDelay <= 0)
        if(rightHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out GiroVectTemp))
        {

            GiroVect = GiroVectTemp;

            if (GiroVectTemp.x > 0.5f)
            {
                GirarDer = true; 

                TurnDelay = this.gameObject.transform.GetChild(1).GetChild(0).GetComponent<SnapTurnProvider>().debounceTime;
            }
            else
            {
                GirarDer = false;
            }

            if (GiroVectTemp.x < -0.5f)
            {
                GirarIzq = true;

                TurnDelay = this.gameObject.transform.GetChild(1).GetChild(0).GetComponent<SnapTurnProvider>().debounceTime;
            }
            else
            {
                GirarIzq = false;
            }
        }



    }
}
