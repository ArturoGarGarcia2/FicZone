using UnityEngine;
using TMPro;

public class Mannequin : MonoBehaviour
{
    public enum Location
    {
        Cocina,
        Baño,
        Pasillo,
        Sofa
    }

    public TMP_Text txt;
    public HatSocket hs;
    public Location location;

    public Transform hatSocket;

    public Hat.Family currentHat = Hat.Family.None;
    public Hat.Family targetHat;

    public Location lookingAt;
    public Location lookingTarget;

    private Location[] possibleLooking = new Location[3];
    private int lookingAtInt = 0;

    void Start()
    {
        int ite = 0;
        for(int i = 0; i < 4; i++)
            if((Location)i != location)
            {
                possibleLooking[ite] = (Location)i;
                ite++;
            }

        lookingAt = possibleLooking[lookingAtInt];
    }

    void Update()
    {
        UpdateHat();
        // Lights();
        txt.text = lookingAt+"\n"+lookingTarget+"\n\n"+currentHat+"\n"+targetHat;
    }

    public void Next()
    {
        lookingAtInt++;
        if(lookingAtInt > 2)
            lookingAtInt = 0;
        
        lookingAt = possibleLooking[lookingAtInt];
    }

    public void Prev()
    {
        lookingAtInt--;
        if(lookingAtInt < 0)
            lookingAtInt = 2;
        
        lookingAt = possibleLooking[lookingAtInt];
    }

    public void UpdateHat()
    {
        currentHat = hs.currentHatInSocket;
    }
    
    public bool CorrectMannequin()
    {
        return lookingAt == lookingTarget && currentHat == targetHat;
    }

    public void Lights() => transform.GetChild(4).gameObject.SetActive(CorrectMannequin());
}