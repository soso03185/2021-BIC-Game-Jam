using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{       
    [SerializeField] private float fadeTime = 2;

    private Image fadeImage;

    private float start = 0;
    private float end = 1;
    private float time = 0f;

    [SerializeField] private bool fadeType;
   

    private void Start()
    {
        fadeImage = GetComponent<Image>();

        start = fadeType ? 1 : 0;
        end = fadeType ? 0 : 1;

        StartCoroutine(FadeRoutine());
    }   

    private IEnumerator FadeRoutine()
    {
        Color fadecolor = fadeImage.color;
        time = 0f;
        fadecolor.a = 0.1f;

        while (fadecolor.a > 0 && fadecolor.a < 1)
        {
            time += Time.deltaTime / fadeTime;

            fadecolor.a = Mathf.Lerp(start, end, time);

            fadeImage.color = fadecolor;

            yield return null;
        }
    }


}
