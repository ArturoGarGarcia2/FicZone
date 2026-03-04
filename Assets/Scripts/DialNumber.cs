using UnityEngine;
using TMPro;
using System.Text;

public class DialNumber : MonoBehaviour
{
    public HingeJoint hinge;      // tu Hinge Joint
    public int currentNumber;     // número actual del dial (0-9)
    public TMP_Text txt;

    private int divisions = 10;   // 10 divisiones: 0-9

    void Update()
    {
        // ángulo actual del Hinge Joint
        float angle = hinge.angle; // devuelve entre min y max

        // Normalizamos entre 0 y 1 según el rango del Hinge Joint
        float normalized = Mathf.InverseLerp(hinge.limits.min, hinge.limits.max, angle);

        // Mapeamos a 0-9 y redondeamos
        currentNumber = Mathf.RoundToInt(normalized * (divisions - 1));

        txt.text = ""+currentNumber;
        // Debug para ver el valor
        // Debug.Log("Dial number: " + currentNumber);
    }
}