using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager instance;

    public Slider sliderAlturaPlayer, sliderAlturaCinturon, sliderRotacionCinturon,
    sliderVelGir, sliderTamañoCinturon;

    public Button tipoRotacion;

    public bool modoDaltonico, rotacionASaltos, rectificarGiro;

    public ControllerInputActionManager controlRotacion;

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
        SceneManager.LoadScene("MapaCasa");
    }

    public void CambiarRotacion()
    {
        rotacionASaltos = !rotacionASaltos;

        if (rotacionASaltos == true)
        {
            controlRotacion.m_SmoothTurnEnabled = false;

            tipoRotacion.image.color = Color.black;
            tipoRotacion.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = Color.white;
            tipoRotacion.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Actual: \n\nPor saltos";
        }
        else
        {
            controlRotacion.m_SmoothTurnEnabled = true;

            tipoRotacion.image.color = Color.white;
            tipoRotacion.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = Color.black;
            tipoRotacion.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Actual: \n\nContinua (puede provocar mareos)";
        }
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

    public void RectificarGiro()
    {
        rectificarGiro = !rectificarGiro;
    }
}
