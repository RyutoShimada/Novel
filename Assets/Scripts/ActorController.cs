using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorController : MonoBehaviour
{
    [SerializeField] float _fadeTime = default;
    [SerializeField] Color _notTurnColor = default;
    Image _image = default;
    Color _color = default;

    void Start()
    {
        _image = GetComponent<Image>();
        _color = _image.color;
        _color.a = 0;
        _image.color = _color;
        OnActive(true);
    }

    public void OnActive(bool active)
    {
        if (active)
        {
            StartCoroutine(FadeIn(_fadeTime));
        }
        else
        {
            StartCoroutine(FadeOut(_fadeTime));
        }
    }

    void OnTurn() => _image.color = Color.white;

    void UnTurn() => _image.color = _notTurnColor;

    /// <summary>
    /// フェードイン
    /// </summary>
    /// <param name="fadeTime">かける時間</param>
    /// <returns></returns>
    IEnumerator FadeIn(float fadeTime)
    {
        float timer = 0;

        if (!gameObject.activeSelf) gameObject.SetActive(true);

        while (true)
        {
            timer += Time.deltaTime;

            if (timer > fadeTime)
            {
                UnTurn();
                yield break;
            }
            else
            {
                _color.a = timer / fadeTime;
                _image.color = _color;
            }

            yield return null;
        }
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    /// <param name="fadeTime">かける時間</param>
    /// <returns></returns>
    IEnumerator FadeOut(float fadeTime)
    {
        float timer = fadeTime;

        while (true)
        {
            timer -= Time.deltaTime;

            if (timer > fadeTime)
            {
                if (gameObject.activeSelf) gameObject.SetActive(false);
                yield break;
            }
            else
            {
                _color.a = timer / fadeTime;
                _image.color = _color;
            }

            yield return null;
        }
    }
}
