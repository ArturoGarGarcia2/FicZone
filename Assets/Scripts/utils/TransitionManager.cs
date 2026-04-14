using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    public string fadePanelName = "FadePanel"; // 👈 ponle este nombre en cada escena
    private GameObject fadePanel;
    private Image panelImage;

    public float fadeDuration = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(WaitForCameraAndAssign());
    }

    IEnumerator WaitForCameraAndAssign()
    {
        // Espera a que la cámara exista (muy importante en VR)
        while (Camera.main == null)
            yield return null;

        AssignFadePanel();
    }

    void AssignFadePanel()
    {
        Transform[] allTransforms = FindObjectsOfType<Transform>(true);

        foreach (Transform t in allTransforms)
        {
            if (t.name == fadePanelName)
            {
                fadePanel = t.gameObject;
                panelImage = fadePanel.GetComponent<Image>();

                if (panelImage == null)
                {
                    Debug.LogError("FadePanel encontrado pero NO tiene Image.");
                }

                return;
            }
        }

        Debug.LogError("No se encontró el FadePanel en la escena.");
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndSwitch(sceneName));
    }

    IEnumerator FadeAndSwitch(string sceneName)
    {
        yield return StartCoroutine(FadeOut());

        SceneManager.LoadScene(sceneName);

        yield return StartCoroutine(WaitForCameraAndAssign());

        yield return StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = 1f - (timer / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(0f);
    }

    IEnumerator FadeOut()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration;
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(1f);
    }

    void SetAlpha(float alpha)
    {
        if (panelImage == null) return;

        Color c = panelImage.color;
        c.a = alpha;
        panelImage.color = c;
    }
}