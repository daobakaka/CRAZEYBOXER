using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Swordeffects : NetworkBehaviour
{
    public float looptime = 1;
    // Start is called before the first frame update
    /// <summary>
    /// ���´�����������
    /// </summary>
    private NetworkVariable<bool> objactive = new NetworkVariable<bool>(true);

    //public override void OnNetworkSpawn()//��start ֮ǰִ��,�൱��on aweak
    //{
    //    objactive.OnValueChanged += (prev, newv) =>
    //    {

    //        this.gameObject.SetActive(newv);
    //       // Debug.Log("����״̬��ʵ");

    //    };
    //    this.gameObject.SetActive(objactive.Value);
    //   // Debug.Log("���翪ʼ");
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
/// �˶��龰ֻʹ�����´���
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
