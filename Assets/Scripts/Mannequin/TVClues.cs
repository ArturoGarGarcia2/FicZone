using UnityEngine;
using TMPro;
using System.Text;

public class TVClues : MonoBehaviour
{
    public static TVClues tvclues;
    [SerializeField] private TMP_Text clues;
    private StringBuilder totalClues = new StringBuilder();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!tvclues) tvclues = this;
        clues.text = "";
    }

    public void UpdateClues(string clue)
    {
        totalClues.AppendLine(clue);
        clues.text = totalClues.ToString();
    }
}
