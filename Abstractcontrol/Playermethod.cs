using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class  Playermethod : Abstractcontrol
{

    public bool isabstractson { set; get; }


    public override bool Fuckingtime()
    {
        throw new System.NotImplementedException();
    }

    public override void Goswim()
    {
        Debug.Log("抽象方法重写,开始游泳");
    }

    public override bool Isplay()
    {
        return true;
    }


}
