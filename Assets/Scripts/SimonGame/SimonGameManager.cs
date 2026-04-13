using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SimonGameManager : MonoBehaviour
{
    public static SimonGameManager Instance;

    public Renderer[] bulbs; // Aquí va el objeto con el material que tiene el cristal. El objeto está en root/GLTF_SceneRootNode/lightbulb_01_0/Object_4
    public Colors[] colors;

    public List<int> sequence = new List<int>();
    public List<int> playerInput = new List<int>();

    public float lightDuration = 1f;

    public TMP_Text resultText;

    public bool gameStart = false;

    public Hint pista;

    [Header("DEBUG")]
    public bool debugTurnOnAll;
    public bool debugTurnOffAll;

    void OnValidate()
    {
        if (debugTurnOnAll)
        {
            debugTurnOnAll = false;
            TurnOnAllDebug();
        }

        if (debugTurnOffAll)
        {
            debugTurnOffAll = false;
            TurnOffAllDebug();
        }
    }

    void TurnOnAllDebug()
    {
        for (int i = 0; i < bulbs.Length; i++)
        {
            SetEmission(i, GetUnityColor(colors[i]), 10f);
        }
    }

    void TurnOffAllDebug()
    {
        for (int i = 0; i < bulbs.Length; i++)
        {
            TurnOffEmission(i);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        resultText.text = "";

        // Apagar todas las bombillas al inicio
        for (int i = 0; i < bulbs.Length; i++)
        {
            TurnOffEmission(i);
        }
    }

    public void StartGame()
    {
        if (gameStart) return;

        for (int i = 0; i < bulbs.Length; i++)
        {
            TurnOffEmission(i);
        }

        gameStart = true;
        sequence.Clear();
        playerInput.Clear();
        resultText.text = "";

        NewRound();
    }

    void NewRound()
    {
        playerInput.Clear();
        int randomIndex = Random.Range(0, bulbs.Length);
        sequence.Add(randomIndex);

        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        yield return new WaitForSeconds(1f);

        resultText.text = "";

        foreach (int index in sequence)
        {
            SetEmission(index, GetUnityColor(colors[index]), 3f);

            yield return new WaitForSeconds(lightDuration);

            TurnOffEmission(index);

            yield return new WaitForSeconds(0.5f);
        }
    }

    Color GetUnityColor(Colors colorEnum) // Con esto vamos a poder cambiar el color emisivo de forma dinámica
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

        StartCoroutine(FlashBulb(buttonIndex));

        if (sequence[playerInput.Count - 1] != buttonIndex)
        {
            resultText.text = "¡Incorrecto!";
            StartCoroutine(Equivocarse());
            playerInput.Clear();
            return;
        }

        if (playerInput.Count == sequence.Count)
        {
            if (sequence.Count >= 5)
            {
                resultText.text = "¡Ganaste!";
                StartCoroutine(Ganar());
                pista.PuzzleResuelto("simon");
                GameManager.Instance.CompletePuzzle(1);
                return;
            }

            resultText.text = "¡Correcto!";
            NewRound();
        }
    }

    IEnumerator Equivocarse()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < bulbs.Length; i++)
        {
            SetEmission(i, Color.red, 3f);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < bulbs.Length; i++)
        {
            TurnOffEmission(i);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(PlaySequence());
    }

    IEnumerator Ganar()
    {
        gameStart = false;

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < bulbs.Length; i++)
        {
            SetEmission(i, Color.green, 3f);
        }

        yield return new WaitForSeconds(1f);
    }

    IEnumerator FlashBulb(int index)
    {
        SetEmission(index, GetUnityColor(colors[index]), 3f);

        yield return new WaitForSeconds(0.3f);

        TurnOffEmission(index);
    }

    void SetEmission(int index, Color color, float intensity) // La intensidad debe estar a 3 o casi no se ve. Se podría subir bastante más pero entonces el brillo es blanco y queda feo.
    {
        Renderer rend = bulbs[index];
        if (rend == null) return;

        Material mat = rend.material;

        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", color * intensity);
    }

    void TurnOffEmission(int index)
    {
        Renderer rend = bulbs[index];
        if (rend == null) return;

        Material mat = rend.material;

        mat.SetColor("_EmissionColor", Color.black);
    }
}