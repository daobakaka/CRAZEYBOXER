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

public class Build : NetworkBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> prefeb0;
    //public GameObject reefebreal;
    //public List<string> keys;
    //
    public Dictionary<string, GameObject> effcts = new Dictionary<string, GameObject>();//技能特效
    public Dictionary<string, AudioClip> Music0 = new Dictionary<string, AudioClip>();//音乐片段
    public Dictionary<string, GameObject> NPC0 = new Dictionary<string, GameObject>();//NPC 字典
    public Dictionary<string, UnityEngine.Object> Must0 = new Dictionary<string, UnityEngine.Object>();//必要组件 uild
    public Dictionary<string, GameObject> Monster0 = new Dictionary<string, GameObject>();//怪物字典
    public Dictionary<string, GameObject> Effects0 = new Dictionary<string, GameObject>();//粒子效果
    public Dictionary<string, GameObject> Add0 = new Dictionary<string, GameObject>();//角色生成附加物品
    public Dictionary<string, GameObject> Player0 = new Dictionary<string, GameObject>();//角色
    public List<Material> Materials0 = new List<Material>();
    //---以下代码增加初始化速度--简洁
    public List<GameObject> insgameObjects = new List<GameObject>();
    public List<NetworkObject> networklist = new List<NetworkObject>();
    //字典初始化
    // private int x;
    public Transform clonetree, effectstree;
    public AssetLabelReference AssetLabel, AssetLabel2, AssetLabel3, AssetLabel4, AssetLabel5, AssetLabel6, AssetLabel7, Assetlocal0, AssetShader, AssetMaterial, AssrtAnimator;
    // public AssetReferenceT<AudioClip> AssetReference1;
    public GameObject gamemanager;
    //
    private AsyncOperationHandle<IList<GameObject>> loadHandle0, loadHandle1, loadHandle2, loadHandle3, loadHandle5, loadHandle6;
    private AsyncOperationHandle<IList<UnityEngine.Object>> loadT;
    private AsyncOperationHandle<IList<AudioClip>> loadHandle4;
    private AsyncOperationHandle<IList<Shader>> loadShader;
    private AsyncOperationHandle<IList<AnimationClip>> loadAnimator;
    private AsyncOperationHandle<IList<AnimatorControllerParameter>> loadAnicon;
    private AsyncOperationHandle<IList<Material>> loadMaterial;
    public int pronum;
    private bool pronum_assist = true;

    private void Awake()
    {
        StartCoroutine(Addresoures());
        // StartCoroutine(AddresoureslocalIE());
    }
    //void Start()
    //{
    //    // StartCoroutine("Addre");
    //    StartCoroutine("InsServer");
    //    StartCoroutine("InsClient");
    //    //StartCoroutine(GetText());
    //  //  StartCoroutine("IniMonster");

    //    // StartCoroutine("Addmusicclip");
    //    // StartCoroutine("Testkk");
    //}

    // Update is called once per frame
    void Update()
    {
        if (pronum >= 8 && pronum_assist)
        {
            pronum_assist = false;
            Addnetprefab();

        }
    }
    IEnumerator AddresoureslocalIE()
    {

        loadShader = Addressables.LoadAssetsAsync<Shader>(Assetlocal0, (handle0) =>
        {

            Debug.Log("shader加载");


        });
        yield return null;

        loadShader.Completed += obj => {

            pronum++;
            Debug.Log("Shader" + "加载完毕");

        };//这是一个方法，用一个变量来存储方法



    }//本地资源加载
    IEnumerator Addresoures()
    {
        loadHandle0 = Addressables.LoadAssetsAsync<GameObject>(AssetLabel, (handle0) =>
        {


            NPC0.Add(handle0.name, handle0);
            Debug.Log("开始AA包加载");


        });
        yield return null;

        loadHandle0.Completed += obj => {


            pronum++;
            Debug.Log("NPC" + "加载完毕");


        };//这是一个方法，用一个变量来存储方法
        loadT = Addressables.LoadAssetsAsync<UnityEngine.Object>(AssetLabel2, (handle1) =>
        {


            Must0.Add(handle1.name, handle1);


        });
        loadT.Completed += obj => {


            pronum++;
            Debug.Log("MUST" + "加载完毕");


        };
        loadHandle2 = Addressables.LoadAssetsAsync<GameObject>(AssetLabel3, (handle2) =>
        {


            Monster0.Add(handle2.name, handle2);



        });
        yield return null;
        loadHandle2.Completed += obj => {


            pronum++;

            Debug.Log("怪物" + "加载完毕");



        };
        loadHandle3 = Addressables.LoadAssetsAsync<GameObject>(AssetLabel4, (handle3) =>
        {


            Effects0.Add(handle3.name, handle3);


        });
        loadHandle3.Completed += obj => {


            pronum++;
            Debug.Log("特效" + "加载完毕");

        };
        loadHandle4 = Addressables.LoadAssetsAsync<AudioClip>(AssetLabel5, (handle4) =>
        {


            Music0.Add(handle4.name, handle4);




        });
        loadHandle4.Completed += obj => {


            pronum++;
            Debug.Log("音乐" + "加载完毕");




        };
        loadHandle5 = Addressables.LoadAssetsAsync<GameObject>(AssetLabel6, (handle5) =>
        {


            Add0.Add(handle5.name, handle5);





        });
        loadHandle5.Completed += obj => {


            pronum++;
            Debug.Log("附加资源" + "加载完毕");



        };
        loadHandle6 = Addressables.LoadAssetsAsync<GameObject>(AssetLabel7, (handle6) =>
        {

            Player0.Add(handle6.name, handle6);


        });
        loadHandle6.Completed += obj => {


            pronum++;
            Debug.Log("角色" + "加载完毕");



        };
        ///

        loadMaterial = Addressables.LoadAssetsAsync<Material>(AssetMaterial, (handle6) =>
        {

            Materials0.Add(handle6);
            Debug.Log("材质名字" + handle6.name);

        });
        loadMaterial.Completed += obj => {


            pronum++;
            Debug.Log("材质" + "加载完毕");



        };


        yield return loadHandle2;
        //  Instantiate(loadHandle2.Result[0], new Vector3(0, 5, 5), Quaternion.identity);

        // NetworkManager networkManager0 = gamemanager.GetComponent<NetworkManager>();
        // networkManager0.AddNetworkPrefab(Must0["Sphere"]);
        // networkManager0.AddNetworkPrefab(Must0["Cube"]);
        // //
        //// networkManager0.AddNetworkPrefab(NPC0["64e6b6c4597896294484e448 Variant"]);
        // //
        // networkManager0.AddNetworkPrefab(NPC0["Dimples"]);
        // //
        // networkManager0.AddNetworkPrefab(Effects0["frameBall"]);
        // //
        // networkManager0.AddNetworkPrefab(Effects0["Skillball"]);
        // //
        // networkManager0.AddNetworkPrefab(Monster0["Skeleton@Attack"]);
        // //
        // foreach (var item in Player0)
        // {

        //     networkManager0.AddNetworkPrefab(item.Value);
        // }

    }
    private void Addnetprefab()
    {

        NetworkManager networkManager0 = gamemanager.GetComponent<NetworkManager>();
        // networkManager0.AddNetworkPrefab(NPC0["64e6b6c4597896294484e448 Variant"]);
        //
        networkManager0.AddNetworkPrefab(NPC0["Dimples"]);
        //
        networkManager0.AddNetworkPrefab(Effects0["frameBall"]);
        //
        networkManager0.AddNetworkPrefab(Effects0["Skillball"]);
        //
        networkManager0.AddNetworkPrefab(Monster0["Skeleton@Attack"]);
        //
        networkManager0.AddNetworkPrefab((GameObject)Must0["Sphere"]);
        //
        foreach (var item in Player0)
        {

            networkManager0.AddNetworkPrefab(item.Value);
        }

        Debug.Log("加载完毕" + pronum);

    }
    IEnumerator InsServer()//服务器初始化加载资源

    {
        // GameObject landscape=  insgameObjects.Add(Instantiate(Must0["Landscape"], new Vector3(0,0,0), Quaternion.Euler(0, 0, 0), clonetree));//加载场景


        yield return new WaitForSeconds(0.5f);
        {

            //insgameObjects.Add(Instantiate(Must0["Landscape"], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), clonetree));//场景也用于热更
            // insgameObjects.Add(Instantiate(NPC0["64e6b6c4597896294484e448 Variant"], new Vector3(UnityEngine.Random.Range(0,3), UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 3)), Quaternion.Euler(0, 0, 0), clonetree));
            insgameObjects.Add(Instantiate(NPC0["Dimples"], new Vector3(UnityEngine.Random.Range(0, 0), 1.11f, UnityEngine.Random.Range(0, 0)), Quaternion.Euler(0, 0, 0), clonetree));
            insgameObjects.Add(Instantiate(Monster0["Skeleton@Attack"], new Vector3(UnityEngine.Random.Range(20, 30), UnityEngine.Random.Range(-5, -3f), UnityEngine.Random.Range(0, 5)), Quaternion.Euler(0, 0, 0), clonetree));
            insgameObjects.Add((GameObject)Instantiate(Must0["Sphere"], new Vector3(UnityEngine.Random.Range(0, 5), 1.11f, UnityEngine.Random.Range(0, 2)), Quaternion.Euler(0, 0, 0), clonetree));
            for (int i = 0; i < insgameObjects.Count; i++)
            {

                networklist.Add(insgameObjects[i].GetComponent<NetworkObject>());
                networklist[i].Spawn();//双端配置
                networklist[i].TrySetParent(clonetree);

            }
        }
        // insgameObjects.Add(Instantiate(Must0["64dbaf84c603b299c0fe54a6"], new Vector3(UnityEngine.Random.Range(0, 5), 1.11f, UnityEngine.Random.Range(0, 2)), Quaternion.Euler(0, 0, 0), clonetree));
    }

    public void Insnetobj(Dictionary<string, GameObject> dic, FixedString128Bytes name, FixedString128Bytes self, Vector3 pos, Quaternion ro)
    {

        Debug.Log(dic[name.ToString()].name);
        GameObject clone = Instantiate(dic[name.ToString()], pos, ro);
        clone.GetComponent<NetworkObject>().Spawn();
        clone.GetComponent<NetworkObject>().TrySetParent(effectstree);
        clone.name = self.ToString();

    }
    //public void Insnetobj(Dictionary<string, GameObject> dic,  Vector3 pos)
    //{

    //    Debug.Log(dic[name.ToString()].name);
    //    GameObject clone = Instantiate(dic["frameBall"], pos, Quaternion.identity, effectstree);
    //    clone.GetComponent<NetworkObject>().Spawn();

    //}
    public void ChooseServer()
    {
        StartCoroutine(InsServer());
    }
    IEnumerator InsClient()

    {
        yield return new WaitForSeconds(5);

        {

            Instantiate(Must0["3rdPersonController"], new Vector3(552 + UnityEngine.Random.Range(0, 7), 30, 472 + UnityEngine.Random.Range(0, 7)), Quaternion.Euler(0, 0, 0), clonetree);
        }


    }
    public void ChooseClient()
    {
        StartCoroutine(InsClient());
    }
    IEnumerator IniMonster()

    {
        Addressables.LoadAssetsAsync<GameObject>(AssetLabel3, (handle1) =>
        {


            Monster0.Add(handle1.name, handle1);


        });
        yield return new WaitForSeconds(3);

        {
            Instantiate(Monster0["Skeleton @Attack Variant"], new Vector3(300, 20, 550), Quaternion.Euler(0, 0, 0), clonetree);

        }

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

