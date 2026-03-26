using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class LuzLinterna : MonoBehaviour
{
    
    private GameObject luz;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        luz = this.gameObject.transform.GetChild(1).gameObject;

        UpdateBotones();

        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void FixedUpdate()
    {
        
    }

    private async UniTask UpdateBotones()
    {
        while (true)
        {
            UnityEngine.XR.InputDevice leftHand =InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            UnityEngine.XR.InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

            bool presionado;
            if (rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out presionado))
            {
                if (presionado)
                {

                    await UniTask.Delay(200);

                    if (luz.gameObject.activeInHierarchy == false)
                    {
                        luz.gameObject.SetActive(true);
                    }

                    else
                    {
                        luz.gameObject.SetActive(false);
                    }  
                }
            }

            if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out presionado))
            {
                if (presionado)
                {

                    await UniTask.Delay(200);

                    if (luz.gameObject.activeInHierarchy == false)
                    {
                        luz.gameObject.SetActive(true);
                    }

                    else
                    {
                        luz.gameObject.SetActive(false);
                    }  
                }
            }

            await UniTask.Delay(1);
            
        }
    }
}
