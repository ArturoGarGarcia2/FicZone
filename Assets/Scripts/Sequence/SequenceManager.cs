using TMPro;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using System.Collections;

public class SequenceManager : MonoBehaviour
{
    public static SequenceManager Instance;

    public GameObject[] lightbulbs; 
    public GameObject[] buttons;    

    public bool daltonicMode = false;

    public bool[] lightsOn = new bool[5];

    public Hint pista;
    Colors[] originalColors = new Colors[5];
    private Colors[] colors =
    {
        Colors.RED,
        Colors.GREEN,
        Colors.BLUE,
        Colors.YELLOW,
        Colors.PURPLE,
    };

    public Colors[] sequence =
    {
        Colors.RED,
        Colors.GREEN,
        Colors.BLUE,
        Colors.YELLOW,
        Colors.PURPLE,
    };

    private Colors[] playerSequence = new Colors[5];

    int sequenceStep = 0;
    bool puzzleSolved = false;
    bool inputLocked = false;

    void Awake()
    {
        F.ShuffleArray(sequence);
        foreach (var text in lightbulbs)
            text.transform.GetChild(2).gameObject.SetActive(false);
        Instance = this;
    }

    void Start()
    {
        int n = Mathf.Min(lightbulbs.Length, buttons.Length, colors.Length);

        Colors[] shuffledColors = (Colors[])colors.Clone();
        F.ShuffleArray(shuffledColors);

        for (int i = 0; i < n; i++)
        {
            lightbulbs[i].GetComponent<LightbulbColor>().color = shuffledColors[i];
            originalColors[i] = shuffledColors[i];
        }     

        for (int i = 0; i < n; i++)
            playerSequence[i] = default;      

        

        string sequenceShapes = "";
        foreach (Colors c in sequence)
        {
            sequenceShapes += ColorShapes.shapes[c] + " ";
        }
        UVPattern3D.Instance.SetSequence(sequenceShapes);
        
    }

    void Update()
    {
        for (int i = 0; i < lightbulbs.Length; i++)
        {
            lightbulbs[i].transform.GetChild(1).gameObject.SetActive(lightsOn[i]);

            if(daltonicMode && lightsOn[i])
            {
                GameObject text = lightbulbs[i].transform.GetChild(2).gameObject;

                text.SetActive(true);

                Colors color = lightbulbs[i].GetComponent<LightbulbColor>().color;

                var tmp = text.GetComponent<TextMeshPro>();
                if (tmp != null && ColorShapes.shapes.ContainsKey(color))
                    tmp.text = ColorShapes.shapes[color];
            }
            else
            {
                lightbulbs[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public void Press(int i)
    {
        if (inputLocked || puzzleSolved)
            return;

        if (lightsOn[i])
            return;

        lightsOn[i] = true;

        playerSequence[sequenceStep] = lightbulbs[i].GetComponent<LightbulbColor>().color;
        sequenceStep++;

        if (sequenceStep == sequence.Length)
        {
            CheckSequence();
        }
    }

    void CheckSequence()
    {
        for (int i = 0; i < sequence.Length; i++)
        {
            if (playerSequence[i] != sequence[i])
            {
                StartCoroutine(FailRoutine());
                return;
            }
        }

        StartCoroutine(SuccessRoutine());
    }

    IEnumerator SuccessRoutine()
    {
        inputLocked = true;
        puzzleSolved = true;
        pista.PuzzleResuelto("secuencia");
        GameManager.Instance.CompletePuzzle(2);

        for (int j = 0; j < 2; j++)
        {
            SetAllColors(Colors.GREEN);
            SetLights(true);
            yield return new WaitForSeconds(0.3f);

            SetLights(false);
            yield return new WaitForSeconds(0.3f);
        }

        SetAllColors(Colors.GREEN);
        SetLights(true);
    }

    IEnumerator FailRoutine()
    {
        inputLocked = true;

        SetAllColors(Colors.RED);
        SetLights(true);

        yield return new WaitForSeconds(1.5f);

        ResetPuzzle();
        RestoreOriginalColors();

        inputLocked = false;
    }

    public void ResetPuzzle()
    {
        lightsOn = new bool[5];
        playerSequence = new Colors[5];
        sequenceStep = 0;

    }

    void SetLights(bool state)
    {
        for (int i = 0; i < lightsOn.Length; i++)
            lightsOn[i] = state;
    }

    void SetAllColors(Colors color)
    {
        foreach (GameObject b in lightbulbs)
            b.GetComponent<LightbulbColor>().color = color;
    }

    void RestoreOriginalColors()
    {
        for (int i = 0; i < lightbulbs.Length; i++)
            lightbulbs[i].GetComponent<LightbulbColor>().color = originalColors[i];
    }

}