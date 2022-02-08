using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] Image _fadeImage = default;
    [SerializeField] Image _switchImage = default;
    [SerializeField] float _fadeTime = default;

    public void StartFade()
    {
        StartCoroutine(Fade(_fadeImage));
    }

    public void StartSwitch()
    {
        StartCoroutine(Fade(_switchImage));
    }

    IEnumerator Timer()
    {
        float time = 0;

        while (time < _fadeTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator Fade(Image img)
    {
        Color c = img.color;

        if (_fadeTime <= 0)
        {
            _fadeTime = 1;
        }

        StartCoroutine(Timer());

        if (c.a > 0)
        {
            while (c.a > 0)
            {
                c.a -= Time.deltaTime / _fadeTime;
                img.color = c;
                yield return null;
            }
        }
        else
        {
            while (c.a < 1)
            {
                c.a += Time.deltaTime / _fadeTime;
                img.color = c;
                yield return null;
            }
        }
    }

    IEnumerator Switch(Image[] imgs)
    {
        yield return null;
    }
}
