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
        Vector3 dir = flashlight.transform.forward;
        Vector3 origin = flashlight.transform.position;

        if (Physics.Raycast(origin, dir, out RaycastHit hit, 5f))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                foreach (var obj in shapeObjects)
                    obj.SetActive(true);
            }
        }
        else
        {
            foreach (var obj in shapeObjects)
                obj.SetActive(false);
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