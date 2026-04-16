using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    private Vector3  playerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall1Floor"))
        {
            transform.position = playerPosition;
        }
    }
}
