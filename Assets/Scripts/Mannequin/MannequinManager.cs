using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MannequinManager : MonoBehaviour
{
    public Mannequin[] mannequins;

    public TMP_Text[] clueTexts;

    private Dictionary<Mannequin.Location, Hat.Family> hatSolution =
        new Dictionary<Mannequin.Location, Hat.Family>();

    private Dictionary<Mannequin.Location, Mannequin.Location> lookSolution =
        new Dictionary<Mannequin.Location, Mannequin.Location>();

    void Start()
    {
        GenerateSolution();
        GenerateClues();
    }

    void Update()
    {
        AlignMannequins();
    }

    public void AlignMannequins()
    {
        foreach (var m in mannequins)
        {
            Mannequin target = null;
            foreach (var other in mannequins)
            {
                if (other.location == m.lookingTarget)
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
                    float angle = Mathf.Atan2(dir.y, dir.z) * Mathf.Rad2Deg;

                    Transform child = m.transform.GetChild(0);
                    child.localEulerAngles = new Vector3(0, angle, 0);

                    m.lookingAt = target.location;
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
        F.ShuffleArray(locs);

        for (int i = 0; i < 4; i++)
        {
            mannequins[i].lookingTarget = locs[i];
            mannequins[i].targetHat = hats[i];
            Debug.Log("Mannequin Manager: "+ mannequins[i] + " " + locs[i] + " " + hats[i]);
        }
    }

    string LocationToString(Mannequin.Location loc)
    {
        switch (loc)
        {
            case Mannequin.Location.Cocina: return "cocina";
            case Mannequin.Location.Baño: return "baño";
            case Mannequin.Location.Pasillo: return "pasillo";
            case Mannequin.Location.Sofa: return "sofá";
        }

        return "";
    }

    string FamilyToString(Hat.Family fam)
    {
        switch (fam)
        {
            case Hat.Family.Padre: return "padre";
            case Hat.Family.Madre: return "madre";
            case Hat.Family.Hijo: return "hijo";
            case Hat.Family.Hija: return "hija";
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
                "El sombrero del " + fam + " está en el maniquí del " + loc + ".";

            i++;
        }

        foreach (var pair in lookSolution)
        {
            string from = LocationToString(pair.Key);
            string to = LocationToString(pair.Value);

            clueTexts[i].text =
                "El maniquí del " + from + " está mirando al maniquí del " + to + ".";

            i++;
        }
    }

    public void CheckPuzzle()
    {
        foreach (var m in mannequins)
        {
            if(!m.CorrectMannequin()) return;
        }

        Debug.Log("Puzzle resuelto!");
    }
}