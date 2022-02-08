using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AsyncNovelManager : MonoBehaviour
{
    [SerializeField] Text _message = default;
    [SerializeField] float _waitTime = default;

    void Start()
    {
        string[] data =
        {
            "AAAAAAAAAAAAAAAAAA",
            "BBBBBBBBBBBBBBBBBBBBBBB",
            "CCCCCCCCCCCCCCCCCCCCCCCCCCCCC"
        };
        StartCoroutine(ShowMessagesAsync(data));
    }

    IEnumerator ShowMessagesAsync(string[] messages)
    {
        foreach (var line in messages)
        {
            yield return null; // クリックのフラグが落ちるのを待つ
            yield return ShowMessageAsync(line);
            yield return null; // クリックのフラグが落ちるのを待つ
            while (!AnyAction()) { yield return null; }
        }
        yield return null;
    }

    IEnumerator ShowMessageAsync(string message)
    {
        _message.text = "";

        foreach (var ch in message)
        {
            _message.text += ch;

            float timer = 0;
            while (timer <= _waitTime)
            {
                timer += Time.deltaTime;
                if (AnyAction())
                {
                    _message.text = message; // 全て表示
                    yield break;
                }
                yield return null;
            }
        }
    }

    bool AnyAction()
    {
        if (Input.GetButtonDown("Submit"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
