using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Whether : NetworkBehaviour
{
    // Start is called before the first frame update

    public Light lightsun;
    public Light lightpoint;
    public float timespeed;
    public GameObject fog;
    void Start()
    {

        NetworkManager.Singleton.OnServerStarted += () =>
        {
           
            StartCoroutine(Sunrise());
            StartCoroutine(LightchangeIE());
            StartCoroutine(FogrotateIE());
            Debug.Log("开始光照");
        };
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Sunrise()
    {
        
        WaitForSeconds loop0 = new WaitForSeconds(timespeed);
        lightsun.transform.localEulerAngles = new Vector3(0, -30, 0);
        for (; ; )
        {
            yield return loop0;

            if (true)
            {
                // Debug.Log("变化"+ lightsun.transform.localEulerAngles+">>--"+ lightsun.transform.rotation);
                //lightsun.transform.localEulerAngles += new Vector3(2, 0, 0);
                lightsun.transform.rotation *= Quaternion.Euler(1, 0, 0);//用rotation 规避万向锁！！！

            }     
        }
    
    }
    IEnumerator LightchangeIE()
    {
       
        WaitForSeconds loop0 = new WaitForSeconds(10*timespeed);
      
        for (; ; )
        {
            yield return loop0;

            if (true)
            {

                lightpoint.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
                

            }
        }

    }
    IEnumerator FogrotateIE()
    {
        WaitForSeconds loop = new WaitForSeconds(2*Time.fixedDeltaTime);
        for (; ; )
        {
            yield return loop;
            fog.transform.rotation *= Quaternion.Euler(Random.Range(0,0.1f), Random.Range(0, 0.1f), Random.Range(0, 0.1f));
        
        }
    
    }
}
