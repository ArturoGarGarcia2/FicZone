using UnityEngine;

public class EndRunManager : MonoBehaviour
{
    [SerializeField] MenuManager menu;

    public AudioSource sonidoGanarJuego;

    private void OnTriggerEnter(Collider other)
    {
        foreach(bool puzzle in GameManager.Instance.puzzlesCompleted)
            if(!puzzle) return;
        
        if (other.CompareTag("Player"))
        {
            if (sonidoGanarJuego != null)
                sonidoGanarJuego.Play();
            menu.EndRun();
        }
    }
}