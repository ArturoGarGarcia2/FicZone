using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MannequinManager : MonoBehaviour
{
    public static MannequinManager Instance;
    public Mannequin[] mannequins;

    public TMP_Text[] clueTexts;
    public TMP_Text debugText;

    private Dictionary<Mannequin.Location, Hat.Family> hatSolution =
        new Dictionary<Mannequin.Location, Hat.Family>();

    private Dictionary<Mannequin.Location, Mannequin.Location> lookSolution =
        new Dictionary<Mannequin.Location, Mannequin.Location>();

    public float rotationSmooth = 5f;

    public Hint pista;
    public bool pistaDada = false;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        F.ShuffleArray(clueTexts);
        GenerateSolution();
        GenerateClues();
    }

    void Update()
    {
        AlignMannequins();
        CheckPuzzle();
    }

    public void AlignMannequins()
    {
        foreach (var m in mannequins)
        {
            Mannequin target = null;

            foreach (var other in mannequins)
            {
                if (other.location == m.lookingAt)
                {
                    target = other;
                    break;
                }
            }

            if (target != null && m.transform.childCount > 0)
            {
                Vector3 dir = target.transform.position - m.transform.position;
                dir.y = 0;

                if (dir != Vector3.zero)
                {
                    Transform child = m.transform.GetChild(0);

                    Quaternion targetRot = Quaternion.LookRotation(dir);

                    child.rotation = Quaternion.Slerp(
                        child.rotation,
                        targetRot,
                        rotationSmooth * Time.deltaTime
                    );
                }
            }
        }
    }

    void GenerateSolution()
    {
        Hat.Family[] hats =
        {
            Hat.Family.Padre,
            Hat.Family.Madre,
            Hat.Family.Hijo,
            Hat.Family.Hija
        };

        Mannequin.Location[] locs =
        {
            Mannequin.Location.Baño,
            Mannequin.Location.Cocina,
            Mannequin.Location.Pasillo,
            Mannequin.Location.Sofa,
        };

        F.ShuffleArray(hats);

        bool valid = false;

        while (!valid)
        {
            F.ShuffleArray(locs);
            valid = true;

            for (int i = 0; i < mannequins.Length; i++)
            {
                if (mannequins[i].location == locs[i])
                {
                    valid = false;
                    break;
                }
            }
        }

        hatSolution.Clear();
        lookSolution.Clear();

        for (int i = 0; i < 4; i++)
        {
            mannequins[i].lookingTarget = locs[i];
            mannequins[i].targetHat = hats[i];

            hatSolution.Add(mannequins[i].location, hats[i]);
            lookSolution.Add(mannequins[i].location, locs[i]);
        }
    }

    string LocationToString(Mannequin.Location loc)
    {
        switch (loc)
        {
            case Mannequin.Location.Cocina: return "de la cocina";
            case Mannequin.Location.Baño: return "del baño";
            case Mannequin.Location.Pasillo: return "del pasillo";
            case Mannequin.Location.Sofa: return "del sofá";
        }

        return "";
    }

    string FamilyToString(Hat.Family fam)
    {
        switch (fam)
        {
            case Hat.Family.Padre: return "El padre";
            case Hat.Family.Madre: return "La madre";
            case Hat.Family.Hijo: return "El hijo";
            case Hat.Family.Hija: return "La hija";
        }

        return "";
    }

    void GenerateClues()
    {
        int i = 0;

        foreach (var pair in hatSolution)
        {
            string loc = LocationToString(pair.Key);
            string fam = FamilyToString(pair.Value);

            clueTexts[i].text =
                fam + " está\nen " + loc + ".";

            i++;
        }

        foreach (var pair in lookSolution)
        {
            string from = LocationToString(pair.Key);
            string to = LocationToString(pair.Value);

            clueTexts[i].text =
                "El maniquí " + from + " mira\n al " + to + ".";

            i++;
        }
    }

    public void CheckPuzzle()
    {
        foreach (var m in mannequins)
            if(!m.CorrectMannequin()){
                debugText.text = "Está mal";
                return;
            }

        debugText.text = "Está perfe";
        GameManager.Instance.CompletePuzzle(4);
        if (!pistaDada)
        {
            pista.PuzzleResuelto("maniqui");
            pistaDada = true;
        }
    }
}