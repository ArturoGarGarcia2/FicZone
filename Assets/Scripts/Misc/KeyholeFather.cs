using UnityEngine;

public class KeyholeFather : MonoBehaviour
{
    public bool hasKeyInside;

    void Start()
    {
        hasKeyInside = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyFather"))
        {
            hasKeyInside = true;
            Destroy(other.gameObject);
        }
    }
}
