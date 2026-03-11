using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager instance;

    public Slider sliderAlturaPlayer, sliderAlturaCinturon, sliderRotacionCinturon,
    sliderVelGir, sliderTamañoCinturon;

    public bool modoDaltonico;

    public float alturaPlayer, alturaCinturon, rotacionCinturon, velGiro, tamañoCinturón;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            alturaPlayer = sliderAlturaPlayer.value * -0.5f;

            alturaCinturon = sliderAlturaCinturon.value * -1;

            rotacionCinturon = sliderRotacionCinturon.value;

            velGiro = sliderVelGir.value;

            tamañoCinturón = sliderTamañoCinturon.value;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Juego");
    }

    public void ModoDaltonico()
    {

        if(modoDaltonico == false)
        {
            modoDaltonico = true;
        }
        else
        {
            modoDaltonico = false;
        }
    }
}
