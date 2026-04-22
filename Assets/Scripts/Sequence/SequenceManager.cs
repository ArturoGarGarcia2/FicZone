using TMPro;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using System.Collections;

public class SequenceManager : MonoBehaviour
{
    public static SequenceManager Instance;

    public Renderer[] lightbulbs;

    public GameObject[] lightbulbParents; 
    public GameObject[] buttons;

    public Material lightBulb;
    public Material[] colorMaterial;

    public bool daltonicMode = false;

    public bool[] lightsOn = new bool[5];

    public AudioSource sonidoGanar;

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
        Instance = this;
    }

    void Start()
    {

        daltonicMode = ConfigManager.instance.modoDaltonico;

        int n = Mathf.Min(lightbulbs.Length, buttons.Length, colors.Length);

        Colors[] shuffledColors = (Colors[])colors.Clone();
        F.ShuffleArray(shuffledColors);

        for (int i = 0; i < n; i++)
        {
            lightbulbs[i].GetComponentInParent<LightbulbColor>().color = shuffledColors[i];
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
            if (lightsOn[i])
            {
                Colors c = lightbulbs[i].GetComponentInParent<LightbulbColor>().color;
                SetEmission(i, lightbulbs[i].GetComponentInParent<LightbulbColor>().GetUnityColor());
            }
            else
            {
                TurnOffEmission(i);
            }

            // Daltonic mode
            if (daltonicMode && lightsOn[i])
            {
                GameObject text = lightbulbParents[i].transform.GetChild(1).gameObject;
                text.SetActive(true);

                Colors color = lightbulbs[i].GetComponentInParent<LightbulbColor>().color;
                var tmp = text.GetComponent<TextMeshPro>();

                if (tmp != null && ColorShapes.shapes.ContainsKey(color))
                    tmp.text = ColorShapes.shapes[color];
            }
            else
            {
                if (lightbulbs[i].transform.childCount > 2)
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

        playerSequence[sequenceStep] = lightbulbs[i].GetComponentInParent<LightbulbColor>().color;
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
        if (sonidoGanar != null)
            sonidoGanar.Play();
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
        for(int i = 0; i < lightbulbParents.Length; i++)
        {
            lightbulbParents[i].transform.GetChild(1).gameObject.SetActive(false);
        }

    }

    void SetLights(bool state)
    {
        for (int i = 0; i < lightsOn.Length; i++)
            lightsOn[i] = state;
    }

    void SetAllColors(Colors color)
    {
        foreach (Renderer b in lightbulbs)
            b.GetComponentInParent<LightbulbColor>().color = color;

    }

    void RestoreOriginalColors()
    {
        for (int i = 0; i < lightbulbs.Length; i++)
            lightbulbs[i].GetComponentInParent<LightbulbColor>().color = originalColors[i];
    }

    void SetEmission(int index, Color color, float intensity = 3f)
    {
        Renderer rend = lightbulbs[index];
        if (rend == null) return;

        Colors bulbColor = rend.GetComponentInParent<LightbulbColor>().color;

        // 👉 Cambiar material según color
        if (colorMaterial != null && (int)bulbColor < colorMaterial.Length)
        {
            rend.material = colorMaterial[(int)bulbColor];
        }

        // 👉 (Opcional) mantener emisión
        Material mat = rend.material;
        // mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", color * intensity);
    }

    void TurnOffEmission(int index)
    {
        Renderer rend = lightbulbs[index];
        if (rend == null) return;

        // 👉 Material apagado
        if (lightBulb != null)
        {
            rend.material = lightBulb;
        }

        // 👉 Quitar emisión
        Material mat = rend.material;
        mat.SetColor("_EmissionColor", Color.black);
    }
}