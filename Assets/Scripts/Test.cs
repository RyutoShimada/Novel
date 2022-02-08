using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private IEnumerator _coroutine;
    Stack<IEnumerator> _stack = new Stack<IEnumerator>();

    private void Start()
    {
        _coroutine = RotateAsync();
        // StartCoroutine(_coroutine);
    }

    private void Update()
    {
        //// コルーチンを自前実行しなさい

        ////コルーチンがなければ何もしない
        //if (_coroutine == null) { return; }

        //// MoveNext() の結果が false (次の処理が存在しない) なら
        //// コルーチン終了でフィールドを null にする
        //if (!_coroutine.MoveNext())
        //{
        //    _coroutine = null;
        //}

        if (_coroutine == null) { return; }

        if (_coroutine.MoveNext())
        {
            if (_coroutine.Current is IEnumerator e)
            {
                _stack.Push(_coroutine);
                _coroutine = e; // 切り替え
            }
        }
        else if (_stack.Count != 0)
        {
            _coroutine = _stack.Pop();
        }
        else
        {
            Debug.Log("コルーチン終了");
            _coroutine = null;
        }
    }

    private IEnumerator RotateAsync()
    {
        Debug.Log("コルーチン開始");
        //while (true)
        {
            yield return RotateAxisAsync(180, Vector3.right);
            yield return RotateAxisAsync(180, Vector3.up);
            yield return RotateAxisAsync(180, Vector3.forward);
        }
    }

    IEnumerator RotateAxisAsync(int count, Vector3 axis)
    {
        Debug.Log("実行");
        for (var i = 0; i < count; i++)
        {
            transform.Rotate(axis);
            yield return null;
        }
        yield return WaitSecAsync(1); // ここ追加
        Debug.Log("終了");
    }

    private IEnumerator WaitSecAsync(float sec)
    {
        for (var t = 0F; t < sec; t += Time.deltaTime)
        {
            yield return null;
        }
    }
}
