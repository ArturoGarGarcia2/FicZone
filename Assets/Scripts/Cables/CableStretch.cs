using UnityEngine;

public class CableStretch : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform cableMesh;

    [Tooltip("La longitud real del modelo 3D original en el eje Y (antes de escalarlo)")]
    public float meshLength = 1.0f; 

    private Vector3 originalScale;

    void Start()
    {
        if (cableMesh != null)
            originalScale = cableMesh.localScale;
    }

    void Update()
    {
        if (startPoint == null || endPoint == null || cableMesh == null) return;

        Vector3 direction = endPoint.position - startPoint.position;
        float distance = direction.magnitude;

        cableMesh.position = startPoint.position + (direction / 2.0f);

        if (direction != Vector3.zero)
            cableMesh.up = direction;

        float scaleY = distance / meshLength;
        
        cableMesh.localScale = new Vector3(
            originalScale.x,
            scaleY,
            originalScale.z
        );
    }
}