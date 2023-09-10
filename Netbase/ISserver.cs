using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class ISserver : NetworkBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(Testcon());
    }

    IEnumerator Testcon()
    {
        //UnityWebRequest webRequest = UnityWebRequest.Get("http://123.207.15.20:7777");
        //yield return webRequest.SendWebRequest();

        //if (webRequest.result != UnityWebRequest.Result.Success)

        //   Debug.Log(webRequest.error);
        //else
        //    Debug.Log("端口连接成功" + webRequest.downloadHandler.text);
        yield return new WaitForSeconds(3);

#if UNITY_STANDALONE_LINUX
{NetworkManager.Singleton.StartServer();}
#else 
        {
            Debug.Log("windows 客户端");
            //NetworkManager.Singleton.StartServer();
        }
#endif



    }
}
