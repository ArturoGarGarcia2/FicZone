using UnityEngine;

public class FirstStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TransitionManager.Instance.LoadSceneWithFade("MenuScene");
    }
}
