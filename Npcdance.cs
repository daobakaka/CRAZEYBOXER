using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Npcdance : NetworkBehaviour
{
    public Animator animator0;
    
    void Start()
    {
        StartCoroutine("SettimeIE");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SettimeIE()
    {
        WaitForSeconds loop = new WaitForSeconds(3);
        for (; ; )
        {
            yield return loop;
            animator0.SetFloat("Time", Random.Range(0, 1f));
        
        
        
        
        }
    
    
    
    
    
    
    }
}
