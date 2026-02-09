using UnityEngine;
using VicGenLib.Controllers;

public class PlayerPrefabs : MonoBehaviour
{
    float rotationX_in;

    float rotationX_out;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovBase.SimplePlayerMovement(this.gameObject);

        MovBase.KeyCamMov(this.gameObject, rotationX_in, out rotationX_out, 40, -40);
    }
}
