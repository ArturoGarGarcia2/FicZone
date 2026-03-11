using TMPro;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using System.Collections;

public class SequenceManager : MonoBehaviour
{
    public static SequenceManager Instance;

    public GameObject[] lightbulbs;
    public GameObject[] buttons;

    public bool[] lightsOn = new bool[5];

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
        Instance = this;
    }

    void Start()
    {
        int n = Mathf.Min(buttons.Length, lightbulbs.Length, colors.Length);

        F.ShuffleArray(lightbulbs);
        F.ShuffleArray(buttons);

        for (int i = 0; i < n; i++)
        {
            int index = i;

            lightbulbs[index].GetComponent<LightbulbColor>().color = colors[index];
            originalColors[index] = colors[index];

            buttons[index]
                .GetComponent<XRPushButton>()
                .onPress.AddListener(() => Press(index));
        }

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
            lightbulbs[i].transform.GetChild(1).gameObject.SetActive(lightsOn[i]);
    }

    void Press(int i)
    {
        if (inputLocked || puzzleSolved)
            return;

        if (lightsOn[i])
            return;

        lightsOn[i] = true;

        playerSequence[sequenceStep] =
            lightbulbs[i].GetComponent<LightbulbColor>().color;

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

        for (int j = 0; j < 2; j++)
        {
            SetAllColors(Colors.GREEN);
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

        inputLocked = false;
    }

    public void ResetPuzzle()
    {
        lightsOn = new bool[5];
        playerSequence = new Colors[5];
        sequenceStep = 0;

        RestoreOriginalColors();
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