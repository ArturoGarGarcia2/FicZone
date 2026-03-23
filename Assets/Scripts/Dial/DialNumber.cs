using UnityEngine;
using TMPro;
using System.Text;

public class DialNumber : MonoBehaviour
{
    public HingeJoint hinge;
    public int currentNumber;
    public TMP_Text txt;

    private int divisions = 10;

    void Update()
    {
        float angle = hinge.angle;

        float normalized = Mathf.InverseLerp(hinge.limits.min, hinge.limits.max, angle);

        currentNumber = Mathf.RoundToInt(normalized * (divisions - 1));

        txt.text = ""+currentNumber;
    }
}