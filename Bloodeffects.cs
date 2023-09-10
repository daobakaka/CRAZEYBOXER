using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bloodeffects : NetworkBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(Closeself());
    }
    IEnumerator Closeself()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
