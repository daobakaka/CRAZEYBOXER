using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
/// <summary>
/// 简要缓存池技术
/// </summary>
///

public class Poll : NoneMonoinstance<Poll>//继承单例模式非MOno基类
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

                Debug.Log("有位置/抽屉"+item.Key + "-----" + item.Value.Count);
            
            }
        }
        else//如果缓存池没有抽屉或者位置不够，则增加
        {
            gameObject0 = GameObject.Instantiate(obj, vector, quaternion, parent);
            gameObject0.name = obj.name;//把对象名字改为缓存池抽屉名字
            if (pooldic.ContainsKey(obj.name))
            {
                pooldic[obj.name].Add(gameObject0);
            }
            else
            {
                pooldic.Add(obj.name, new List<GameObject>() { gameObject0});
                Debug.Log("创造新抽屉");
            }
            try
            { Debug.Log("没有位置/抽屉" + pooldic[obj.name] + pooldic[obj.name].Count); }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        gameObject0.SetActive(true);
        return gameObject0;

    }

    public GameObject NetInsgameobj(GameObject obj, Vector3 vector, Quaternion quaternion, Transform parent)//网络单例缓存池初始化
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

                Debug.Log("有位置/抽屉" + item.Key + "-----" + item.Value.Count);

            }
        }
        else//如果缓存池没有抽屉或者位置不够，则增加
        {
            gameObject0 = GameObject.Instantiate(obj, vector, quaternion, parent);
            gameObject0.name = obj.name;//把对象名字改为缓存池抽屉名字
            if (pooldic.ContainsKey(obj.name))
            {
                pooldic[obj.name].Add(gameObject0);
            }
            else
            {
                pooldic.Add(obj.name, new List<GameObject>() { gameObject0 });
                Debug.Log("创造新抽屉");
            }
            try
            { Debug.Log("没有位置/抽屉" + pooldic[obj.name] + pooldic[obj.name].Count); }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        gameObject0.SetActive(true);
        if (!gameObject0.GetComponent<NetworkObject>().IsSpawned)
        {
            gameObject0.GetComponent<NetworkObject>().Spawn();
            gameObject0.GetComponent<NetworkObject>().TrySetParent(parent);
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
       // obj.GetComponent<NetworkObject>().Despawn();
        obj.SetActive(false);//失活
        if (pooldic.ContainsKey(name))
        {
            pooldic[name].Add(obj);
        }
        else
        {
            pooldic.Add(name, new List<GameObject>() { obj });//链表添加obj gameobject
        }
    }

    public void NetPushobject(string name, GameObject obj)
    {
        obj.SetActive(false);//失活
        if (pooldic.ContainsKey(name))
        {
            pooldic[name].Add(obj);
        }
        else
        {
            pooldic.Add(name, new List<GameObject>() { obj });//链表添加obj gameobject
        }




    }
    public void Clearpoll()//过场清算
    {

        pooldic.Clear();
        Rootpoll = null;



    }
}
