using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Insplayer : Player
{
    // Start is called before the first frame update
    public GameObject remotebuild;
    public GameObject player;
    private Remotebulid remotebulid0;
    public GameObject startpanle;
   // private Actor actor0;
    public int playerkey;
    public GameObject networkmanager;
    public List<GameObject> playermod;
    private string key;
    public bool Isman;
    public GameObject UI;
    private bool sliderable = true;
    public GameObject kakagam;
    void Start()
    {
        startpanle.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate ()
        {
            Choosestart();
        });
        //��ʼ��Ϸ
        startpanle.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate ()
        {
            Choosesex(true);
        });
        startpanle.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate ()
        {
            Choosesex(false);
        });
        //ѡ���Ա� ��/Ů
        for (int i = 0; i < startpanle.transform.GetChild(2).childCount; i++)
        {
            startpanle.transform.GetChild(2).GetChild(i).GetComponent<Button>().onClick.AddListener(delegate () {
                Choosejob();
            });
        }
      
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)&&IsServer)
        {
            int i = Random.Range(1, 6);
            NetworkManager.Singleton.NetworkConfig.PlayerPrefab = NetworkManager.Singleton.GetNetworkPrefabOverride(remotebulid0.Player0["M"+ i + "(Clone)"]);
            Debug.Log("M" + i);
        }
        startpanle.transform.GetChild(4).GetChild(0).GetComponent<Slider>().value = remotebulid0.pronum;
        if (startpanle.transform.GetChild(4).GetChild(0).GetComponent<Slider>().value >= 5&&sliderable)
        {
            sliderable = false;
            startpanle.transform.GetChild(4).gameObject.SetActive(false);
            startpanle.transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("�˳�����");
        }
    }
    private void Awake()
    {
      //  actor0 = new Actor();
        startpanle.transform.GetChild(4).gameObject.SetActive(true);
        remotebulid0 = remotebuild.GetComponent<Remotebulid>();
    }
    //IEnumerator InsmyselfIE()
    //{
    //    string key = null;
    //    if (Isman)
    //        key = "M" + playerkey;
    //    else
    //        key = "W" + playerkey;
    //    player = Instantiate(remotebulid0.Player0[key], new Vector3(Random.Range(0, 10f), 5, Random.Range(0, 10f)), Quaternion.identity, null);
    //    yield return null;//���ɽ�ɫ��Ϣ
    //    Instantiate(remotebulid0.Add0["CameraLive"], player.transform);
    //    Instantiate(remotebulid0.Add0["Ray"], player.transform);
    //    Instantiate(remotebulid0.Add0["Canvas"], player.transform);
    //    Instantiate(remotebulid0.Add0["Playersound"], player.transform);
    //    //Ѫ��������������ߡ�����
    //    Instantiate(remotebulid0.Add0["Hit"], player.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform);
    //    Instantiate(remotebulid0.Add0["Hit"], player.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).transform);
    //    Instantiate(remotebulid0.Add0["Hit"], player.transform.GetChild(9).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform);
    //    Instantiate(remotebulid0.Add0["Hit"], player.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).transform);
    //    //�ֽ�Hitϵͳ
    //    yield return null;//Ϊ��ɫ������ظ���
    //    NetworkManager network0 = networkmanager.GetComponent<NetworkManager>();
    //    network0.AddNetworkPrefab(player);//����ɫ�ӽ������б�
    //    network0.GetNetworkPrefabOverride(player);
    //    yield return new WaitForSeconds(1);
    //    startpanle.transform.GetChild(3).gameObject.SetActive(false);
    //    //this.gameObject.SetActive(false);


    //}
    public  void Rechoose()
    {

       
        if (kakagam.GetComponent<KAKA>().usenum >0)
        {
            startpanle.transform.parent.transform.GetChild(3).gameObject.SetActive(false);
            UI.transform.GetChild(3).gameObject.SetActive(false);
            startpanle.transform.GetChild(1).gameObject.SetActive(true);
            kakagam.GetComponent<KAKA>().usenum --;
           

        }
       else 
        {

            startpanle.transform.GetChild(5).gameObject.SetActive(true);
            

        }
    }
    void Choosestart()
    {
       // Debug.Log("��ʼ��Ϸ");
        startpanle.transform.GetChild(0).gameObject.SetActive(false);
        startpanle.transform.GetChild(1).gameObject.SetActive(true);


    }
    public void Choosesex(bool sex)
    {
        Isman = sex;
        startpanle.transform.GetChild(1).gameObject.SetActive(false);
        startpanle.transform.GetChild(2).gameObject.SetActive(true);


    }
    void Choosejob()
    {
        playerkey = Random.Range(1, 6);
        startpanle.transform.GetChild(2).gameObject.SetActive(false);
        startpanle.transform.GetChild(3).gameObject.SetActive(true);
        startpanle.transform.parent.transform.GetChild(3).gameObject.SetActive(false);
        //  StartCoroutine(InsmyselfIE());
        // StartCoroutine(BuildplayerIE());

        //NetworkManager network0 = networkmanager.GetComponent<NetworkManager>();
        //network0.NetworkConfig.PlayerPrefab = network0.GetNetworkPrefabOverride(remotebulid0.Player0["M1(Clone)"]);
        //Debug.Log("����д+Getneowrkprefabovveride");
        //startpanle.transform.GetChild(3).gameObject.SetActive(false);
        StartCoroutine(ActorinsIE());
    }
    IEnumerator ActorinsIE()
    {
        
        if (Isman)
            key = "M" + playerkey;
        else
            key = "W" + playerkey;
        //  NetworkManager.Singleton.NetworkConfig.PlayerPrefab= NetworkManager.Singleton.GetNetworkPrefabOverride(remotebulid0.Player0[key+"(Clone)"]);
        //NetworkManager network0 = networkmanager.GetComponent<NetworkManager>();
        //network0.NetworkConfig.PlayerPrefab = network0.GetNetworkPrefabOverride(remotebulid0.Player0[key + "(Clone)"]);
       // ChangeplayerServerRpc();
        NetworkManager.Singleton.NetworkConfig.PlayerPrefab = NetworkManager.Singleton.GetNetworkPrefabOverride(remotebulid0.Player0[key + "(Clone)"]);
        yield return new WaitForSeconds(1);
        startpanle.transform.GetChild(3).gameObject.SetActive(false);
        NetworkManager.Singleton.StartClient();
#if UNITY_STANDALONE_LINUX
{NetworkManager.Singleton.StartServer();}
#else 
        {
            Debug.Log("windows �ͻ���");
            NetworkManager.Singleton.StartClient();
        }
#endif
        { }
    
}
    [ServerRpc(RequireOwnership =false)]
    public void ChangeplayerServerRpc(bool sex)
    {

      //  mysex.Value = sex;
       
    }
    //IEnumerator BuildplayerIE()
    //{
    //    string key = null;
    //    foreach (var item in  remotebulid0.Player0)
    //    {
    //        playermod.Add(item.Value);
        
    //    }

    //    for (int i = 0; i < playermod.Count; i++)
    //    {
    //        player = Instantiate(playermod[i], new Vector3(Random.Range(0, 10f), 5, Random.Range(0, 10f)), Quaternion.identity, null);
    //        yield return null;//���ɽ�ɫ��Ϣ
    //        Instantiate(remotebulid0.Add0["CameraLive"], player.transform);
    //        Instantiate(remotebulid0.Add0["Ray"], player.transform);
    //        Instantiate(remotebulid0.Add0["Canvas"], player.transform);
    //        Instantiate(remotebulid0.Add0["Playersound"], player.transform);
    //        //Ѫ��������������ߡ�����
    //        Instantiate(remotebulid0.Add0["Hit"], player.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform);
    //        Instantiate(remotebulid0.Add0["Hit"], player.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).transform);
    //        Instantiate(remotebulid0.Add0["Hit"], player.transform.GetChild(9).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform);
    //        Instantiate(remotebulid0.Add0["Hit"], player.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).transform);
    //        //�ֽ�Hitϵͳ
    //        yield return null;//Ϊ��ɫ������ظ���
    //        //NetworkManager network0 = networkmanager.GetComponent<NetworkManager>();
    //        //network0.AddNetworkPrefab(player);//����ɫ�ӽ������б�
    //        //network0.GetNetworkPrefabOverride(player);
    //        //yield return new WaitForSeconds(1);
    //        //startpanle.transform.GetChild(3).gameObject.SetActive(false);

    //    }

    //}

    IEnumerator AutochooseIE()
    {

        WaitForSeconds loop = new WaitForSeconds(10);
        for (; ; )
        {
            yield return loop;  
            int i = Random.Range(1, 6);
            
            Debug.Log("��ʼԤ�ƽ�ɫѭ��");

            if (Random.Range(0,1f)>0.5f)
                NetworkManager.Singleton.NetworkConfig.PlayerPrefab = NetworkManager.Singleton.GetNetworkPrefabOverride(remotebulid0.Player0["M" + i + "(Clone)"]);
            else
               NetworkManager.Singleton.NetworkConfig.PlayerPrefab = NetworkManager.Singleton.GetNetworkPrefabOverride(remotebulid0.Player0["W" + i + "(Clone)"]);



        }

    }
    public void GoAutochooseIE()
    {
        {
            StartCoroutine(AutochooseIE());
        
        
        }
    }

}
