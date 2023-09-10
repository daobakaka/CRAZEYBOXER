using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Cinemachine;

public class NetMainControl : NetworkBehaviour
{
    public GameObject parent;
    public GameObject[] effects;
    public int num;
    public TMP_InputField ip, prot;
    public GameObject transport, remotebulid, effectstree, freelookcin;
    private Remotebulid remotebulid0;
    void Start()
    {
        // Stratgame();
        remotebulid0 = remotebulid.GetComponent<Remotebulid>();
        Debug.Log(transport.GetComponent<UnityTransport>().ConnectionData.Address + transport.GetComponent<UnityTransport>().ConnectionData.Port + transport.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress);
        if (IsOwner)
        {
            Debug.Log("我是主机");

        }
        else
        { Debug.Log("未选择"); }

        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            // StartClinet();
            //GameObject clone = Instantiate(freelookcin, trankmove.transform.parent.transform);
            //clone.GetComponent<NetworkObject>().Spawn();
           // trankmove.GetComponent<PlayableDirector>().Play();
            Debug.Log("已连接客户端，ID=" + NetworkObjectId);
           // NetworkManager.Singleton.NetworkConfig.PlayerPrefab = NetworkManager.Singleton.GetNetworkPrefabOverride(remotebulid0.Player0["M1(Clone)"]);

        };
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            Debug.Log("已退出客户端，ID=" + id);

        };
        NetworkManager.Singleton.OnServerStarted += () =>
        {
            Debug.Log("服务器准备就绪");
            StratServices();
           StartCoroutine( CleareffectstreeIE());

        };



    }

    // Update is called once per frame
    void Update()
    {

    }
    void StratServices()//初始化服务器东西
    {
        //Instantiate(player1, new Vector3(Random.Range(0, 3f), 1, Random.Range(0, 3f)), Quaternion.identity, null);
        remotebulid.GetComponent<Remotebulid>().ChooseServer();//rebulid脚本热更材料
        //GameObject clone1 = Instantiate(effects[0],new Vector3(553,16,472),Quaternion.identity,null);
        //clone1.GetComponent<NetworkObject>().Spawn();

    }
    void StartClinet()
    {

        remotebulid.GetComponent<Remotebulid>().ChooseClient();

    }

    public void Clicentgame()
    {try
        { NetworkManager.Singleton.Shutdown(); }
        catch(Exception e)
        { Debug.Log(e); }
        NetworkManager.Singleton.StartClient();
        parent.SetActive(false);
        Debug.Log("客户端" + NetworkManager.Singleton.StartClient());
    }
    public void Hostgame()
    {
        NetworkManager.Singleton.StartHost();
        parent.SetActive(false);
        Debug.Log("HOST" + NetworkManager.Singleton.StartHost());

    }
    public void Servicegame()
    {
        NetworkManager.Singleton.StartServer();
        parent.SetActive(false);
        Debug.Log("服务端" + NetworkManager.Singleton.StartServer());
    }
    public void Suntdowngame()
    {
        NetworkManager.Singleton.Shutdown();
        parent.SetActive(false);
        Debug.Log("开始断网");


    }

    public void InsIP()
    {
        //transport.GetComponent<UnityTransport>().ConnectionData.Address = ip.text;
        //transport.GetComponent<UnityTransport>().ConnectionData.Port = Convert.ToUInt16(prot.text);
        //  transport.GetComponent<UnityTransport>().SetConnectionData("0.0.0.0", Convert.ToUInt16(prot.text), ip.text);
        transport.GetComponent<UnityTransport>().SetConnectionData(ip.text, Convert.ToUInt16(prot.text), ip.text);

    }
    IEnumerator CleareffectstreeIE()
    {
        for (; ; )

        {
            yield return new WaitForSeconds(60);
            for (int i = 0; i < effectstree.transform.childCount; i++)
            {
                GameObject clone = effectstree.transform.GetChild(i).gameObject;
                if (!clone.activeSelf)
                {
                    clone.GetComponent<NetworkObject>().Despawn();
                
                }            
            
            
            
            
            
            }

            Debug.Log("缓存已清除,版本号1.1.0");


        }
    
    
    
    }
}

