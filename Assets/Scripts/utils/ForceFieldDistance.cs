using UnityEngine;

public class ForceFieldDistance : MonoBehaviour
{
    public Transform player;
    private float maxDistance = 20f;
    public float fadeSpeed = 3f;

    private Material mat;
    private float currentOpacity = 0f;
    
    // Guardamos los IDs para que sea súper eficiente
    private int opacityID, playerPosID, radiusID, blendID, speedID, cellSizeID;

    void Start()
    {
        mat = GetComponent<Renderer>().material;

        // Asignamos los IDs una sola vez
        opacityID = Shader.PropertyToID("_Opacity");
        playerPosID = Shader.PropertyToID("_PlayerPos");
        radiusID = Shader.PropertyToID("_SphereRadius");
        blendID = Shader.PropertyToID("_Blend");
        speedID = Shader.PropertyToID("_Speed");
        cellSizeID = Shader.PropertyToID("_CellSize");

        // Valores iniciales
        mat.SetFloat(opacityID, 0); 
        mat.SetFloat(speedID, 5);
        mat.SetFloat(radiusID, 1);
        mat.SetFloat(blendID, .5f);
        mat.SetFloat(cellSizeID, 30);
    }

    void Update()
    {
        if (player == null) return;

        // CORRECCIÓN: Usar SetVector y pasar la posición (.position)
        mat.SetVector(playerPosID, player.position);

        float dist = Vector3.Distance(player.position, transform.position);

        // Lógica de aparecer/desaparecer suave
        float targetOpacity = (dist < maxDistance) ? 1f : 0f;
        currentOpacity = Mathf.MoveTowards(currentOpacity, targetOpacity, Time.deltaTime * fadeSpeed);

        mat.SetFloat(opacityID, currentOpacity);
    }
}