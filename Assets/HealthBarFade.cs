using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFade : MonoBehaviour
{

    bool fadeIn = false;
    public float secondsToFadeIn = .5f;
    float fadeInTimer;

    public CanvasGroup group;
    void Start()
    {
        group.alpha = 0;
    }
    private void OnEnable()
    {
        fadeIn = true;
        fadeInTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            fadeInTimer += Time.deltaTime;

            group.alpha = Mathf.Lerp(0, 1, fadeInTimer / secondsToFadeIn);
            if (fadeInTimer >= secondsToFadeIn)
            {
                fadeIn = false;                
            }
        }
    }
}
