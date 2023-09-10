using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniy.KAKA;

public class Playerone : Playermethod, Isinterface
{
    public bool Isinterface { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

   public void Dosomething()
    {

        Isinterface = true;
        Debug.Log(Isinterface);
        isabstractson = true;
        bool k = isabstractson;
        Debug.Log(k);
    
    
    }
}
 

