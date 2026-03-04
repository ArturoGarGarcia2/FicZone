using UnityEngine;

public class Keyhole : MonoBehaviour
{
    public bool hasKeyInside;

    void Start()
    {
        hasKeyInside = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            hasKeyInside = true;
            Destroy(other.gameObject);
        }
    }
}
