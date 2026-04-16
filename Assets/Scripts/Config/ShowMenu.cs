using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowMenu : MonoBehaviour
{

    public GameObject menu;

    bool activo = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activo)
        {
            menu.gameObject.SetActive(true);
        }
        else
        {
            menu.gameObject.SetActive(false);
        }
    }

    public void Volver()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ActivarMenu()
    {
        activo = !activo;
    }

    public void ModoDaltonico()
    {
        ConfigManager.instance.modoDaltonico = !ConfigManager.instance.modoDaltonico;
    }
}
