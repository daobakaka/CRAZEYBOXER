using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : NetworkBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent nev;
    public Transform target;
    public List<GameObject> paths = new List<GameObject>();
    private Animator animator0;
    private NetworkAnimator networkAnimator0;
    //>>����Ѱ·ϵͳ
    private bool startfind = false;
    private bool attachplayer = false;
    private float distance0;
    public int cha = 0;
    //-->>

    private NetworkVariable<Vector3> networkVariablepo = new NetworkVariable<Vector3>(Vector3.zero);
    private NetworkVariable<Quaternion> networkVariableQe = new NetworkVariable<Quaternion>(Quaternion.identity);
    private NetworkVariable<int> clientID = new NetworkVariable<int>();
    private NetworkVariable<float> anmarorspeed = new NetworkVariable<float>();
    private NetworkVariable<bool> objactive = new NetworkVariable<bool>(true);
    private bool IsAttack = false;
    private bool Attakloop = true;
    private AudioSource soundmonster, soundsword;
    private Remotebulid remotebulid0;
    //-->>
    void Start()
    {
        networkAnimator0 = GetComponent<NetworkAnimator>();
        animator0 = GetComponent<Animator>();
        // target = GameObject.FindWithTag("Player").transform;
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            // StartClinet();
            //GameObject clone = Instantiate(freelookcin, trankmove.transform.parent.transform);
            //clone.GetComponent<NetworkObject>().Spawn();
            // trankmove.GetComponent<PlayableDirector>().Play();
           // Debug.Log("�¿ͻ��˽������" + "����" + IsServer + "����" + IsHost + "�ͻ���" + IsClient + "�������" + IsLocalPlayer + "ӵ����" + IsOwner + "������ӵ����" + IsOwnedByServer);


        };
        StartCoroutine(Findpath());
        StartCoroutine(Findobj());
        soundmonster = this.GetComponent<AudioSource>();
        soundsword = transform.GetChild(3).GetComponent<AudioSource>();
        remotebulid0 = GameObject.FindWithTag("Remotebuild").GetComponent<Remotebulid>();
        soundmonster.clip = remotebulid0.Music0["back3"];
        soundsword.clip = remotebulid0.Music0["sword"];
        soundmonster.Play();

        //this.transform.GetChild(4).GetComponent<MeshRenderer>().material = remotebulid0.Materials0[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (startfind)
        {
            if (IsOwner && target != null)
            {
                distance0 = (this.transform.position - target.position).sqrMagnitude;
                if (distance0 > 6)
                {
                    try
                    {
                        nev.SetDestination(target.position);
                        animator0.SetFloat("Speed", nev.velocity.sqrMagnitude);
                    }
                    catch (Exception e)
                    { }

                }
                else
                {
                    //animator0.SetTrigger("Attack");
                    if (Attakloop&&target.tag.Equals("Player"))
                    {
                        Attakloop = false;
                        networkAnimator0.SetTrigger("Attack");
                        //StopCoroutine("Swordeffects");
                        StartCoroutine(Swordeffects());
                        StartCoroutine(Attackrecover());
                    }
                }
            }
        }
    }

    IEnumerator Findobj()
    {
        yield return new WaitForSeconds(15);
        Debug.Log("��ʼѰ�ҽ�ɫ" + IsServer + ">>");
        for (; ; )
        {
            yield return new WaitForSeconds(4);
            if (attachplayer)//Ѱ·�����ȡ��ϣ����￪ʼѰ�����
            {
                try
                {
                  //  Transform targetchae = target;//����·������
                    GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
                    float min = Vector3.Distance(targets[0].transform.position, this.transform.position);
                    for (int i = 0; i < targets.Length; i++)
                    {
                        if (min > Vector3.Distance(targets[i].transform.position, this.transform.position))
                        {
                            min = Vector3.Distance(targets[i].transform.position, this.transform.position);
                            if (min < 150 && Vector3.Distance(paths[cha].transform.position, this.transform.position) < 300)//��������������С��200������Ѱ�����,�ҹ�����Ŀ��·�����С��200(Ѳ�鷶Χ)
                            { 
                                target = targets[i].transform;
                                nev.speed = 8;
                                if (target.position.y > -2)
                                {
                                    target = paths[cha].transform;
                                    nev.speed = 2;
                                }
                                //    Debug.Log("��С����" + min);
                            }
                            else //����ڸ�����Ȼ���ᴥ��else
                            {
                                target = paths[cha].transform;//Ŀ�껺����ԭ��Ŀ��,��ʼ·��ִ���߼�
                                nev.speed = 2;
                                // attachplayer = false;
                                //   Debug.Log("�ƽ�����Ȩ����ʼ��Ѱ·��1");

                            }
                        }
                        else if(min >=Vector3.Distance(targets[0].transform.position, this.transform.position))
                        {
                            if (min < 150 && Vector3.Distance(paths[cha].transform.position, this.transform.position) <300)//��������������С��100������Ѱ�����,�ҹ�����Ŀ��·�����С��200(Ѳ�鷶Χ)
                            {
                                target = targets[0].transform;
                                nev.speed = 8;
                                if (target.position.y > -2)
                                {
                                    target = paths[cha].transform;
                                    nev.speed = 2;
                                }

                            }
                            else
                            {
                                target = paths[cha].transform;////Ŀ�껺����ԭ��Ŀ��,��ʼ·��ִ���߼���ֻҪ���벻С��100
                                nev.speed = 2;// attachplayer = false;
                                              // Debug.Log("�ƽ�����Ȩ����ʼ��Ѱ·��2");
                            }
                        }
                       
                    }
                    attachplayer = false;
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    attachplayer = false;
                  Debug.Log("�ƽ�����Ȩ����ʼ��Ѱ·��3");
                    // startfind = false;
                }
            }
        }
    }

    IEnumerator Findpath()
    {
        yield return new WaitForSeconds(3);
        GameObject path = GameObject.FindWithTag("Enemyway1");
        Debug.Log("����·�꣬��ʼ���ɽڵ�");
        paths.Add(path.transform.GetChild(0).gameObject);
        float min = Vector3.Distance(paths[0].transform.position, this.transform.position);
        for (int i = 1; i < path.transform.childCount; i++)
        {

            paths.Add(path.transform.GetChild(i).gameObject);

            if (min > Vector3.Distance(paths[i].transform.position, this.transform.position))

            {
                min = Vector3.Distance(paths[i].transform.position, this.transform.position);
                target = paths[i].transform;
                cha = i;
             //   Debug.Log("�ڵ����"+min+"�ڵ���"+i);
            }
            else if(min> Vector3.Distance(paths[0].transform.position, this.transform.position))
            {
                target = paths[0].transform;
                cha = 0;
            }
            

        }//��һ�β��ң�����������װ���б�
        for (; ; )//��ʼ�ڵ�ѭ��
        {
            yield return new WaitForSeconds(4);//����ѭ��һ��
            if (attachplayer == false)//��ʼattachplayer��false
            {
                if (Vector3.Distance(paths[cha].transform.position, this.transform.position) < 8)//�뵱�ڵ�С��8ʱ������Ŀ�꣬��ڵ�
                {
                    try
                    {
                        target = paths[cha + 1].transform;
                        cha += 1;
                       
                    }
                    catch (Exception e)

                    {
                        Debug.Log(e.Message);
                        target = paths[0].transform;
                        cha = 0;
                       
                    }

                }

                //����Ȩ�ƽ�
                startfind = true;//������Ŀ�꿪ʼ�ƶ�
                attachplayer = true;//������Կ�ʼѰ�����.��ǰtarget�Ѿ�ȷ��
             //  Debug.Log("�ƽ�����Ȩ,�������ѭ��"+"��ǰĿ��ڵ�"+cha);
            }

        }
    }



        [ServerRpc]
        public void SerchangeServerRpc(Vector3 po, Quaternion qe, float sp)
        {


            networkVariablepo.Value = po;
            networkVariableQe.Value = qe;
            anmarorspeed.Value = sp;
        }
        [ServerRpc]
        public void SetNetworkobjactiveServerRpc(bool act)//״̬������������
        {

            objactive.Value = act;
            //Debug.Log("�������ѽ��չ���״̬");

        }

        [ClientRpc]
        public void SetNetworkobjactiveClientRpc(bool act)//������֪ͨ�ͻ��ˣ������������������������������������൱�ڹ㲥,����Ч����������
        {
            if (!IsOwner)
            {
                transform.GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(act);

            }


        }
        IEnumerator Swordeffects()
        {
            if (!IsAttack)
            {
                IsAttack = true;
                yield return new WaitForSeconds(1);

                if (IsServer)
                {
                    transform.GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(true);
                    soundsword.Play();
                    SetNetworkobjactiveServerRpc(true);
                    SetNetworkobjactiveClientRpc(objactive.Value);//�ƽ����
                }
                //else
                //{ transform.GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(objactive.Value); 
                //    Debug.Log("�ͻ�����ִ��");
                //}
                yield return new WaitForSeconds(1.76f);
                IsAttack = false;
            }
            //Swordeffec set = transform.GetChild(0).GetChild(2).GetChild(1).gameObject.GetComponent<Swordeffec>();
            //set.Setactiveobj(true);


        }
    IEnumerator Attackrecover()
    {
        yield return new WaitForSeconds(8);

        Attakloop = true;
    
    }
    }
