using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame() =>
        TransitionManager.Instance.LoadSceneWithFade("MapaCasa");
    public void EndRun() =>
        TransitionManager.Instance.LoadSceneWithFade("MenuScene");
}
