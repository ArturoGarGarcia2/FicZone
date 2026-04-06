using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class UVText : MonoBehaviour
{
    public static UVText Instance;
    

    public Light flashlight;
    public TMP_Text text;

    float lookTimer = 0f;
    bool hasSentClue = false;
    bool isLooking = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        text.gameObject.SetActive(false);
    }

    void Update()
    {
        Vector3 dirToObj = text.gameObject.transform.position - flashlight.transform.position;
        float distance = dirToObj.magnitude;
        isLooking = true;

        // comprobar rango
        if (distance > flashlight.range)
        {
            //Debug.Log("Fuera de rango: " + distance + " Rango de la linterna: " + flashlight.range);
            text.gameObject.SetActive(false);
            isLooking = false;
            return;
        }

        // comprobar ángulo del cono
        float angle = Vector3.Angle(flashlight.transform.forward, dirToObj);

        if (angle > flashlight.spotAngle * 0.5f)
        {
            //Debug.Log("Fuera de cono");
            text.gameObject.SetActive(false);
            isLooking = false;
            return;
        }

        // comprobar si algo bloquea la luz
        if (Physics.Raycast(flashlight.transform.position, dirToObj.normalized, distance))
        {
            //Debug.Log("Algo en medio");
            text.gameObject.SetActive(false);
            isLooking = false;
            return;
        }

        //Debug.Log("Todo piola");
        text.gameObject.SetActive(true);
        if (isLooking)
        {
            lookTimer += Time.deltaTime;
        }
        else
        {
            lookTimer = 0f;
        }

        if (lookTimer >= 0.5f && !hasSentClue)
        {
            hasSentClue = true;
            string textoLimpio = text.text
            .Replace("\n", " ")
            .Replace("\r", " ")
            .Replace("\t", " ");
            textoLimpio = Regex.Replace(text.text, @"\s+", " ").Trim();
            TVClues.tvclues.UpdateClues(textoLimpio);
        }
        
    }
}
