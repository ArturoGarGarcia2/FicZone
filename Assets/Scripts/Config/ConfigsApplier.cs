using UnityEngine;

public class ConfigsApplier : MonoBehaviour
{
    public GameObject configManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        configManager = GameObject.Find("ConfigManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
