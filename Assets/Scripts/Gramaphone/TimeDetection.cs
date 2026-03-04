using TMPro;
using UnityEngine;

public class TimeDetection : MonoBehaviour
{

    public static TimeDetection Instance;

    private float timer = 30f;

    private float maxTime = 180f;

    public AudioSource musicSource;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0f)
        {
            if(!musicSource.isPlaying)
            {
                musicSource.Play();
            }
            timer -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            if(timer <= 10f)
            {
                musicSource.pitch = 1f - (10f - timer) * 0.1f;
            }


            if(timer <= 0f)
            {
                timer = 0f;
                musicSource.Stop();
                
            }
        }
    }

    public void AddTime()
    {
        if(timer < maxTime)
        {
            timer += 5f;
            if(timer > maxTime)
                timer = maxTime;

            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
        }
    }
}
