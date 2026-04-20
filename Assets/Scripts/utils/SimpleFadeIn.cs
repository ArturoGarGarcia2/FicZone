using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleFadeIn : MonoBehaviour
{
    public Image panelImage;
    public float duration = 1f;

    void Start()
    {
        if (panelImage == null)
        {
            panelImage = GetComponent<Image>();
        }

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;

        // Empieza completamente negro
        SetAlpha(1f);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = 1f - (timer / duration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(0f);
    }

    void SetAlpha(float alpha)
    {
        if (panelImage == null) return;

        Color c = panelImage.color;
        c.a = alpha;
        panelImage.color = c;
    }
}