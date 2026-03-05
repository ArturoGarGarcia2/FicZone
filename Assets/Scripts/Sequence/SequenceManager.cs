using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class SequenceManager : MonoBehaviour
{
    public GameObject[] lightbulbs;
    public GameObject[] buttons;
    public bool[] lightsOn = new bool[5];
    private Colors[] colors =
    {
        Colors.RED,
        Colors.GREEN,
        Colors.BLUE,
        Colors.YELLOW,
        Colors.PURPLE,
    };
    private Colors[] sequence =
    {
        Colors.RED,
        Colors.GREEN,
        Colors.BLUE,
        Colors.YELLOW,
        Colors.PURPLE,
    };
    private Colors[] playerSequence = new Colors[5];
    int sequenceStep = 0;

    void Start()
    {
        int n = Mathf.Min(buttons.Length, lightbulbs.Length, colors.Length);
        F.ShuffleArray(lightbulbs);
        F.ShuffleArray(buttons);

        for (int i = 0; i < n; i++)
        {
            int index = i;

            lightbulbs[index].GetComponent<LightbulbColor>().color = colors[index];

            buttons[index]
                .GetComponent<XRPushButton>()
                .onPress.AddListener(() => Press(index));
        }

        F.ShuffleArray(sequence);

        foreach(Colors c in sequence)
            Debug.Log(c);
    }

    void Update()
    {
        for(int i = 0; i < lightbulbs.Length; i++)
            lightbulbs[i].transform.GetChild(1).gameObject.SetActive(lightsOn[i]);
        
        for(int i = 0; i < colors.Length; i++)
        {
            if(playerSequence[i] == null) continue;
            if(playerSequence[i] != sequence[i])
            {
                lightsOn = new bool[5];
                playerSequence = new Colors[5];
                sequenceStep = 0;
                return;
            }
            if(sequenceStep == 5 && playerSequence[i] == sequence[i])
                Debug.Log("PIOLÍSIMA");
        }
    }

    void Press(int i)
    {
        lightsOn[i] = !lightsOn[i];
        playerSequence[sequenceStep] = lightbulbs[i].GetComponent<LightbulbColor>().color;
        sequenceStep++;
    }
}