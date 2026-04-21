using UnityEngine;

public class PanicoManager : MonoBehaviour
{
    public GameObject[] objects;
    private Vector3[] objectsPosition;
    private Quaternion[] objectsRotation;
    
    public static PanicoManager Instance;

    void Awake()
    {
        objectsPosition = new Vector3[objects.Length];
        objectsRotation = new Quaternion[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            objectsPosition[i] = objects[i].transform.position;
            objectsRotation[i] = objects[i].transform.rotation;
        }

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); 
    }

    public void Panicked()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if(objects[i] == null) continue;
            objects[i].transform.position = objectsPosition[i];
            objects[i].transform.rotation = objectsRotation[i];
        }
    }
}