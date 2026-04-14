using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class ButtonController : MonoBehaviour
{
    public Renderer lightInFront;
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

            if (index == -1)
            {
                Debug.LogError("No se encontró la bombilla en el array");
                return;
            }

            if (!SequenceManager.Instance.lightsOn[index])
            {
                SequenceManager.Instance.Press(index);
            }
        }
    }
}