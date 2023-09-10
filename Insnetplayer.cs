using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Insnetplayer : NetworkBehaviour
{
    // Start is called before the first frame update
    private Remotebulid remotebulid0;
    void Start()
    {
        remotebulid0 = GameObject.FindWithTag("Remotebuild").GetComponent<Remotebulid>();

        Instantiate(remotebulid0.Player0["M1(Clone)"],this.transform);
        StartCoroutine(InsIE());
        //NetworkManager.Singleton.GetNetworkPrefabOverride(remotebulid0.Player0["M1"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InsIE()
    {
        yield return new WaitForSeconds(1);

        gameObject.GetComponent<Actormoveplayer>().enabled = true;
        Debug.Log("ÒÑ¸´ÖÆ");


    }
}

