using UnityEngine;
using TMPro;
using System.Text;

public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager Instance;

    public TMP_Text txt;
    public CablePort[] ports;

    public string correctCode { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        AssignColors();
        correctCode = GenerateCorrectCode();
        txt.text = "";
        Debug.Log("Código correcto guardado: " + correctCode);
    }

    void Update()
    {
        if (!AllConnected())
        {
            txt.text = "";
            return;
        }

        bool allCorrect = AllCorrect();

        string code = GenerateCode(allCorrect);

        txt.text = allCorrect
            ? "Código: " + code
            : "Código: " + code;
    }

    bool AllConnected()
    {
        foreach (CablePort cp in ports)
            if (!cp.conected)
                return false;

        return true;
    }

    bool AllCorrect()
    {
        foreach (CablePort cp in ports)
            if (!cp.correct)
                return false;

        return true;
    }

    void AssignColors()
    {
        Colors[] colors =
        {
            Colors.RED,
            Colors.GREEN,
            Colors.BLUE,
        };

        ShuffleArray(colors);

        for (int i = 0; i < ports.Length; i++)
            ports[i].color = colors[i];
    }

    void ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, array.Length);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    string GenerateCorrectCode()
    {
        StringBuilder sb = new StringBuilder();
        foreach (CablePort cp in ports)
            sb.Append((int)cp.color);

        int seed = StableHash(sb.ToString());
        System.Random rng = new System.Random(seed);

        int length = 4;
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < length; i++)
            result.Append(rng.Next(0, 10));

        return result.ToString();
    }

    string GetCurrentOrderKey()
    {
        StringBuilder sb = new StringBuilder();

        foreach (CablePort cp in ports)
        {
            if (cp.currentCable != null)
                sb.Append((int)cp.currentCable.color);
        }

        return sb.ToString();
    }

    int StableHash(string input)
    {
        int hash = 17;

        for (int i = 0; i < input.Length; i++)
            hash = hash * 31 + input[i];

        return hash;
    }

    string GenerateCode(bool correct)
    {
        string key = GetCurrentOrderKey();
        int seed = StableHash(key);

        System.Random rng = new System.Random(seed);

        int length = 4;
        StringBuilder result = new StringBuilder();

        if (correct)
        {
            for (int i = 0; i < length; i++)
                result.Append(rng.Next(0, 10));
        }
        else
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < length; i++)
                result.Append(chars[rng.Next(chars.Length)]);
        }

        return result.ToString();
    }
}