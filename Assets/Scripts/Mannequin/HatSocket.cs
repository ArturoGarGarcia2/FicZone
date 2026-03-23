using UnityEngine;

public class HatSocket : MonoBehaviour
{
    public Hat.Family currentHatInSocket;

    void OnTriggerEnter(Collider other)
    {
        Hat hat = other.GetComponent<Hat>();
        if (hat)
            currentHatInSocket = hat.family;
    }

    void OnTriggerExit(Collider other)
    {
        Hat hat = other.GetComponent<Hat>();
        if (hat)
            currentHatInSocket = Hat.Family.None;
    }
}
