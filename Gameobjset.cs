using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Gameobjset : NetworkBehaviour///����������������ʧ�㲥
{
    public NetworkVariable<bool> objactive = new NetworkVariable<bool>(true);
    public NetworkVariable<bool> newtrue=new NetworkVariable<bool>(true);
    public Netpool netpool0;
    //public override void OnNetworkSpawn()//��start ֮ǰִ��,�൱��on aweak
    //{
    //    objactive.OnValueChanged += (prev, newv) =>
    //    {

    //       this.gameObject.SetActive(newv);


    //    };
    //    this.gameObject.SetActive(objactive.Value);
    //    Debug.Log("��������");

    //}
    public override void OnNetworkSpawn()
    {

        objactive.OnValueChanged += (prev, newv) =>
        {
        
                this.gameObject.SetActive(newv);
               
            
            Debug.Log("��������,����ֵ");
        };
        this.gameObject.SetActive(objactive.Value);
    }
    public void Setactiveobj(bool act)
    {
  
     
            SetNetworkobjactiveServerRpc(act);
        
    }
    [ServerRpc(RequireOwnership = false)]
    public void SetNetworkobjactiveServerRpc(bool act)
    {

        objactive.Value = act;

    }
    [ServerRpc(RequireOwnership = false)]
    public void SetNettrueServerRpc(bool act)
    {
        objactive.Value = act;

       Netpool.Getinstance().NetPushobject(objactive.Value, this.gameObject);
    }
    public void BaseobjSet(string name)
    {
        netpool0.Pushobject(this.gameObject.name ,this.gameObject);
        
    }

    [ServerRpc(RequireOwnership = false)]
    public void NetSetNetworkobjactiveServerRpc(bool act)
    {

        objactive.Value = act;

        Netpool.Getinstance().NetPushobject(objactive.Value, this.gameObject);//���û����ʧ���
    }
    public void NetSetactiveobj(bool act)
    {
        //   NetSetNetworkobjactiveServerRpc(act);
        SetNettrueServerRpc(act);
    }
    public  void   Underlinepush(string name,GameObject obj)
    {
        Netpool.Getinstance().Pushobject(name, obj);
    
    }


    //////
    ///���������ʽ
    public void Serversetactiv(bool act)
    {

        SetNettrueServerRpc(act);

    }


}
