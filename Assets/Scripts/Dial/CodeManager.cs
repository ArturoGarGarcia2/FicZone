using UnityEngine;
using TMPro;
using System.Text;

public class CodeManager : MonoBehaviour
{
    public DialNumber[] dials;
    public TMP_Text txt;

    void Start()
    {
        
    }

    void Update()
    {
        string res = "";

        foreach(DialNumber dial in dials)
        {
            res += dial.currentNumber;
        }

        if(res == ConnectionManager.Instance.correctCode)
            txt.text = "Piola";
        else
            txt.text = "Piolan't";
    }
}
