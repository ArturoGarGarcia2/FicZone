using UnityEngine;

public class Keyhole : MonoBehaviour
{
    public bool hasKeyInside;
    public string requiredKeyID;


    void Start()
    {
        hasKeyInside = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        KeyID key = other.GetComponent<KeyID>();

        if (key != null && key.keyID == requiredKeyID)
        {
            hasKeyInside = true;
            Destroy(other.gameObject);
        }
    }
}
