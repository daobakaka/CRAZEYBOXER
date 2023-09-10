using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
/// <summary>
/// ���绺���
/// </summary>
///

public class Netpool :NoneMonoinstance<Netpool>
{
    public Dictionary<string, List<GameObject>> pooldic = new Dictionary<string, List<GameObject>>();
    public GameObject Rootpoll;
    public GameObject Insgameobj(string name)//������������س�ʼ��
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
            gameObject0.name = name;//�Ѷ������ָ�Ϊ����س�������
            if (Rootpoll == null)//������������ ��Ϊ��¡��ڵ�
                Rootpoll = new GameObject("Rootpoll");
            gameObject0.transform.parent = Rootpoll.transform;
        }
        gameObject0.SetActive(true);
        return gameObject0;
    }

    public GameObject Insgameobj(GameObject obj, Vector3 vector, Quaternion quaternion, Transform parent)//��ͨ��������س�ʼ��
    {
        GameObject gameObject0 = null;
        if (pooldic.ContainsKey(obj.name) && pooldic[obj.name].Count > 0)//���������г������λ�ù�����仯λ�ü���
        {
            gameObject0 = pooldic[obj.name][0];
            pooldic[obj.name].RemoveAt(0);
            gameObject0.transform.position = vector;
            gameObject0.transform.rotation = quaternion;
            foreach (var item in pooldic)
            {

                Debug.Log("��λ��/����" + item.Key + "-----" + item.Value.Count+"����״��"+pooldic.ContainsKey(obj.name));

            }
        }
        else//��������û�г������λ�ò�����������
        {
            gameObject0 = GameObject.Instantiate(obj, vector, quaternion, parent);
            gameObject0.name = obj.name;
            if (pooldic.ContainsKey(obj.name))
            {
                pooldic[obj.name].Add(obj);
                Debug.Log("λ�ò���������λ��");
            }
            else
            {
                pooldic.Add(obj.name, new List<GameObject>() { obj });
                Debug.Log("�����³���");
                foreach (var item in pooldic)
                {

                    Debug.Log("�³�����" + item.Key + "<<<++++++++>>>>" + item.Value.Count + "<<<<<<");

                }
            }
        }
        try
        {
            gameObject0.SetActive(true);
            Debug.Log("���¼���");
        }
        catch (Exception e)
        { Debug.Log(e); }
        return gameObject0;

    }

    public GameObject NetInsgameobj(GameObject obj, Vector3 vector, Quaternion quaternion, Transform parent)//���绺��س�ʼ��
    {
        GameObject gameObject0 = null;
        if (pooldic.ContainsKey(obj.name+ "(Clone)") && pooldic[obj.name + "(Clone)"].Count > 0)//���������г������λ�ù�����仯λ�ü���
        {
            gameObject0 = pooldic[obj.name+ "(Clone)"][0];
            pooldic[obj.name + "(Clone)"].RemoveAt(0);
            gameObject0.transform.position = vector;
            gameObject0.transform.rotation = quaternion;
            foreach (var item in pooldic)
            {

                Debug.Log("��λ��/����" + item.Key + "-----" + item.Value.Count);

            }
        }
        else//��������û�г������λ�ò�����������
        {
            gameObject0 = GameObject.Instantiate(obj, vector, quaternion, parent);
            //gameObject0.name = obj.name;//�Ѷ������ָ�Ϊ����س�������
            if (pooldic.ContainsKey(obj.name + "(Clone)"))
            {
                pooldic[obj.name + "(Clone)"].Add(obj);
                Debug.Log("λ�ò���������λ��");
            }
            else
            {
                pooldic.Add(obj.name + "(Clone)", new List<GameObject>() { gameObject0 });
                try
                {
                    foreach (var item in pooldic)
                    {

                        Debug.Log("���벻��,�����³��뵱ǰ" + item.Key + "++++++++" + item.Value.Count);

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
        Debug.Log("����������������������״̬");
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
    /// �Ŷ�����ʧ��
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void Pushobject(string name, GameObject obj)
    {
    
        Debug.Log("��ʼ�ж�");
        if (pooldic.ContainsKey(name))
        {
            pooldic[name].Add(obj);
            Debug.Log("ʧ������ѹ�����");
        }
        else
        {
            Debug.Log("����û����Ӧ��Key�������³���" + name + "--�Ƿ����KEY" + pooldic.ContainsKey(name));
            pooldic.Add(name, new List<GameObject>() { obj });//�������obj gameobject
            foreach (var item in pooldic)
            {

                Debug.Log("ѹ��ǰ�Ų�" + item.Key + "<<<++++++++>>>>" + item.Value.Count + "<<<<<<--�Ƿ����KEY"+ pooldic.ContainsKey(name)+"��������"+obj.name);

            }
        }
        if (obj.activeSelf)
            obj.SetActive(false);//ʧ��
    }

    public void NetPushobject(bool booler ,GameObject obj)
    {

 
        if (obj.activeSelf)
        obj.SetActive(booler);//ʧ��
        //if (obj.GetComponent<NetworkObject>().IsSpawned)
        //{
        //    obj.GetComponent<NetworkObject>().Despawn();
        //    Debug.Log("�������");
        //};
        if (pooldic.ContainsKey(obj.name ))
        {
            pooldic[obj.name ].Add(obj);
            Debug.Log("ʧ������ѹ�����");
        }
        else
        {
            pooldic.Add(obj.name , new List<GameObject>() { obj });//�������obj gameobject
            Debug.Log("ô����Ӧ��Key�������³���" + obj.name);
            foreach (var item in pooldic)
            {

                Debug.Log(item.Key + "<<<++++++++>>>>" + item.Value.Count+"<<<<<<");
               
            }
        }


    }
    public void Clearpoll()//��������
    {

        pooldic.Clear();
        Rootpoll = null;



    }
}

