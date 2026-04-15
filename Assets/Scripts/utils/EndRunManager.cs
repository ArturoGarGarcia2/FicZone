using UnityEngine;
using System.Collections;

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
            StartCoroutine(EndRunCoroutine());
        }
    }

    IEnumerator EndRunCoroutine()
    {
        yield return new WaitForSeconds(4f);
        menu.EndRun();
    }
}