using UnityEngine;

public class NumericCodeChecker : MonoBehaviour
{
    public NumericCodeButton[] buttons; // 4 botones
    public bool puzzleSolved = false;

    public Hint pista;
    public bool pistaDada = false;

    public AudioSource sonidoGanar;
    void Update()
    {
        CheckCode();
    }

    void CheckCode()
    {
        if (ConnectionManager.Instance == null)
            return;

        string correctCode = ConnectionManager.Instance.correctCode;

        if (correctCode.Length < buttons.Length)
            return;

        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonValue = buttons[i].GetValue();
            int correctValue = correctCode[i] - '0';

            if (buttonValue != correctValue)
            {
                puzzleSolved = false;
                return;
            }
        }

        puzzleSolved = true;
        if (!pistaDada)
        {
            if (sonidoGanar != null)
                sonidoGanar.Play();
            pista.PuzzleResuelto("codigo");
            pistaDada = true;
        }
        GameManager.Instance.CompletePuzzle(0);
    }
}