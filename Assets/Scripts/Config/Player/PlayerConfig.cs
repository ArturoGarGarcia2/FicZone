using UnityEngine;

public class PlayerConfig : MonoBehaviour
{

    public GameObject configManager;

    public float cameraOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        configManager = GameObject.Find("ConfigManager");
    }

    // Update is called once per frame
    void Update()
    {
        cameraOffset = configManager.GetComponent<ConfigManager>().alturaPlayer;

        this.gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(this.gameObject.transform.GetChild(0).gameObject.transform.position.x, this.gameObject.transform.position.y - 0.5f - cameraOffset, this.gameObject.transform.GetChild(0).gameObject.transform.position.z);
    }
}
