using UnityEngine;

public class CopyScale : MonoBehaviour
{

    public GameObject objectToCopy;

    public Vector3 scaleOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localScale = objectToCopy.transform.localScale + scaleOffset;
    }
}
