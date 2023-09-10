using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Gameobjset : NetworkBehaviour///类用于主控物体消失广播
{
    public NetworkVariable<bool> objactive = new NetworkVariable<bool>(true);
    public NetworkVariable<bool> newtrue=new NetworkVariable<bool>(true);
    public Netpool netpool0;
    //public override void OnNetworkSpawn()//在start 之前执行,相当于on aweak
    //{
    //    objactive.OnValueChanged += (prev, newv) =>
    //    {

    //       this.gameObject.SetActive(newv);


    //    };
    //    this.gameObject.SetActive(objactive.Value);
    //    Debug.Log("父级监听");

    //}
    public override void OnNetworkSpawn()
    {

        objactive.OnValueChanged += (prev, newv) =>
        {
        
                this.gameObject.SetActive(newv);
               
            
            Debug.Log("父级监听,更改值");
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

        Netpool.Getinstance().NetPushobject(objactive.Value, this.gameObject);//调用缓存池失活方法
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
    ///服务器激活方式
    public void Serversetactiv(bool act)
    {

        SetNettrueServerRpc(act);

    }


}
