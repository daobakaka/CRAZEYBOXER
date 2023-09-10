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
    //>>怪物寻路系统
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
           // Debug.Log("新客户端进入怪物" + "服务" + IsServer + "主机" + IsHost + "客户端" + IsClient + "本地玩家" + IsLocalPlayer + "拥有者" + IsOwner + "服务器拥有者" + IsOwnedByServer);


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
        Debug.Log("开始寻找角色" + IsServer + ">>");
        for (; ; )
        {
            yield return new WaitForSeconds(4);
            if (attachplayer)//寻路组件获取完毕，怪物开始寻找玩家
            {
                try
                {
                  //  Transform targetchae = target;//缓存路标物体
                    GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
                    float min = Vector3.Distance(targets[0].transform.position, this.transform.position);
                    for (int i = 0; i < targets.Length; i++)
                    {
                        if (min > Vector3.Distance(targets[i].transform.position, this.transform.position))
                        {
                            min = Vector3.Distance(targets[i].transform.position, this.transform.position);
                            if (min < 150 && Vector3.Distance(paths[cha].transform.position, this.transform.position) < 300)//如果玩家与怪物距离小于200，怪物寻找玩家,且怪物与目标路标距离小于200(巡查范围)
                            { 
                                target = targets[i].transform;
                                nev.speed = 8;
                                if (target.position.y > -2)
                                {
                                    target = paths[cha].transform;
                                    nev.speed = 2;
                                }
                                //    Debug.Log("最小距离" + min);
                            }
                            else //玩家在附近必然不会触发else
                            {
                                target = paths[cha].transform;//目标缓存至原来目标,开始路标执行逻辑
                                nev.speed = 2;
                                // attachplayer = false;
                                //   Debug.Log("移交控制权，开始找寻路标1");

                            }
                        }
                        else if(min >=Vector3.Distance(targets[0].transform.position, this.transform.position))
                        {
                            if (min < 150 && Vector3.Distance(paths[cha].transform.position, this.transform.position) <300)//如果玩家与怪物距离小于100，怪物寻找玩家,且怪物与目标路标距离小于200(巡查范围)
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
                                target = paths[cha].transform;////目标缓存至原来目标,开始路标执行逻辑，只要距离不小于100
                                nev.speed = 2;// attachplayer = false;
                                              // Debug.Log("移交控制权，开始找寻路标2");
                            }
                        }
                       
                    }
                    attachplayer = false;
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    attachplayer = false;
                  Debug.Log("移交控制权，开始找寻路标3");
                    // startfind = false;
                }
            }
        }
    }

    IEnumerator Findpath()
    {
        yield return new WaitForSeconds(3);
        GameObject path = GameObject.FindWithTag("Enemyway1");
        Debug.Log("发现路标，开始生成节点");
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
             //   Debug.Log("节点距离"+min+"节点数"+i);
            }
            else if(min> Vector3.Distance(paths[0].transform.position, this.transform.position))
            {
                target = paths[0].transform;
                cha = 0;
            }
            

        }//第一次查找，把所有物体装入列表
        for (; ; )//开始节点循环
        {
            yield return new WaitForSeconds(4);//三秒循环一次
            if (attachplayer == false)//开始attachplayer是false
            {
                if (Vector3.Distance(paths[cha].transform.position, this.transform.position) < 8)//与当节点小于8时，重设目标，清节点
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

                //控制权移交
                startfind = true;//怪物获得目标开始移动
                attachplayer = true;//怪物可以开始寻找玩家.当前target已经确定
             //  Debug.Log("移交控制权,进入玩家循环"+"当前目标节点"+cha);
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
        public void SetNetworkobjactiveServerRpc(bool act)//状态传输至服务器
        {

            objactive.Value = act;
            //Debug.Log("服务器已接收攻击状态");

        }

        [ClientRpc]
        public void SetNetworkobjactiveClientRpc(bool act)//服务器通知客户端！！！！！！！！！！！！！！！！！，相当于广播,粒子效果动画播放
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
                    SetNetworkobjactiveClientRpc(objactive.Value);//黄金代码
                }
                //else
                //{ transform.GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(objactive.Value); 
                //    Debug.Log("客户端已执行");
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
