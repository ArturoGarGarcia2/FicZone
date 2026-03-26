using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UVPattern3D : MonoBehaviour
{
    public static UVPattern3D Instance;

    public Light flashlight;
    public GameObject[] shapeObjects; 
    private string sequenceShapes = "";

    void Awake()
    {
        Instance = this;
    }

    public void SetSequence(string sequence)
    {
        sequenceShapes = sequence;
        UpdateShapesText();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
    void UpdateShapesText()
    {
        string[] symbols = sequenceShapes.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < symbols.Length && i < shapeObjects.Length; i++)
        {
            var textMesh = shapeObjects[i].GetComponent<TextMeshPro>();
            if (textMesh != null)
                textMesh.text = symbols[i];
        }
    }
}