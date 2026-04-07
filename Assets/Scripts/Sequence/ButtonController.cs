using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class ButtonController : MonoBehaviour
{
    public GameObject lightInFront;

    private XRPushButton pushButton;

    void Awake()
    {
        pushButton = GetComponent<XRPushButton>();
        pushButton.onPress.AddListener(OnPress);
    }

    void OnPress()
    {
        if (lightInFront != null)
        {
            int index = System.Array.IndexOf(SequenceManager.Instance.lightbulbs, lightInFront);
            if (!SequenceManager.Instance.lightsOn[index])
            {
                SequenceManager.Instance.Press(index);
            }
        }
    }
}