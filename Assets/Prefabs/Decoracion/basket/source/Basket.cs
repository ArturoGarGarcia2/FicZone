using TMPro;
using UnityEngine;

public class Basket : MonoBehaviour
{

    public TextMeshPro text;
    private int puntuacion;
    public bool Lebron;

    void Start()
    {
        Lebron = false;
        puntuacion = 0;
    }
    private void Update()
    {
        text.SetText("Puntuacion: "+ puntuacion);
        if (puntuacion > 10)
        {
            Lebron = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("basketball"))
        {
            if(transform.position.y < other.transform.position.y)
            {
                puntuacion++;
            }
            else
            {
                text.SetText("No hagas trampa granujilla");
            }
        }
    }
}
