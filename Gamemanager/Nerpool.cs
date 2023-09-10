using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
/// <summary>
/// 网络缓存池
/// </summary>
///

public class Netpool :NoneMonoinstance<Netpool>
{
    public Dictionary<string, List<GameObject>> pooldic = new Dictionary<string, List<GameObject>>();
    public GameObject Rootpoll;
    public GameObject Insgameobj(string name)//基础单例缓存池初始化
    {
        GameObject gameObject0 = null;
        if (pooldic.ContainsKey(name) && pooldic[name].Count > 0)
        {
            gameObject0 = pooldic[name][0];
            pooldic[name].RemoveAt(0);
        }
        else
        {
            gameObject0 = GameObject.Instantiate(Resources.Load<GameObject>(name));
            gameObject0.name = name;//把对象名字改为缓存池抽屉名字
            if (Rootpoll == null)//场景创建物体 作为克隆体节点
                Rootpoll = new GameObject("Rootpoll");
            gameObject0.transform.parent = Rootpoll.transform;
        }
        gameObject0.SetActive(true);
        return gameObject0;
    }

    public GameObject Insgameobj(GameObject obj, Vector3 vector, Quaternion quaternion, Transform parent)//普通单例缓存池初始化
    {
        GameObject gameObject0 = null;
        if (pooldic.ContainsKey(obj.name) && pooldic[obj.name].Count > 0)//如果缓存池有抽屉或者位置够，则变化位置激活
        {
            gameObject0 = pooldic[obj.name][0];
            pooldic[obj.name].RemoveAt(0);
            gameObject0.transform.position = vector;
            gameObject0.transform.rotation = quaternion;
            foreach (var item in pooldic)
            {

                Debug.Log("有位置/抽屉" + item.Key + "-----" + item.Value.Count+"单池状况"+pooldic.ContainsKey(obj.name));

            }
        }
        else//如果缓存池没有抽屉或者位置不够，则增加
        {
            gameObject0 = GameObject.Instantiate(obj, vector, quaternion, parent);
            gameObject0.name = obj.name;
            if (pooldic.ContainsKey(obj.name))
            {
                pooldic[obj.name].Add(obj);
                Debug.Log("位置不够创造新位置");
            }
            else
            {
                pooldic.Add(obj.name, new List<GameObject>() { obj });
                Debug.Log("创造新抽屉");
                foreach (var item in pooldic)
                {

                    Debug.Log("新抽屉检查" + item.Key + "<<<++++++++>>>>" + item.Value.Count + "<<<<<<");

                }
            }
        }
        try
        {
            gameObject0.SetActive(true);
            Debug.Log("重新激活");
        }
        catch (Exception e)
        { Debug.Log(e); }
        return gameObject0;

    }

    public GameObject NetInsgameobj(GameObject obj, Vector3 vector, Quaternion quaternion, Transform parent)//网络缓存池初始化
    {
        GameObject gameObject0 = null;
        if (pooldic.ContainsKey(obj.name+ "(Clone)") && pooldic[obj.name + "(Clone)"].Count > 0)//如果缓存池有抽屉或者位置够，则变化位置激活
        {
            gameObject0 = pooldic[obj.name+ "(Clone)"][0];
            pooldic[obj.name + "(Clone)"].RemoveAt(0);
            gameObject0.transform.position = vector;
            gameObject0.transform.rotation = quaternion;
            foreach (var item in pooldic)
            {

                Debug.Log("有位置/抽屉" + item.Key + "-----" + item.Value.Count);

            }
        }
        else//如果缓存池没有抽屉或者位置不够，则增加
        {
            gameObject0 = GameObject.Instantiate(obj, vector, quaternion, parent);
            //gameObject0.name = obj.name;//把对象名字改为缓存池抽屉名字
            if (pooldic.ContainsKey(obj.name + "(Clone)"))
            {
                pooldic[obj.name + "(Clone)"].Add(obj);
                Debug.Log("位置不够创造新位置");
            }
            else
            {
                pooldic.Add(obj.name + "(Clone)", new List<GameObject>() { gameObject0 });
                try
                {
                    foreach (var item in pooldic)
                    {

                        Debug.Log("抽屉不够,创造新抽屉当前" + item.Key + "++++++++" + item.Value.Count);

                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
        //Gameobjset gameobjset0 = new Gameobjset();
        //gameobjset0.Setactiveobj(true);
        Debug.Log("服务器调动方法设置物体状态");
       // gameObject0.SetActive(true);
        if (!gameObject0.GetComponent<NetworkObject>().IsSpawned)
        {
            gameObject0.GetComponent<NetworkObject>().Spawn();
            try
            {
               //gameObject0.GetComponent<NetworkObject>().Spawn();
                gameObject0.GetComponent<NetworkObject>().TrySetParent(parent);
            }
            catch (Exception e)
            { Debug.Log(e); }
        }
        return gameObject0;
    }
    /// <summary>
    /// 放东西，失活
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void Pushobject(string name, GameObject obj)
    {
    
        Debug.Log("开始判断");
        if (pooldic.ContainsKey(name))
        {
            pooldic[name].Add(obj);
            Debug.Log("失活物体压入抽屉");
        }
        else
        {
            Debug.Log("线下没有相应的Key，创建新抽屉" + name + "--是否包含KEY" + pooldic.ContainsKey(name));
            pooldic.Add(name, new List<GameObject>() { obj });//链表添加obj gameobject
            foreach (var item in pooldic)
            {

                Debug.Log("压缩前排查" + item.Key + "<<<++++++++>>>>" + item.Value.Count + "<<<<<<--是否包含KEY"+ pooldic.ContainsKey(name)+"物体名字"+obj.name);

            }
        }
        if (obj.activeSelf)
            obj.SetActive(false);//失活
    }

    public void NetPushobject(bool booler ,GameObject obj)
    {

 
        if (obj.activeSelf)
        obj.SetActive(booler);//失活
        //if (obj.GetComponent<NetworkObject>().IsSpawned)
        //{
        //    obj.GetComponent<NetworkObject>().Despawn();
        //    Debug.Log("解除播种");
        //};
        if (pooldic.ContainsKey(obj.name ))
        {
            pooldic[obj.name ].Add(obj);
            Debug.Log("失活物体压入抽屉");
        }
        else
        {
            pooldic.Add(obj.name , new List<GameObject>() { obj });//链表添加obj gameobject
            Debug.Log("么有相应的Key，创建新抽屉" + obj.name);
            foreach (var item in pooldic)
            {

                Debug.Log(item.Key + "<<<++++++++>>>>" + item.Value.Count+"<<<<<<");
               
            }
        }


    }
    public void Clearpoll()//过场清算
    {

        pooldic.Clear();
        Rootpoll = null;



    }
}

