using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager instance;

    [SerializeField] private GameObject player;

    public GameObject panelConfig;

    public Slider sliderAlturaPlayer, sliderAlturaCinturon, sliderRotacionCinturon,
    sliderVelGir, sliderTamañoCinturon;

    public Button tipoRotacion;

    public bool modoDaltonico, rotacionASaltos, rectificarGiro;

    public ControllerInputActionManager controlRotacion;

    public float alturaPlayer, alturaCinturon, rotacionCinturon, velGiro, tamañoCinturón;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rotacionASaltos = true;

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
            player = GameObject.FindWithTag("Player");

            if(panelConfig == null)
            {
                panelConfig = player.GetComponent<PlayerConfig>().Menu;
            }

            sliderAlturaPlayer = panelConfig.transform.GetChild(0).GetChild(3).GetChild(2).GetComponent<Slider>();
            
            sliderAlturaCinturon = panelConfig.transform.GetChild(0).GetChild(3).GetChild(6).GetComponent<Slider>();

            sliderRotacionCinturon = panelConfig.transform.GetChild(0).GetChild(3).GetChild(7).GetComponent<Slider>();

            tipoRotacion = panelConfig.transform.GetChild(0).GetChild(3).GetChild(8).gameObject.GetComponent<Button>();

            tipoRotacion.onClick.AddListener (() => BotonTipoGiro(this.gameObject));

            sliderVelGir = panelConfig.transform.GetChild(0).GetChild(3).GetChild(9).GetComponent<Slider>();

            sliderTamañoCinturon = panelConfig.transform.GetChild(0).GetChild(3).GetChild(10).GetComponent<Slider>();

            GameObject RectificarGiro = panelConfig.transform.GetChild(0).GetChild(3).GetChild(11).gameObject;

            RectificarGiro.GetComponent<Button>().onClick.AddListener (() => BotonRectGiro(this.gameObject));

            controlRotacion = player.GetComponent<PlayerConfig>().controlDer;

            alturaPlayer = sliderAlturaPlayer.value * -0.5f;

            alturaCinturon = sliderAlturaCinturon.value * -1;

            rotacionCinturon = sliderRotacionCinturon.value;

            velGiro = sliderVelGir.value;

            tamañoCinturón = sliderTamañoCinturon.value;

            GameObject botonPanico = panelConfig.transform.GetChild(0).GetChild(3).GetChild(12).gameObject;
            botonPanico.GetComponent<Button>().onClick.AddListener (() => BotonPanico(this.gameObject));

            CambiarRotacion();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void BotonRectGiro(GameObject obj)
    {
        Debug.Log("Button clicked! Target object: " + obj.name);

        rectificarGiro = !rectificarGiro;
    }

    private void BotonPanico(GameObject obj)
    {
        PanicoManager.Instance.Panicked();
    }

    private void BotonTipoGiro(GameObject obj)
    {
        Debug.Log("Button clicked! Target object: " + obj.name);

        rotacionASaltos = !rotacionASaltos;
    }

    public void CambiarRotacion()
    {

        if (rotacionASaltos == true)
        {
            controlRotacion.m_SmoothTurnEnabled = true;

            tipoRotacion.image.color = Color.white;
            tipoRotacion.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = Color.black;
            tipoRotacion.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Actual: \n\nContinua (puede provocar mareos)";
        }
        else
        {
            controlRotacion.m_SmoothTurnEnabled = false;

            tipoRotacion.image.color = Color.black;
            tipoRotacion.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = Color.white;
            tipoRotacion.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Actual: \n\nPor saltos";
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
