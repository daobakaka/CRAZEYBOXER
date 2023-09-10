using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode.Components;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using Unity.Collections;
using Unity.BossRoom.Infrastructure;

public class Actormove : Player
{
    Rigidbody rb;
    float s, h, y0, x0, tc;
    [SerializeField]
    float speedpa;
    private TMP_Text TextMesh0;
    private NetworkVariable<Vector3> networkVariablepo = new NetworkVariable<Vector3>(Vector3.zero);
    private NetworkVariable<Quaternion> networkVariableQe = new NetworkVariable<Quaternion>(Quaternion.identity);
    private NetworkVariable<int> clientID = new NetworkVariable<int>();
    private NetworkVariable<float> anmarorspeed = new NetworkVariable<float>();
    private NetworkVariable<float> animatorrunspeed = new NetworkVariable<float>();
    private NetworkVariable<int> rollnum = new NetworkVariable<int>();
    private NetworkVariable<bool> Isgroundnet = new NetworkVariable<bool>();
    private NetworkVariable<bool> Isswimnet = new NetworkVariable<bool>();
    private NetworkVariable<bool> Isjumpablenet = new NetworkVariable<bool>();
    private NetworkVariable<float> timenet = new NetworkVariable<float>();
    private NetworkVariable<int> standposnet = new NetworkVariable<int>();
    //����������ذ��
    private NetworkVariable<float> nethealth = new NetworkVariable<float>();
    private NetworkVariable<float> netstamina = new NetworkVariable<float>();
    //������������ذ��
    private NetworkVariable<Vector3> netexppostion = new NetworkVariable<Vector3>();
    private NetworkVariable<FixedString128Bytes> netobjname = new NetworkVariable<FixedString128Bytes>();
    private NetworkVariable<FixedString128Bytes> selfname = new NetworkVariable<FixedString128Bytes>();
    private NetworkManager netroot;
    //
    //private NetworkVariable<NetworkObject> networkVariableobj = new NetworkVariable<NetworkObject>();
    private NetworkVariable<bool> netobjbool = new NetworkVariable<bool>();
    private GameObject objactive;
    //����ɼ����
    private NetworkVariable<Vector3> skillpos = new NetworkVariable<Vector3>();
    private NetworkVariable<int> skillnum = new NetworkVariable<int>();
    private NetworkVariable<bool> skillbool = new NetworkVariable<bool>();
    private GameObject[] clonearry = new GameObject[10];
    private Netpool netpool0;
    //
    private Animator animator0;
    private NetworkAnimator networkAnimator0;
    //
    public GameObject Aimview;

    public CinemachineVirtualCamera virtualCamera;
    public CinemachineFreeLook CinemachineFree;
    public GameObject CarmeraLive0;
    private float Vs, Hs;
    //
    private bool jumpble = true;
    public float jumpsize = 5;
    //
    private bool attackble = true;
    private float size;
    private bool ifstadnpos;
    private bool weapon = true;
    //
    private GameObject poller;
    private GameObject netpoolerclone;
    private GameObject netpoolerrpc;
    //
    private Volume postprocessing;
    //
    private NetworkVariable<int> animationnum = new NetworkVariable<int>();
    //
    public float netID;

    //
    public Joystick joystick1, joystick2;
    private float viewx, viewy;
    //
    public GameObject buttonpanel;
    //
    public Actor actor0;//��ɫ����״̬����
    private Slider sliderhealth, sliderbackground, sliderstamina, staminabackground;
    private bool alive = true;
    private bool spritable = true;
    //
    private GameObject deathcamera;
    //
    private Remotebulid remotebulid0;
    //
    public List<AudioClip> musics;
    private AudioSource soundplayer, soundplayerstep, soundplayerback, soundpain;
    private Insplayer insnetplayer0;
    public bool mysex = true;
    void Start()
    {
        netID = NetworkObjectId;
        Hitsports(0, "false");
        ChangeHitname();
        if (IsOwner&&IsClient)
        {
            this.transform.position = new Vector3(0 + UnityEngine.Random.Range(0, 7), 8, 0 + UnityEngine.Random.Range(0, 7));
            CarmeraLive0 = GameObject.FindWithTag("CarmeraLive");
            transform.GetChild(10).GetChild(0).gameObject.SetActive(true);
            CinemachineFree = transform.GetChild(10).GetChild(0).GetComponent<CinemachineFreeLook>();
            CinemachineFree.Follow = this.transform;
            CinemachineFree.LookAt = this.transform;
            Debug.Log("ִ�д˶�");
        }
        postprocessing = GameObject.FindWithTag("Volume").GetComponent<Volume>();
        Debug.Log("�¿ͻ��˽���--" + "������-" + IsServer + "����-" + IsHost + "�ͻ���-" + IsClient + "�������-" + IsLocalPlayer + "ӵ����-" + IsOwner + "������ӵ����-" + IsOwnedByServer);
        //>>>
        poller = GameObject.FindWithTag("Poller");//�ҵ���������崢����
        for (int j = 0; j < poller.GetComponent<Gamemanagertest>().objnum.Length; j++)
        {
            clonearry[j] = poller.GetComponent<Gamemanagertest>().objnum[j];
        }

            ///---
        rb = GetComponent<Rigidbody>();
        animator0 = GetComponent<Animator>();
        networkAnimator0 = GetComponent<NetworkAnimator>();
        animator0.SetBool("Weapon", false);//--���ô���
        ///
        ///
        remotebulid0 = GameObject.FindWithTag("Remotebuild").GetComponent<Remotebulid>();
        foreach (var item in remotebulid0.Music0)
        {

            musics.Add(item.Value);
        
        }
        ///
        soundplayerback = GameObject.FindWithTag("Soundplayer").transform.GetChild(1).GetComponent<AudioSource>();
        if (Random.Range(0, 1f) > 0.5f)
        { soundplayerback.clip = remotebulid0.Music0["back1"]; }
        else
        { soundplayerback.clip = remotebulid0.Music0["back2"]; }
        soundplayerback.Play();
        soundplayer = transform.GetChild(13).GetChild(1).GetComponent<AudioSource>();
        soundplayerstep = transform.GetChild(13).GetChild(0).GetComponent<AudioSource>();
        soundplayerstep.clip = remotebulid0.Music0["step"];
        soundpain= transform.GetChild(13).GetChild(2).GetComponent<AudioSource>();
        //����ģ��
        StartCoroutine(Actorattackpos());
    }

    // Update is called once per frame
    public override void OnNetworkSpawn()//��start ֮ǰִ��,�ҵ�joy���ݸ�,�ҵ�UI ����
    {
        //if (IsServer)
        //{
        //    clientID.Value = (int)OwnerClientId;
        //}
        //GameObject kaka = GameObject.FindWithTag("UI");
        //joystick1 = kaka.transform.GetChild(0).GetComponent<Joystick>();
        //joystick2 = kaka.transform.GetChild(1).GetComponent<Joystick>();
        //joystick1.DeadZone = 0.2f;
        //Debug.Log(kaka.transform.GetChild(2).GetChild(0).GetComponent<Button>().name);
        //buttonpanel = kaka.transform.GetChild(2).GetChild(4).gameObject;//���漼������ַ
        //deathcamera = GameObject.FindWithTag("MainCamera");

        //NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        //{
        //    kaka.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate ()
        //    {
        //        Playerjump();

        //    });
        //    kaka.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate ()
        //    {
        //        Playerattack();

        //    });
        //    kaka.transform.GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate ()
        //    {
        //        Playerroll();

        //    });
        //    kaka.transform.GetChild(2).GetChild(3).GetComponent<Button>().onClick.AddListener(delegate ()
        //    {
        //        Playerskill(0);

        //    });
        //    kaka.transform.GetChild(2).GetChild(4).GetComponent<Button>().onClick.AddListener(delegate ()
        //    {
        //        Playersprit();

        //    });



        //    for (int i = 0; i < kaka.transform.GetChild(2).childCount; i++)
        //    {
        //        kaka.transform.GetChild(2).GetChild(i).gameObject.SetActive(true);

        //    }

        //};
        /////--
        //Inisplayer();
        //StartCoroutine("PlayerrecoverIE");
    }
    private void Awake()
    {
        if (IsServer)
        {
            clientID.Value = (int)OwnerClientId;
        }
        GameObject kaka = GameObject.FindWithTag("UI");
        joystick1 = kaka.transform.GetChild(0).GetComponent<Joystick>();
        joystick2 = kaka.transform.GetChild(1).GetComponent<Joystick>();
        joystick1.DeadZone = 0.2f;
        Debug.Log(kaka.transform.GetChild(2).GetChild(0).GetComponent<Button>().name);
        buttonpanel = kaka.transform.GetChild(2).GetChild(4).gameObject;//���漼������ַ
        deathcamera = GameObject.FindWithTag("MainCamera");
        //
        //
        insnetplayer0 = GameObject.FindWithTag("Insplayer").GetComponent<Insplayer>();

        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            kaka.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate ()
            {
                Playerjump();

            });
            kaka.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate ()
            {
                Playerattack();

            });
            kaka.transform.GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate ()
            {
                Playerroll();

            });
            kaka.transform.GetChild(2).GetChild(3).GetComponent<Button>().onClick.AddListener(delegate ()
            {
                Playerskill(0);

            });
            kaka.transform.GetChild(2).GetChild(4).GetComponent<Button>().onClick.AddListener(delegate ()
            {
                Playersprit();

            });



            for (int i = 0; i < kaka.transform.GetChild(2).childCount; i++)
            {
                kaka.transform.GetChild(2).GetChild(i).gameObject.SetActive(true);

            }

        };
        ///--
        ///
        Inisplayer();
        StartCoroutine("PlayerrecoverIE");
    }

    void Inisplayer()//��ɫ��ʼ���Զ���,�Ӷ���new��Ѫ��������������
    {
        actor0 = new Actor();
        sliderhealth = transform.GetChild(12).GetChild(0).GetComponent<Slider>();
        sliderbackground = transform.GetChild(12).GetChild(1).GetComponent<Slider>();//��ȡ��ɫ����ֵ������
        sliderstamina = transform.GetChild(12).GetChild(2).GetComponent<Slider>();//��ȡ��ɫ����������
        staminabackground = transform.GetChild(12).GetChild(3).GetComponent<Slider>();
        actor0.Insme();
        speedpa = actor0.speed;
        actor0.Isman = mysex;
        netpool0 = new Netpool();
        //����
  


    }
    IEnumerator Healthaddreduce()//Ѫ������Я��
    {

        WaitForSeconds loop = new WaitForSeconds(Time.deltaTime);
        for (; sliderhealth.value > actor0.health;)
        {
            yield return loop;

            sliderhealth.value -= Time.deltaTime * 30;
        }
        yield return new WaitForSeconds(0.3f);
        for (; sliderbackground.value > actor0.health;)
        {
            yield return loop;

            sliderbackground.value -= Time.deltaTime * 30;
        }

    }

    IEnumerator Staminareduce()//��������Я��
    {
        WaitForSeconds loop = new WaitForSeconds(Time.deltaTime);
        for (; sliderstamina.value > actor0.stamina;)
        {
            yield return loop;

            sliderstamina.value -= Time.deltaTime * 50;
        }
        yield return new WaitForSeconds(0.3f);
        for (; staminabackground.value > actor0.stamina;)
        {
            yield return loop;

            staminabackground.value -= Time.deltaTime * 50;
        }
    }
    void Healthmonitor()//����������
    {
        if (actor0.health <= 0 && alive)
        {
            //NetworkManager.Singleton.Shutdown();
            alive = false;
            this.gameObject.tag = "Untagged";
            Debug.Log("����ս��");
            sliderhealth.transform.parent.gameObject.SetActive(false);//�ر�Ѫ��
            networkAnimator0.SetTrigger("Death");
            attackble = false;
           // transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
            StartCoroutine(HealthmonitorIE());
            for (int i = 0; i < buttonpanel.transform.parent.childCount; i++)//�رհ���
            {
                buttonpanel.transform.parent.GetChild(i).gameObject.SetActive(false);

            }
           
        }
    }
    [ServerRpc]
    public void BroadcaststateServerRpc(float health,float stamina)
    {
        nethealth.Value = health;
        netstamina.Value = stamina;
    
    }//Ѫ��ӳ��
    [ServerRpc]
    public void BroadcastexpServerRpc(Vector3 po, FixedString128Bytes name, FixedString128Bytes self)
    {
        netexppostion.Value = po;
        netobjname.Value = name;
        selfname.Value = self;
        remotebulid0.Insnetobj(remotebulid0.Effects0, netobjname.Value, selfname.Value,netexppostion.Value,this.transform.rotation);//����rebuild ���ں��� ��¡ �㲥���
        
    }
    [ClientRpc]//�������߼�
    public void BroadcastexpClientRpc(Vector3 po, FixedString128Bytes name)
    {
        
        Debug.Log("���������ڴ�����Ϣ"+po+name+"--����");
        
        
    }
    [ClientRpc]
    public void BroadcaststateClientRpc(float health,float stamina)
    { 
      
    
    
    
    }
    IEnumerator HealthmonitorIE()
    {
        yield return new WaitForSeconds(1);
        BroadcastexpServerRpc(transform.position, "frameBall", this.name);//�������߼�����ֱ��д�߼�����
        transform.GetComponent<Rigidbody>().useGravity = false;
        transform.GetComponent<CapsuleCollider>().isTrigger = true;
        transform.GetChild(10).GetChild(0).gameObject.SetActive(false);//�ر����
        yield return new WaitForSeconds(2);
        WaitForSeconds loop = new WaitForSeconds(Time.deltaTime);
        for (; transform.position.y > -1;)
        {
            yield return loop;
            transform.position += new Vector3(0, -1 * Time.deltaTime, 0);
        }
        buttonpanel.transform.parent.parent.GetChild(3).gameObject.SetActive(true);//������ѡ���
        BroadcastexpServerRpc(transform.position, "frameBall", this.name);//�������߼�����ֱ��д�߼�����
        NetworkManager.Singleton.Shutdown();
       
        

    }
    void Spiritmonitor()//��ҳ�̶������
    {
        if (joystick1.Direction.normalized.magnitude > 0.95f && spritable == false)
        {
            animator0.SetFloat("Isrun", 1);

        }
        else
        {
            animator0.SetFloat("Isrun", 0);

        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (IsClient && IsOwner)
                BroadcastexpServerRpc(transform.position, "Cube", this.name);
               // BroadcastexpServerRpc(transform.position, "frameBall", this.name);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            
       
            if (IsClient && IsOwner)
                Useskill(0,true);
        }
    }
    private void FixedUpdate()
    {
#if UNITY_EDITOR
        {
            // Monvsp();


        }
#elif PLATFORM_STANDALONE_WIN
{  Monvsp();
}
#else
{

 Monvspmoblie();
}
#endif
        Monvspmoblie();
        Rayfalldown();
        Healthmonitor();
        Spiritmonitor();
       // Movenew();
    }
    void ChangeHitname()
    {
        this.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(5).gameObject.name = netID.ToString();
        this.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5).gameObject.name = netID.ToString();
        this.transform.GetChild(9).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.name = netID.ToString();
        this.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.name = netID.ToString();
        this.transform.gameObject.name = "Player" + netID.ToString();
    }//�����λ�����֣�ȷ�������ĸ���ҵĹ���

    public void Hitsports(int pos)//�˶�������������
    {
        switch (pos)
        {
            case 0://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(true);
                this.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(true);
                this.transform.GetChild(9).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                this.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                break;
            case 1:
                this.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                break;
            case 2://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                break;
            case 3://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                break;
            case 4://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                break;
            case 5://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                break;
            case 6://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(true);
                break;
            case 7://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(true);
                break;
        }

        //SetNetworkobjactiveServerRpc(pos);
        //SetNetworkobjactiveClientRpc(animationnum.Value);
    }
    [ServerRpc]
    public void SetNetworkobjactiveServerRpc(int act)//״̬������������
    {
        animationnum.Value = act;

        //Debug.Log("�������ѽ��չ���״̬");

    }
    [ClientRpc]
    public void SetNetworkobjactiveClientRpc(int par)//������֪ͨ�ͻ��ˣ������������������������������������൱�ڹ㲥,����Ч����������
    {
        if (IsClient && IsOwner)
        {
            Hitsports(par);
        }

    }
    public void Hitsports(int pos, string boolstring)//���ö�������
    {
        bool bool0 = System.Convert.ToBoolean(boolstring);
        switch (pos)
        {
            case 0://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(bool0);
                this.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(bool0);
                this.transform.GetChild(9).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(bool0);
                this.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(bool0);
                break;
            case 1:
                this.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(bool0);
                break;
            case 2://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(bool0);
                break;
            case 3://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(bool0);
                break;
            case 4://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(bool0);
                break;
            case 5://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(bool0);
                break;
            case 6://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(bool0);
                break;
            case 7://���֡����֡���š��ҽ�
                this.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(bool0);
                break;
        }
    }
    public void Hitsound(int i)
    {
        switch (i)
        { case 1:
            soundplayer.clip = remotebulid0.Music0["hit1"];
                break;
            case 2:
            soundplayer.clip = remotebulid0.Music0["hit2"];
                break;
        }
        soundplayer.Play();





    }//��ɫ��������
    IEnumerator Actorjump()
    {
        yield return new WaitForSeconds(1.6f);
        jumpble = true;
        if (IsOwner && IsClient)
        {
            animator0.SetBool("Jumpable", jumpble);

        }
    }

    IEnumerator Actorattackpos()//��ɫȭ��posture ѭ��,��ɫվ��ѭ��������
    {
        WaitForSeconds loop = new WaitForSeconds(0.5f);
        for (; ; )
        {
            if (IsOwner && IsClient)
            {
                animator0.SetFloat("Teakwondo", Random.Range(0, 1.2f));
                animator0.SetInteger("Stadnpose", Random.Range(0, 15));//���Ӱ�����˶���
            }
            yield return loop;
        }
    }
    public void Attackableevent()
    {
        attackble = true;
        Hitsports(0, "false");

    }
    /// <summary>
    /// ��ɫ����
    /// </summary>
    public void Playerjump()
    {
        if (IsOwner && IsClient)
        {
            if (jumpble && attackble && actor0.stamina > 5)//��Ծ�޶�,��ˮ������Ծ
            {
                jumpble = false;
                rb.AddRelativeForce(Vector3.up * jumpsize, ForceMode.VelocityChange);
                StartCoroutine(Actorjump());
                networkAnimator0.SetTrigger("Jump");
                actor0.stamina = actor0.stamina - 5;
                StartCoroutine("Staminareduce");
                soundplayer.clip = remotebulid0.Music0["roll"];
                soundplayer.Play();
            }

        }
    }
    public void Playerattack()
    {
        if (IsOwner && IsClient)
        {
            if (attackble && actor0.stamina > 5)
            {
                if (Random.Range(0, 1f) >= 0.3f)//�����޶�
                {

                    networkAnimator0.SetTrigger("Isattack");
                    attackble = false;
                    actor0.stamina = actor0.stamina - 5;
                    StartCoroutine("Staminareduce");
                }
                else
                {

                    networkAnimator0.SetTrigger("Isweapon");
                    attackble = false;
                    actor0.stamina = actor0.stamina - 5;
                    StartCoroutine("Staminareduce");
                }
               //int i=   Random.Range(0, 4);
               // soundplayer.clip = musics[i];
               // soundplayer.Play();
            }
        }
    }
    public void Playerroll()//��ɫ���� ������Ծ�޶�
    {
        if (IsOwner && IsClient)
        {
            if (jumpble && attackble && actor0.stamina > 5)
            {
                if (s > 0)//����
                {
                    animator0.SetInteger("Rolldre", 1);
                    networkAnimator0.SetTrigger("Roll");
                    jumpble = false;
                    StartCoroutine(Actorjump());
                    actor0.stamina = actor0.stamina - 5;
                    StartCoroutine("Staminareduce");
                    soundplayer.clip = remotebulid0.Music0["jump"];
                    soundplayer.Play();
                }
                if (s < 0)//����
                {
                    animator0.SetInteger("Rolldre", 2);
                    networkAnimator0.SetTrigger("Roll");
                    jumpble = false;
                    StartCoroutine(Actorjump());
                    actor0.stamina = actor0.stamina - 5;
                    StartCoroutine("Staminareduce");
                    soundplayer.clip = remotebulid0.Music0["jump"];
                    soundplayer.Play();
                }
               
            }
        }
    }
    public void Playerskill(int skillnum)
    {
        if (IsOwner && IsClient)
        {
            if (weapon && actor0.stamina > 20&&attackble)
            {
                attackble = false;
                weapon = false;
                actor0.stamina = actor0.stamina - 20;
                StartCoroutine("Staminareduce");
                networkAnimator0.SetTrigger("Skill");
                //soundplayer.clip = remotebulid0.Music0["skill"];
                //soundplayer.Play();
            }
        }
    }
    public void Playerskillstart()
    {
        if (IsOwner && IsClient)
        {
           
            
            soundplayer.gameObject.transform.parent.GetChild(3).GetComponent<AudioSource>().clip= remotebulid0.Music0["skill"];
            soundplayer.gameObject.transform.parent.GetChild(3).GetComponent<AudioSource>().Play();
            BroadcastexpServerRpc(this.transform.GetChild(9).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(5).position, "Skillball", this.name);//����λ��
          
           
            
        }
    }
    public void Skillrecover()
    {

        weapon = true;

    }

    public void Playersprit()//���
    {
        if (IsOwner && IsClient)
        {
            if (jumpble && attackble && spritable && actor0.stamina > 5)
            {
                spritable = false;
                speedpa += actor0.speed;
                speedpa += actor0.speed;
                buttonpanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "ֹͣ";
                StartCoroutine("PlayerspritIE");
                StartCoroutine("Staminareduce");


            }
            else if (jumpble && attackble && spritable == false)
            {
                spritable = true;
                speedpa = actor0.speed;
                buttonpanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "���";
                StopCoroutine("PlayerspritIE");

            }
        }
    }
    IEnumerator PlayerspritIE()//���Э��
    {
        WaitForSeconds loop = new WaitForSeconds(1);

        for (; actor0.stamina > 5;)
        {
            actor0.stamina = actor0.stamina - 5;
            yield return loop;
        }
        spritable = true;
        speedpa = actor0.speed;
        buttonpanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "���";

    }
    IEnumerator PlayerrecoverIE()//��ɫ��������/�����ظ����,�������һ��
    {
        WaitForSeconds loop = new WaitForSeconds(5);
        yield return new WaitForSeconds(5);
        for (; actor0.health > 0;)
        {
            yield return loop;
            if (actor0.health < 100)
            {
                actor0.health += 2f;
                if (joystick1.Direction.sqrMagnitude != 0)
                {
                    actor0.health += actor0.healthrecover;
                    sliderhealth.value = actor0.health;
                    sliderbackground.value = actor0.health;
                }
                else
                {
                    actor0.health += 1.5f * actor0.healthrecover;
                    sliderhealth.value = actor0.health;
                    sliderbackground.value = actor0.health;
                }
            }
            if (actor0.stamina < 100)
            {
                actor0.stamina += 2f;
                if (joystick1.Direction.sqrMagnitude != 0)
                {
                    actor0.stamina += actor0.staminarecover;
                    sliderstamina.value = actor0.stamina;
                    staminabackground.value = actor0.stamina;
                }
                else
                {
                    actor0.stamina += 5 * actor0.staminarecover;
                    sliderstamina.value = actor0.stamina;
                    staminabackground.value = actor0.stamina;
                }
            }
        }
    }
    void Monvsp()
    {
        if (IsClient && IsOwner)
        {
            if (attackble)//�����ڼ��޷�ʹ����������
            {
                s = Input.GetAxis("Vertical");
                //  h = Input.GetAxis("Horizontal");
                if (s >= 0)
                {
                    transform.parent.Translate(0, 0, s * speedpa * Time.deltaTime, Space.Self);

                }
                else
                { transform.Translate(0, 0, s * 0.4f * speedpa * Time.deltaTime, Space.Self); }

                if (Input.GetKey(KeyCode.D))
                {
                    transform.localEulerAngles += new Vector3(0, speedpa * 10 * Time.deltaTime, 0);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.localEulerAngles += new Vector3(0, -speedpa * 10 * Time.deltaTime, 0);

                }
                animator0.SetFloat("Myspeed", s = (Mathf.Abs(s) > 0.05f) ? s : 0);//��Ŀ�����,λ�ƶ�������
                if (Input.GetKey(KeyCode.LeftShift) && animator0.GetBool("Isswim") == false && Mathf.Abs(s) > 0.05f && s >= 0.95)
                {
                    animator0.SetFloat("Isrun", 1);
                    speedpa = 10;
                }
                else
                {
                    animator0.SetFloat("Isrun", 0);
                    speedpa = 5;
                }

                if (Input.GetKey(KeyCode.Space) && jumpble && attackble && animator0.GetBool("Isswim") == false)//��Ծ�޶�,��ˮ������Ծ
                {
                    rb.AddRelativeForce(Vector3.up * jumpsize, ForceMode.VelocityChange);
                    // rb.AddRelativeForce(Vector3.forward * jumpsize, ForceMode.VelocityChange);
                    StartCoroutine(Actorjump());
                    jumpble = false;
                    networkAnimator0.SetTrigger("Jump");
                    //   animator0.SetBool("Jumpable", jumpble);
                }

                if (Input.GetKey(KeyCode.J))//�����޶�
                {
                    // StartCoroutine(Actorattack());
                    networkAnimator0.SetTrigger("Isattack");
                    attackble = false;

                }
                if (Input.GetKey(KeyCode.K))//�����޶�
                {
                    // StartCoroutine(Actorattack());
                    networkAnimator0.SetTrigger("Isweapon");
                    attackble = false;

                }

                if (Mathf.Abs(s) < 0.05f)//stannpos ��ʱ��
                {
                    tc += 0.01f;
                    animator0.SetFloat("Time", tc);
                }
                if (Mathf.Abs(s) >= 0.05f)
                {
                    tc = 0;
                    animator0.SetFloat("Time", tc);

                }

            }
            if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.W))//����
            {
                animator0.SetInteger("Rolldre", 1);
                networkAnimator0.SetTrigger("Roll");
                //attackble = false;
            }
            if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.S))//����
            {
                animator0.SetInteger("Rolldre", 2);
                networkAnimator0.SetTrigger("Roll");
                //attackble = false;
            }
            if (Input.GetKey(KeyCode.P))
            {
                Useskill(0,true);
            }
            if (weapon)
            {
                animator0.SetBool("Weapon", true);
            }

            SerchangeServerRpc(transform.position, transform.rotation, s, animator0.GetFloat("Isrun"), animator0.GetInteger("Rolldre"), tc);//֪ͨ������������ֵ
            Rotatemove();
        }


        else
        {
            transform.position = networkVariablepo.Value;
            transform.rotation = networkVariableQe.Value;
        }

    }
    void Monvspmoblie()
    {if (alive)
        {
            if (IsOwner&&IsClient)
            {
                if (attackble)//�����ڼ��޷�ʹ����������
                {
                    if (joystick1.Vertical >= 0)

                    { s = joystick1.Direction.normalized.magnitude; }
                    else if (s < 0.4f)
                    { s = -joystick1.Direction.normalized.magnitude; }

                    if (Mathf.Abs(joystick1.Horizontal) > 0.4f)
                        transform.rotation *= Quaternion.Euler(0, joystick1.Horizontal * 2, 0);
                    if (s >= 0)
                    {
                        transform.Translate(0, 0, s * speedpa * Time.deltaTime, Space.Self);
                    }
                    else
                    { transform.Translate(0, 0, s * 0.4f * speedpa * Time.deltaTime, Space.Self); }

                    animator0.SetFloat("Myspeed", s = (Mathf.Abs(s) > 0.1f) ? s : 0);//��Ŀ�����,λ�ƶ�������         
                    if (Mathf.Abs(s) < 0.05f)//stannpos ��ʱ��
                    {
                        tc += 0.01f;
                        animator0.SetFloat("Time", tc);
                        if (soundplayerstep.isPlaying)
                            soundplayerstep.Pause();


                    }
                    if (Mathf.Abs(s) >= 0.05f)
                    {
                        tc = 0;
                        animator0.SetFloat("Time", tc);
                        if (!soundplayerstep.isPlaying&&jumpble)
                            soundplayerstep.Play();


                    }
                    BroadcaststateServerRpc(actor0.health, actor0.stamina);//Ѫ��״̬ͬ����������
                    Rotatemovemoblie();
                }
               
                else
                { soundplayerstep.Pause();
                    BroadcaststateServerRpc(actor0.health, actor0.stamina);//Ѫ��״̬ͬ����������
                    Rotatemovemoblie();
                }
            
            }
            else
            {
                sliderhealth.value = nethealth.Value;
                sliderbackground.value = nethealth.Value;
                sliderstamina.value = netstamina.Value;
                staminabackground.value = netstamina.Value;
                
            }
        }

    }//��ɫ�����ƶ�
    void Movenew()
    {
        if (IsClient && IsOwner)
        {
            if (attackble)//�����ڼ��޷�ʹ����������
            {
                RaycastHit hit;
                transform.Translate(joystick1.Horizontal * speedpa * Time.deltaTime,0,joystick1.Vertical * speedpa * Time.deltaTime);
                Physics.Raycast(transform.position, joystick1.Directionxz * 10, out hit, 10);
             transform.LookAt(transform.position+joystick1.Directionxz);
                Debug.DrawRay(transform.position, joystick1.Directionxz, Color.red);
                Debug.Log("����"+joystick1.Directionxz);
            }
            Rotatemovemoblie();
        }

    }
    void Roll()
    {
        if (IsOwner && IsClient)
        {
            if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.W))//����
            {
                animator0.SetInteger("Rolldre", 1);
                networkAnimator0.SetTrigger("Roll");
                //SerchangeServerRpc(1, 1, true, true, true, 3);
                attackble = false;
            }
            if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.S))//����
            {
                animator0.SetInteger("Rolldre", 2);
                networkAnimator0.SetTrigger("Roll");
                //SerchangeServerRpc(1, 2, true, true, true, 3);
                attackble = false;
            }

        }
        else
        {

            animator0.SetInteger("Rlofre", rollnum.Value);

        }



    }
    void Rotatemove()
    {
        if (Input.GetMouseButton(1))
        {
            x0 = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.Lerp(new Vector3(0, transform.position.y, 0), Vector3.up * 3 * x0, 1f));
        }
        else
        { x0 = 0; }
        if (Input.GetMouseButton(0))
        {
            CinemachineFree.m_YAxis.m_MaxSpeed = 2;
            CinemachineFree.m_XAxis.m_MaxSpeed = 300;

        }
        else
        {

            CinemachineFree.m_YAxis.m_MaxSpeed = 0;
            CinemachineFree.m_XAxis.m_MaxSpeed = 0;
            CinemachineFree.m_YAxisRecentering.m_enabled = true;


        }

    }


    void Rotatemovemoblie()//�ƶ����Ӿ��ٿ�
    {
        if (joystick2.Direction.sqrMagnitude != 0)
        {
            CinemachineFree.m_YAxis.m_MaxSpeed = 1;
            CinemachineFree.m_XAxis.m_MaxSpeed = 150;
            CinemachineFree.m_XAxis.m_InputAxisValue = joystick2.Horizontal;
            CinemachineFree.m_YAxis.m_InputAxisValue = joystick2.Vertical;
            CinemachineFree.m_YAxisRecentering.m_enabled = false;

            //      Debug.Log("x=" + joystick2.Vertical+"---y="+joystick2.Horizontal);



        }
        else
        {
            CinemachineFree.m_YAxis.m_MaxSpeed = 0;
            CinemachineFree.m_XAxis.m_MaxSpeed = 0;
            CinemachineFree.m_YAxisRecentering.m_enabled = true;
        }


    }
    [ServerRpc]
    public void SerchangeServerRpc(Vector3 po, Quaternion qe, float sp, float runsp, int roll, float tcnet)
    {
        networkVariablepo.Value = po;
        networkVariableQe.Value = qe;
        anmarorspeed.Value = sp;
        animatorrunspeed.Value = runsp;
        rollnum.Value = roll;
        timenet.Value = tcnet;
    }
    [ServerRpc]
    public void SerchangeServerRpc(int intnum0, int roll, bool isground, bool isswim, bool isjump, int case0)
    {
        switch (case0)
        {
            case 0:
                standposnet.Value = intnum0;
                break;
            case 1:
                Isgroundnet.Value = isground;
                break;
            case 2:
                Isswimnet.Value = isswim;
                break;
            case 3:
                rollnum.Value = roll;
                break;
            case 4:
                Isjumpablenet.Value = isjump;
                break;
        }


    }

    void Useskill(int i,bool str)//��ϻ���� λ�Ƽ���
    {
        if (IsOwner && IsClient)
        {
            //if (poller.transform.childCount <= i)
            //{
            //    GameObject gameObject0 = new GameObject();
            //    gameObject0.name = clonearry[i].name;
            //    gameObject0.AddComponent<NetworkObject>();
            //    gameObject0.GetComponent<NetworkObject>().Spawn();
            //    gameObject0.transform.parent = poller.transform;
            //}

            // Poll.Getinstance().Netinsgameobj(clone[i], transform.position + new Vector3(0, 1f, 1f), this.transform.rotation, poller.transform.GetChild(i).transform);//�̳е���ģʽ��Mono���ֱ࣬�ӵ���
            //Poll.Getinstance().Insgameobj(clone[i], transform.position + new Vector3(0, 1f, 1f), this.transform.rotation, poller.transform.GetChild(i).transform);//�̳е���ģʽ��Mono���ֱ࣬�ӵ���
            //  Poll.Getinstance().NetInsgameobj(clone[i], transform.position + new Vector3(0, 1f, 1f), this.transform.rotation, poller.transform.GetChild(i).transform);//�̳е���ģʽ��Mono���ֱ࣬�ӵ���

            BroadcastskillballServerRpc(this.transform.position, i, str);
            //try
            //{ Netpool.Getinstance().NetInsgameobj(clonearry[skillnum.Value], skillpos.Value + new Vector3(0, 1, 1), this.transform.rotation, poller.transform).SetActive(skillbool.Value); }
            //catch
            //{ }

            // netpool0.NetInsgameobj(clonearry[skillnum.Value], skillpos.Value, this.transform.rotation, poller.transform);//���绺���
        }
        else
        {
            // netpoolerclone.transform.position = skillpos.Value;
            //  netpoolerrpc.transform.position = skillpos.Value;
            //  Netpool.Getinstance().NetInsgameobj(clonearry[skillnum.Value], skillpos.Value + new Vector3(0, 1, 1), this.transform.rotation, poller.transform).transform.position= skillpos.Value;

            //this.transform.position = skillpos.Value;
            //i = skillnum.Value;
            //str = skillbool.Value;
            //Netpool.Getinstance().NetInsgameobj(clonearry[i], this.transform.position + new Vector3(0, 1, 1), this.transform.rotation, poller.transform).SetActive(str);

        }
    
       

    }
    [ServerRpc]
    public void BroadcastskillballServerRpc(Vector3 po, int num,bool str)//֪ͨ���������õ���,�ڲ������½�����
    {
        skillpos.Value = po;
        skillnum.Value = num;
        skillbool.Value = str;
        Debug.Log("���������ڴ�����Ϣ");
 
        Netpool.Getinstance().NetInsgameobj(clonearry[num], po + new Vector3(0, 1, 1), this.transform.rotation, poller.transform).SetActive(str);

    }
    /// <summary>
    /// ��ײ�����߼��ϵͳ
    /// </summary>
    /// <param name="other"></param>

    private void OnTriggerEnter(Collider other)
    {


        if (other.tag.Equals("Player"))
        {
            if (IsClient && IsOwner)
            {
                ulong otherclientID = other.GetComponent<NetworkObject>().OwnerClientId;
                UpdateplayermeetServerRpc(this.OwnerClientId, otherclientID);
            }

        }
        if (other.tag.Equals("Sword") && IsClient && IsOwner && attackble)
        {
            Debug.Log("���" + OwnerClientId + "�ܵ�����");
            attackble = false;
            networkAnimator0.SetTrigger("Down");
            actor0.health -= 80;
            StartCoroutine(Healthaddreduce());



        }
        if (other.tag.Equals("Transdoor") && IsClient && IsOwner)
        {
            Debug.Log("���" + OwnerClientId + "����");
            transform.position = new Vector3(552 + UnityEngine.Random.Range(0, 7), 20, 472 + UnityEngine.Random.Range(0, 7));

        }
        if (other.tag.Equals("Arena") && IsClient && IsOwner)
        {
            Debug.Log("���" + OwnerClientId + "���뾺����");


        }
        if (other.tag.Equals("Transdoor1") && IsClient && IsOwner)
        {
            Debug.Log("���" + OwnerClientId + "����񷶥");
            transform.position = new Vector3(335 + UnityEngine.Random.Range(0, 7), 10, 585 + UnityEngine.Random.Range(0, 7));


        }
        if (other.tag.Equals("Water"))
        {
            if (IsClient && IsOwner)
            {
                Debug.Log("����ˮ��");
                postprocessing.profile.TryGet<ColorAdjustments>(out ColorAdjustments color);
                color.active = true;
                //GetComponent<Rigidbody>().useGravity = false;
                networkAnimator0.SetTrigger("Swim");
                animator0.SetBool("Isswim", true);
                speedpa = 2;
            }

        }
        if (other.tag.Equals("Hit")&&alive)//�ܵ��������Խ�ɫIDȷ������˭
        {
            if (IsClient && IsOwner && other.name != netID.ToString())
            {
                Debug.Log("�ܵ�����");
                networkAnimator0.SetTrigger("Hit");
                actor0.health -= 10;
                StartCoroutine(Healthaddreduce());
                float ii = Random.Range(0, 1f);
                if (Random.Range(0, 1f) > 0.7f)
                {
                    if (actor0.Isman)
                    {
                        if (ii > 0 && ii <= 0.35f)
                        {
                            soundpain.clip = remotebulid0.Music0["painm1"];
                            soundpain.Play();
                        }
                        else if (ii > 0.35 && ii <= 0.7f)
                        {
                            soundpain.clip = remotebulid0.Music0["painm2"];
                            soundpain.Play();

                        }
                        else
                        {

                            soundpain.clip = remotebulid0.Music0["painm3"];
                            soundpain.Play();
                        }
                    }
                    else
                    {
                        if (ii > 0 && ii <= 0.35f)
                        {
                            soundpain.clip = remotebulid0.Music0["painw1"];
                            soundpain.Play();
                        }
                        else if (ii > 0.35 && ii <= 0.7f)
                        {
                            soundpain.clip = remotebulid0.Music0["painw2"];
                            soundpain.Play();

                        }
                        else
                        {

                            soundpain.clip = remotebulid0.Music0["painw3"];
                            soundpain.Play();
                        }
                    }
                }
            }
        }
        if (other.tag.Equals("Effect1"))//
        {
            if (IsClient && IsOwner)
            {
                Debug.Log("ʰȡEXP");
                actor0.health += 30;
                if (actor0.health > 100)
                    actor0.health = 100;



            }
        }
        if (other.tag.Equals("Skillball")&&alive)//
        {
            if (IsClient && IsOwner)
            {
              //  Debug.Log("�ܵ������˺�");
                actor0.health -= 30;
                networkAnimator0.SetTrigger("Hit");
                StartCoroutine(Healthaddreduce());
                float ii = Random.Range(0, 1f);
                if (Random.Range(0, 1f) > 0.1f)
                {
                    if (actor0.Isman)
                    {
                        if (ii > 0 && ii <= 0.35f)
                        {
                            soundpain.clip = remotebulid0.Music0["painm1"];
                            soundpain.Play();
                        }
                        else if (ii > 0.35 && ii <= 0.7f)
                        {
                            soundpain.clip = remotebulid0.Music0["painm2"];
                            soundpain.Play();

                        }
                        else
                        {

                            soundpain.clip = remotebulid0.Music0["painm3"];
                            soundpain.Play();
                        }
                    }
                    else
                    {
                        if (ii > 0 && ii <= 0.35f)
                        {
                            soundpain.clip = remotebulid0.Music0["painw1"];
                            soundpain.Play();
                        }
                        else if (ii > 0.35 && ii <= 0.7f)
                        {
                            soundpain.clip = remotebulid0.Music0["painw2"];
                            soundpain.Play();

                        }
                        else
                        {

                            soundpain.clip = remotebulid0.Music0["painw3"];
                            soundpain.Play();
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Arena") && IsOwner)
        {
            Debug.Log("���" + OwnerClientId + "�뿪������");
        }

        if (other.tag.Equals("Dome") && IsOwner)
        {
            Debug.Log("���" + OwnerClientId + "�߳�񷶥");
        }
        if (other.tag.Equals("Water"))
        {
            if (IsClient && IsOwner)
            {
                postprocessing.profile.TryGet<ColorAdjustments>(out ColorAdjustments color);
                color.active = false;
                GetComponent<Rigidbody>().useGravity = true;
                animator0.SetBool("Isswim", false);
                speedpa = 5;
            }
        }
    }
    private void Rayfalldown()
    {
        RaycastHit hit;
        RaycastHit hit0;
        if (IsOwner && IsClient)
        {
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.05f) || Physics.Raycast(transform.GetChild(4).position, transform.GetChild(4).transform.forward, out hit0, 2f))
            {
                Debug.DrawRay(transform.position, Vector3.down, Color.red);
                Debug.DrawRay(transform.GetChild(4).position, transform.GetChild(4).transform.forward, Color.blue);
                animator0.SetBool("Isground", true);
            }
            else
            {
                animator0.SetBool("Isground", false);

            }
        }
    }


    [ServerRpc(RequireOwnership = false)]
    public void UpdateplayermeetServerRpc(ulong from, ulong to)//__>>����δ��⣬�������յ���Ϣfrom->to
    {
        ClientRpcParams clientRpc = new ClientRpcParams
        {

            Send = new ClientRpcSendParams
            {

                // TargetClientIds = new ulong[] { to }
                TargetClientIds = new ulong[] { to }

            }

        };
        NtifyplayermeetClientRpc(from, clientRpc);
    }
    [ClientRpc]
    public void NtifyplayermeetClientRpc(ulong from, ClientRpcParams clientRpc)//������֪ͨ�ͻ���
    {
        if (!IsOwner)
        {

            Debug.Log("�����" + from + "��ײ");
        }


    }

}

