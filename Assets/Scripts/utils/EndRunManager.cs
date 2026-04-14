using UnityEngine;

public class EndRunManager : MonoBehaviour
{
    [SerializeField] MenuManager menu;

    private void OnTriggerEnter(Collider other)
    {
        foreach(bool puzzle in GameManager.Instance.puzzlesCompleted)
            if(!puzzle) return;
        
        if (other.CompareTag("Player"))
        {
            menu.EndRun();
        }
    }
}