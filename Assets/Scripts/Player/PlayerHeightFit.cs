using UnityEngine;

public class PlayerHeightFit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<CharacterController>().height = 1.36f;
        this.gameObject.GetComponent<CharacterController>().center = new Vector3(0, 0.76f, 0);
        
    }
}
