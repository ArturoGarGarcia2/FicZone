using System.Numerics;
using UnityEngine;

public class CamPosFix : MonoBehaviour
{

    public ControlesExternos controles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controles = this.gameObject.GetComponent<ControlesExternos>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controles.gafasPuestas)
        {
            this.gameObject.transform.GetChild(0).GetChild(0).gameObject.transform.localPosition = new UnityEngine.Vector3(0,this.gameObject.transform.GetChild(0).GetChild(0).gameObject.transform.localPosition.y,0);
        }
    }
}
