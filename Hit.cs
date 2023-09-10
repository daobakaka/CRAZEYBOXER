using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Hit : Player
{
    // Start is called before the first frame update
    private string netID;
    private bool useblood = true;
    private Remotebulid remotebulid0;
    public List<AudioClip> musics;
    private AudioSource soundplayer;
    public override void OnNetworkSpawn()
    {
        Actormove actormove0 = new Actormove();
        netID = actormove0.netID.ToString();
        Controltime controltime0 = new Controltime(5,6,7);
        useblood = controltime0.Iseffects;
        Debug.Log(Ison());
        Debug.Log(controltime0.Ison());

        //
        Playermethod methodone = new Playermethod();
        methodone.Goswim();
        //
    }
    private void Start()
    {
        remotebulid0 = GameObject.FindWithTag("Remotebuild").GetComponent<Remotebulid>();
        foreach (var item in remotebulid0.Music0)
        {

            musics.Add(item.Value);

        }
        soundplayer = GameObject.FindWithTag("Soundplayer").GetComponent<AudioSource>();
    }
    protected virtual string Ison()
    {
        return "kaka";
    
    
    }
    private void OnTriggerEnter(Collider other)
    {
            if (other.tag.Equals("Player"))//受到攻击，以角色ID确定来自谁
            {
                if (IsClient && IsOwner && other.name != netID)
                {
                     Debug.Log("攻击");
                    if (useblood)
                    {
                        this.transform.GetChild(0).gameObject.SetActive(true);
                    }
                int i = Random.Range(0, 4);
                soundplayer.clip = musics[i];
                soundplayer.Play();
            }
            }    
    }
    public void Testone()
    {
        Debug.Log("测试");
    
    }
}
