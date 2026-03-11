using UnityEngine;

public class UVPattern3D : MonoBehaviour
{
    public static UVPattern3D Instance;

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
        UpdateShapesText();
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
                obj.SetActive(false);
                continue;
            }

            // comprobar ángulo del cono
            float angle = Vector3.Angle(flashlight.transform.forward, dirToObj);

            if (angle > flashlight.spotAngle * 0.5f)
            {
                obj.SetActive(false);
                continue;
            }

            // comprobar si algo bloquea la luz
            if (Physics.Raycast(flashlight.transform.position, dirToObj.normalized, distance))
            {
                obj.SetActive(false);
                continue;
            }

            obj.SetActive(true);
        }
    }
    void UpdateShapesText()
    {
        // Asume que shapeObjects.Length == number of symbols in sequence
        string[] symbols = sequenceShapes.Split(' ');

        for (int i = 0; i < symbols.Length && i < shapeObjects.Length; i++)
        {
            var textMesh = shapeObjects[i].GetComponent<TMPro.TextMeshPro>();
            if (textMesh != null)
                textMesh.text = symbols[i];
        }
    }
}