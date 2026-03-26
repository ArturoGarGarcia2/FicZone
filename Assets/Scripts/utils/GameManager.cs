using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool[] puzzlesCompleted = new bool[5];

    public GameObject[] rewards;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        rewards[index].SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
