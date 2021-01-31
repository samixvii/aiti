using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public float fadeTime = 2;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public float Fade(Color color)
    {
        image.color = color;
        image.CrossFadeAlpha(0, 0, true);
        FadeOut();
        Invoke(nameof(FadeIn), fadeTime * 2 / 3);
        return fadeTime / 2;
    }

    void FadeIn()
    {
        image.CrossFadeAlpha(0, fadeTime / 3, true);
    }

    void FadeOut()
    {
        image.CrossFadeAlpha(1, fadeTime / 3, true);
    }

}
