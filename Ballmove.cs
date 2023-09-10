using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballmove :Gameobjset
{
    // Start is called before the first frame update
    // Update is called once per frame
   public  string objname;
 
    private void OnEnable()
    {
        
        StartCoroutine(SetfalseIE());
        //SetNettrueServerRpc(true);
        Setactiveobj(true);

    }
    public override void OnNetworkSpawn()
    {

        objactive.OnValueChanged += (prev, newv) =>
        {

            this.gameObject.SetActive(newv);


            Debug.Log("父级监听,更改值");
        };
        this.gameObject.SetActive(objactive.Value);
    }
    //public override void OnNetworkSpawn()
    //{
    //    Debug.Log("子级监听");
    //}


    void Update()
    {
        this.transform.Translate(transform.forward * 0.5f * Time.deltaTime);
    }
    IEnumerator SetfalseIE()
    {
        Debug.Log("开始失活协程");
      
        yield return new WaitForSeconds(6);
        this.NetSetactiveobj(false);
      
   //Netpool.Getinstance().NetPushobject( this.gameObject);

    }
}
