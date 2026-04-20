using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame() =>
        TransitionManager.Instance.LoadSceneWithFade("MapaCasa");
    public void LobbyGame() =>
        TransitionManager.Instance.LoadSceneWithFade("Lobby");
    public void EndRun() =>
        TransitionManager.Instance.LoadSceneWithFade("MenuScene");
}
