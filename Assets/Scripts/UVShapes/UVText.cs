using UnityEngine;
using TMPro;

public class UVText : MonoBehaviour
{
    public static UVText Instance;

    public Light flashlight;
    public TMP_Text text;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        text.gameObject.SetActive(false);
    }

    void Update()
    {
        
            Vector3 dirToObj = transform.position - flashlight.transform.position;
            float distance = dirToObj.magnitude;

            // comprobar rango
            if (distance > flashlight.range)
            {
                Debug.Log("Fuera de rango");
                text.gameObject.SetActive(false);
                return;
            }

            // comprobar ángulo del cono
            float angle = Vector3.Angle(flashlight.transform.forward, dirToObj);

            if (angle > flashlight.spotAngle * 0.5f)
            {
                Debug.Log("Fuera de cono");
                text.gameObject.SetActive(false);
                return;
            }

            // comprobar si algo bloquea la luz
            if (Physics.Raycast(flashlight.transform.position, dirToObj.normalized, distance))
            {
                Debug.Log("Algo en medio");
                text.gameObject.SetActive(false);
                return;
            }

            Debug.Log("Todo piola");
            text.gameObject.SetActive(true);
        
    }
}
