using UnityEngine;
using TMPro;

public class NumericCodeButton : MonoBehaviour
{
    public int currentValue = 0;
    public TMP_Text displayText;
    public AudioSource sonidoBoton;

    void Start()
    {
        UpdateVisual();
    }

    // Esto lo llamas desde el evento del XR Push Button
    public void OnPressed()
    {
        if (sonidoBoton != null)
            sonidoBoton.Play();
            
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