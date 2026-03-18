using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SpawnCubos : MonoBehaviour
{
    public GameObject prefabCubo;

    private void Start()
    {
        SpawnNuevoCubo();
    }
    public void SpawnNuevoCubo()
    {
        GameObject nuevoCubo = Instantiate(prefabCubo, transform.position, transform.rotation);
        XRGrabInteractable grab = nuevoCubo.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.selectEntered.AddListener(AlCogerCubo);
        }
    }
    private void AlCogerCubo(SelectEnterEventArgs args)
    {
        args.interactableObject.transform.GetComponent<XRGrabInteractable>().selectEntered.RemoveListener(AlCogerCubo);
        // destruir el cubo si no esta agarrado 
        //Destroy(args.interactableObject.transform.gameObject, 60f);
        Invoke(nameof(SpawnNuevoCubo), 0.5f);
    }

}
