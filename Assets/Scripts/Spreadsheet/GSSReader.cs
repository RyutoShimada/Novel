using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class GSSReader : MonoBehaviour
{
    public string SheetID = "読み込むシートのID";
    public string SheetName = "読み込むシート";
    public UnityEvent OnLoadEnd;
    public bool IsLoading { get; private set; }
    public string[][] Datas { get; private set; }

    private void Start()
    {
        Reload();
    }

    IEnumerator GetFromWeb()
    {
        IsLoading = true;
        Debug.Log(IsLoading);
        var tqx = "tqx=out:csv";
        var url = "https://docs.google.com/spreadsheets/d/1m-iHoJvDwpDzamZDM3ZLzJknMdIiF6dYdpWiagI7JRQ/edit?usp=sharing";// + SheetID + "/gviz/tq?" + tqx + "&sheet=" + SheetName;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        IsLoading = false;
        Debug.Log(IsLoading);
        //var protocol_error = request.result == UnityWebRequest.Result.ProtocolError ? true : false;
        //var connection_error = request.result == UnityWebRequest.Result.ConnectionError ? true : false;
        //if (protocol_error || connection_error)
        //{
        //    Debug.LogError(request.error);
        //}
        //else
        //{
        //    Datas = ConvertCSVtoJaggedArray(request.downloadHandler.text);
        //    OnLoadEnd.Invoke();
        //}
    }
    public void Reload() => StartCoroutine(GetFromWeb());
    static string[][] ConvertCSVtoJaggedArray(string t)
    {
        var reader = new StringReader(t);
        reader.ReadLine();  //ヘッダ読み飛ばし
        var rows = new List<string[]>();
        while (reader.Peek() >= 0)
        {
            var line = reader.ReadLine();        // 一行ずつ読み込み
            var elements = line.Split(',');    // 行のセルは,で区切られる
            for (var i = 0; i < elements.Length; i++)
            {
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
            }
            rows.Add(elements);
        }
        return rows.ToArray();
    }
}
