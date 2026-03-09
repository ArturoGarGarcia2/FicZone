using UnityEngine;

public class Mannequin : MonoBehaviour
{
    public enum Location
    {
        Cocina,
        Baño,
        Pasillo,
        Sofa
    }

    public Location location;

    public Transform hatSocket;

    public Hat.Family currentHat;
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

        string debug = gameObject.name+"("+location+"): ";

        foreach(Location loc in possibleLooking)
        {
            debug += loc + " - ";
        }

        debug += "(mira a: "+lookingAtInt +"-"+ lookingAt +")(debe mirar a: "+lookingTarget+")";

        Debug.Log(debug);         
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
        if (hatSocket.childCount == 0)
            return;

        Hat hat = hatSocket.GetComponentInChildren<Hat>();
        if (hat != null)
        {
            currentHat = hat.family;
        }
    }
    
    public bool CorrectMannequin()
    {
        return lookingAt == lookingTarget && currentHat == targetHat;
    }
}