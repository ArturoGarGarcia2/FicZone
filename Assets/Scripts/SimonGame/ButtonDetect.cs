using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class ButtonDetect : MonoBehaviour
{

    public int buttonIndex;
    public XRPushButton button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onPress.AddListener(() => ButtonPressed());
    }

    void ButtonPressed()
    {
        SimonGameManager.Instance.RegisterPlayerInput(buttonIndex);
    }
}
