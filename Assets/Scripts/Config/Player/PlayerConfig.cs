using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerConfig : MonoBehaviour
{

    public GameObject configManager, Menu;

    public ControllerInputActionManager controlDer, controlIzq;

    public float cameraOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        configManager = GameObject.FindWithTag("Config");


    }

    // Update is called once per frame
    void Update()
    {
        cameraOffset = configManager.GetComponent<ConfigManager>().alturaPlayer;

        this.gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(this.gameObject.transform.GetChild(0).gameObject.transform.position.x, this.gameObject.transform.position.y - 0.5f - cameraOffset, this.gameObject.transform.GetChild(0).gameObject.transform.position.z);
    }
}
