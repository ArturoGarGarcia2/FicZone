using UnityEngine;

public class UVText : MonoBehaviour
{
    public static UVText Instance;

    public Light flashlight;
    public GameObject[] shapeObjects; 
    private string sequenceShapes = "";

    void Awake()
    {
        Instance = this;
    }

    public void SetSequence(string sequence)
    {
        sequenceShapes = sequence;
    }

    void Start()
    {
        
        foreach (var obj in shapeObjects)
            obj.SetActive(false);
    }

    void Update()
    {
        foreach (var obj in shapeObjects)
        {
            Vector3 dirToObj = obj.transform.position - flashlight.transform.position;
            float distance = dirToObj.magnitude;

            // comprobar rango
            if (distance > flashlight.range)
            {
                Debug.Log("Fuera de rango");
                obj.SetActive(false);
                continue;
            }

            // comprobar ángulo del cono
            float angle = Vector3.Angle(flashlight.transform.forward, dirToObj);

            if (angle > flashlight.spotAngle * 0.5f)
            {
                Debug.Log("Fuera de cono");
                obj.SetActive(false);
                continue;
            }

            // comprobar si algo bloquea la luz
            if (Physics.Raycast(flashlight.transform.position, dirToObj.normalized, distance))
            {
                Debug.Log("Algo en medio");
                obj.SetActive(false);
                continue;
            }

            Debug.Log("Todo piola");
            obj.SetActive(true);
        }
    }
}
