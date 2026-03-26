using UnityEngine;
using TMPro;
using System.Text;

public class CodeManager : MonoBehaviour
{
    public DialNumber[] dials;

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
            GameManager.Instance.CompletePuzzle(0);
    }
}
