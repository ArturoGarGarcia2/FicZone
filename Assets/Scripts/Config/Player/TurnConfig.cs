using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class TurnConfig : MonoBehaviour
{

    public GameObject configManager;

    public float turnVel;

    public float turnSpeedBase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        configManager = GameObject.Find("ConfigManager");

        turnSpeedBase = this.gameObject.GetComponent<ContinuousTurnProvider>().turnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<ContinuousTurnProvider>().turnSpeed = configManager.GetComponent<ConfigManager>().velGiro * turnSpeedBase;
    }
}
