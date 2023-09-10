using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bloodexp : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        GameObject obj = GameObject.FindWithTag("Clonetree");
        transform.SetParent(obj.transform);
        transform.GetComponent<NetworkObject>().Spawn();
    }
}
