using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Gamemanagertest : NetworkBehaviour

{
    // Start is called before the first frame update

    public GameObject[] objnum;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
      
    }
    void Polltest000()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Poll.Getinstance().Insgameobj("Prefeb/Cube000");

        }
        if (Input.GetMouseButtonDown(1))
        {
            Poll.Getinstance().Insgameobj("Prefeb/Sphere000");

        }

    }
}
