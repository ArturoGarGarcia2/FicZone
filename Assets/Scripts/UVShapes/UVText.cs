using UnityEngine;

public class UVText : MonoBehaviour
{
    public static UVText Instance;

    public Light flashlight;
    public GameObject text;

    void Update()
    {
        Vector3 dir = flashlight.transform.forward;
        Vector3 origin = flashlight.transform.position;

        if (Physics.Raycast(origin, dir, out RaycastHit hit, 5f))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                text.SetActive(true);
            }
        }
        else
        {
            text.SetActive(false);
        }
    }
}
