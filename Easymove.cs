using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Easymove : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float speed = 5;
    private void OnEnable()
    {
        StartCoroutine("DesnetIE");
    }
    IEnumerator DesnetIE()
    {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.tag = "Skillball";
        yield return new WaitForSeconds(10);

        DesnetServerRpc();
    
    
    }
    private void Update()
    {
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime,Space.Self);
    }


    [ServerRpc(RequireOwnership =false)]
    public void DesnetServerRpc()
    {
        this.GetComponent<NetworkObject>().Despawn();
      //  Destroy(gameObject);
    
    }
}
