using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controltime : Hit
{
    // Start is called before the first frame update
    private int helath, level, stamina;
    protected override string Ison()
    {
        return "kakaforever";
    }

    public bool Iseffects = false;
    public Controltime(int a,int b,int c)
    {
        helath = a;
        level = b;
        stamina = c;
        Debug.Log("构造传参数" + helath + ">>__" + level);
       
    
    }
}
