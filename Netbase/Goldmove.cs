using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Goldmove : NetworkBehaviour
{
    private NetworkVariable<bool> objactive = new NetworkVariable<bool>(true);

    public override void OnNetworkSpawn()//在start 之前执行,相当于on aweak
    {
        objactive.OnValueChanged += (prev, newv) =>
        {

            this.gameObject.SetActive(newv);

        };
        this.gameObject.SetActive(objactive.Value);
    }

    public void Setactiveobj(bool act)
    {
        if (IsServer)
        {
            objactive.Value = act;


        }
        else if (IsClient)
        {
            SetNetworkobjactiveServerRpc(act);
        }

    }
    [ServerRpc(RequireOwnership =false)]
    public void SetNetworkobjactiveServerRpc(bool act)
    {

        objactive.Value = act;

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (this.name != other.name)
            {
                Setactiveobj(false);
              //  Debug.Log("玩家" + other.name);
            }
        }
    }



}

