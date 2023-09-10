using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Actor : Actormove
{
    public float health, speed, power, healthrecover, stamina, staminarecover, powercoefficient;
    public bool Isman;

    public void Insme()
    {
        health = 100;
        speed = 3;
        power = 5;
        healthrecover = 2;
        stamina = 100;
        staminarecover = 2;
        powercoefficient = 1;
        Isman = true;

    }
}
