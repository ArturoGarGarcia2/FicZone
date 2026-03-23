using UnityEngine;

public class CableStretch : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform cableMesh;

    void Update()
    {
        Vector3 direction = endPoint.position - startPoint.position;

        // Posicionar en el punto medio
        cableMesh.position = startPoint.position + direction / 2f;

        // Rotar hacia el extremo
        cableMesh.up = direction;

        // Escalar en Y según distancia
        float distance = direction.magnitude;
        cableMesh.localScale = new Vector3(
            cableMesh.localScale.x,
            distance / 3.5f,
            cableMesh.localScale.z
        );
    }
}