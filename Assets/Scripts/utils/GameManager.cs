using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool[] puzzlesCompleted = new bool[5];

    public GameObject[] rewards;

    public float timer;

    public TMP_Text timerText;

    private bool timerRunning = true;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    void Start()
    {
        foreach (GameObject reward in rewards)
        {
            reward.SetActive(false);
        }
    }

    public void CompletePuzzle(int index)
    {
        if (index < 0 || index >= puzzlesCompleted.Length) return;

        puzzlesCompleted[index] = true;
        if (rewards[index] != null)
            rewards[index].SetActive(true);;
        
        if(puzzlesCompleted[0] && puzzlesCompleted[1] && puzzlesCompleted[2] && puzzlesCompleted[3] && puzzlesCompleted[4])
        {
            StopTimer();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!timerRunning) return;
        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }       
        
    }

    public void StopTimer()
    {
        timerRunning = false;
    }
}
