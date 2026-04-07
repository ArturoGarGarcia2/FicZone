using UnityEngine;
using TMPro;

public class NumericCodeButton : MonoBehaviour
{
    public int currentValue = 0;
    public TMP_Text displayText;

    void Start()
    {
        UpdateVisual();
    }

    // Esto lo llamas desde el evento del XR Push Button
    public void OnPressed()
    {
        currentValue++;
        if (currentValue > 9)
            currentValue = 0;

        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (displayText != null)
            displayText.text = currentValue.ToString();
    }

    public int GetValue()
    {
        return currentValue;
    }
}