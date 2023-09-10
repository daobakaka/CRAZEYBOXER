using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Swordeffects : NetworkBehaviour
{
    public float looptime = 1;
    // Start is called before the first frame update
    /// <summary>
    /// 以下代码用于其他
    /// </summary>
    private NetworkVariable<bool> objactive = new NetworkVariable<bool>(true);

    //public override void OnNetworkSpawn()//在start 之前执行,相当于on aweak
    //{
    //    objactive.OnValueChanged += (prev, newv) =>
    //    {

    //        this.gameObject.SetActive(newv);
    //       // Debug.Log("更改状态真实");

    //    };
    //    this.gameObject.SetActive(objactive.Value);
    //   // Debug.Log("网络开始");
    //}

    //public void Setactiveobj(bool act)
    //{
    //    if (IsServer)
    //    {
    //        objactive.Value = act;


    //    }
    //    else if (IsClient)
    //    {
    //        SetNetworkobjactiveServerRpc(act);
    //    }

    //}
    //[ServerRpc(RequireOwnership = false)]
    //public void SetNetworkobjactiveServerRpc(bool act)
    //{

    //    objactive.Value = act;

    //}
//-->>>>
/// <summary>
/// 此段情景只使用如下代码
/// </summary>
    private void OnEnable()
    {
        Invoke("Hideme", looptime);
       
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Hideme()
    {

        gameObject.SetActive(false);
    
    }
}
