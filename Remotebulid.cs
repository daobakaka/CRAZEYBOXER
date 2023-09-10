using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Networking;
using System;
using Unity.Netcode;
using Unity.Collections;

public class Remotebulid : NetworkBehaviour
{
    public List<GameObject> prefeb0;
    public Dictionary<string, AudioClip> Music0 = new Dictionary<string, AudioClip>();//音乐片段
    public AudioClip[] Musicarray;
    public Dictionary<string, GameObject> NPC0 = new Dictionary<string, GameObject>();//NPC 字典
    public GameObject[] NPCarray;
    public Dictionary<string, GameObject> Monster0 = new Dictionary<string, GameObject>();//怪物字典
    public GameObject[] Monsterarray;
    public Dictionary<string, GameObject> Effects0 = new Dictionary<string, GameObject>();//粒子效果
    public GameObject[] Effectsarray;
    public Dictionary<string, GameObject> Player0 = new Dictionary<string, GameObject>();//角色
    public GameObject[] Playerarray;
    //---以下代码增加初始化速度--简洁
    public List<GameObject> insgameObjects = new List<GameObject>();
    public List<NetworkObject> networklist = new List<NetworkObject>();
    //字典初始化
    // private int x;
    public Transform clonetree, effectstree;
    public GameObject gamemanager;
    public GameObject insplayer;
    //
    public int pronum;
    private bool pronum_assist = true;

    private void Awake()
    {
        StartCoroutine(Addresoures());
    }
    void Update()
    {
        if (pronum >= 5 && pronum_assist)
        {
            pronum_assist = false;
            StartCoroutine(AddnetprefabIE());


        }
    }

    IEnumerator Addresoures()
    {
        yield return null;
        for (int i = 0; i < Musicarray.Length; i++)
        {
            Music0.Add(Musicarray[i].name, Musicarray[i]);
            if (i == Musicarray.Length - 1)
            {

                pronum++;
                break;

            }
        }
        for (int i = 0; i < NPCarray.Length; i++)
        {
            NPC0.Add(NPCarray[i].name, NPCarray[i]);
            if (i == NPCarray.Length - 1)
            {

                pronum++;
                break;

            }
        }
        for (int i = 0; i < Monsterarray.Length; i++)
        {
            Monster0.Add(Monsterarray[i].name, Monsterarray[i]);
            if (i == Monsterarray.Length - 1)
            {

                pronum++;
                break;

            }
        }
        for (int i = 0; i < Playerarray.Length; i++)
        {
            Player0.Add(Playerarray[i].name, Playerarray[i]);
            if (i == Playerarray.Length - 1)
            {

                pronum++;
                break;

            }
        }
        for (int i = 0; i < Effectsarray.Length; i++)
        {
            Effects0.Add(Effectsarray[i].name, Effectsarray[i]);
            if (i == Effectsarray.Length - 1)
            {

                pronum++;
                break;

            }
        }

    }
    IEnumerator  AddnetprefabIE()
    {

        //NetworkManager networkManager0 = gamemanager.GetComponent<NetworkManager>();

        //networkManager0.AddNetworkPrefab(NPC0["Dimples"]);
        ////
        //networkManager0.AddNetworkPrefab(Effects0["frameBall"]);
        ////
        //networkManager0.AddNetworkPrefab(Effects0["Skillball"]);
        ////
        //networkManager0.AddNetworkPrefab(Monster0["Skeleton@Attack"]);
        ////
        ////
        //foreach (var item in Player0)
        //{

        //    networkManager0.AddNetworkPrefab(item.Value);
        //}

        Debug.Log("加载完毕" + pronum);
        yield return new WaitForSeconds(3);
#if UNITY_STANDALONE_LINUX
{NetworkManager.Singleton.StartServer();
 insplayer.GetComponent<Insplayer>().GoAutochooseIE();
}
#else
        {
            Debug.Log("不是服务器");
        }
#endif

        { }



    }
    IEnumerator InsServer()//服务器初始化加载资源

    {


        yield return new WaitForSeconds(0.5f);
        {


            insgameObjects.Add(Instantiate(NPC0["Dimples"], new Vector3(UnityEngine.Random.Range(0, 0), 1.11f, UnityEngine.Random.Range(0, 0)), Quaternion.Euler(0, 0, 0), clonetree));
            insgameObjects.Add(Instantiate(Monster0["Skeleton@Attack"], new Vector3(UnityEngine.Random.Range(20, 30), UnityEngine.Random.Range(-5, -3f), UnityEngine.Random.Range(0, 5)), Quaternion.Euler(0, 0, 0), clonetree));

            for (int i = 0; i < insgameObjects.Count; i++)
            {

                networklist.Add(insgameObjects[i].GetComponent<NetworkObject>());
                networklist[i].Spawn();//双端配置
                networklist[i].TrySetParent(clonetree);

            }
        }

    }

    public void Insnetobj(Dictionary<string, GameObject> dic, FixedString128Bytes name, FixedString128Bytes self, Vector3 pos, Quaternion ro)
    {

        Debug.Log(dic[name.ToString()].name);
        GameObject clone = Instantiate(dic[name.ToString()], pos, ro);
        clone.GetComponent<NetworkObject>().Spawn();
        clone.GetComponent<NetworkObject>().TrySetParent(effectstree);
        clone.name = self.ToString();

    }
    public void ChooseServer()
    {
        StartCoroutine(InsServer());
    }
    public void ChooseClient()
    {
    }



    //IEnumerator Addmusicclip()
    //{
    //    yield return new WaitForSeconds(1);
    //    AssetReference1.LoadAssetAsync<AudioClip>().Completed += mus =>
    //    {
    //        this.GetComponent<AudioSource>().clip = Instantiate(mus.Result);

    //        this.GetComponent<AudioSource>().Play();

    //        foreach (KeyValuePair<string, AudioClip> item in musics)
    //        {
    //            // Debug.Log(item.Key + ".... " + item.Value.loadState);
    //        }

    //    };


    //}

    IEnumerator GetText()
    {
        Debug.Log("开始下载资源包");
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle("https://gitcode.net/weixin_42593650/test/-/blob/master/remote/pic/must_assets_all_734248df46e43d3ed8c8d0966db72fa5.bundle"))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);

            }
        }
    }


}

