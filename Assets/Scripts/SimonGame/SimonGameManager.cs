using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimonGameManager : MonoBehaviour
{
    public static SimonGameManager Instance;


    public Light[] lights;
    public Colors[] colors;

    public List<int> sequence = new List<int>();

    public List<int> playerInput = new List<int>();

    public float lightDuration = 1f;

    public TMP_Text resultText;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resultText.text = "";
    }


    public void StartGame()
    {
        sequence.Clear();
        playerInput.Clear();
        resultText.text = "";
        NewRound();
    }

    void NewRound()
    {
        playerInput.Clear();
        int randomIndex = Random.Range(0, lights.Length);
        sequence.Add(randomIndex);

        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        yield return new WaitForSeconds(1f);

        resultText.text = "";

        foreach (int index in sequence)
        {
            lights[index].intensity = 0f;   
            lights[index].enabled = false; 
            if(lights[index] != null)
            {
                lights[index].enabled = true;   
                lights[index].color = GetUnityColor(colors[index]);
                lights[index].intensity = 5f;   
                yield return new WaitForSeconds(lightDuration);

                lights[index].intensity = 0f;   
                lights[index].enabled = false;  
            }
            yield return new WaitForSeconds(0.5f);
        }

    }


    Color GetUnityColor(Colors colorEnum)
    {
        switch (colorEnum)
        {
            case Colors.RED:
                return Color.red;
            case Colors.GREEN:
                return Color.green;
            case Colors.BLUE:
                return Color.blue;
            case Colors.YELLOW:
                return Color.yellow;
            default:
                return Color.white; 
        }
    }

    public void RegisterPlayerInput(int buttonIndex)
    {
        playerInput.Add(buttonIndex);

        if (sequence[playerInput.Count - 1] != buttonIndex)
        {
            // Error: el jugador se equivocó
            resultText.text = "¡Incorrecto!";
            StartCoroutine(Equivocarse());
            playerInput.Clear();
            //GameOver();
            //return;
        }

        if (playerInput.Count == sequence.Count)
        {
            // Completó la ronda correctamente
            if(sequence.Count >= 5)
            {
                resultText.text = "¡Ganaste!";
                StartCoroutine(Ganar());
                return;
            }
            resultText.text = "¡Correcto!";
            NewRound();
        }
    }

    IEnumerator Equivocarse()
    {
        yield return new WaitForSeconds(0.5f);

        
        foreach (Light light in lights)
        {
            if (light != null)
            {
                light.color = Color.red;
                light.enabled = true;
                light.intensity = 5f;
            }
        }

        yield return new WaitForSeconds(1f);

        
        foreach (Light light in lights)
        {
            if (light != null)
            {
                light.intensity = 0f;
                light.enabled = false;
            }
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(PlaySequence());
    }

    IEnumerator Ganar()
    {
        yield return new WaitForSeconds(0.5f);

        
        foreach (Light light in lights)
        {
            if (light != null)
            {
                light.color = Color.green;
                light.enabled = true;
                light.intensity = 5f;
            }
        }

        yield return new WaitForSeconds(1f);
    }
}
