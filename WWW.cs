using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WWW : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://gitcode.net/weixin_42593650/test/-/blob/master/crazykaka/remote_assets_all_1f7bbf3ab5daf895a8597ca79dccd54d.bundle");

      //  UnityWebRequest www = UnityWebRequest.Get("http://123.207.15.20/");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error+"获取文本");
        }
        else
        {
            // 将结果显示为文本

            Debug.Log(www.downloadHandler.text);

            // 或者以二进制数据格式检索结果
            byte[] results = www.downloadHandler.data;
            Debug.Log(results.Length);
        }
    }


    void Kakatest()
    { 
    
    
    
    
    
    
    }
}
