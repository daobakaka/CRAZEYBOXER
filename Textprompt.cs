using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Textprompt : MonoBehaviour
{
    // Start is called before the first frame update
    public float looptime = 1;
    private void OnEnable()
    {
       
        StartCoroutine(TextIE());
    }
    IEnumerator TextIE()
    {
        this.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.8f);
        Debug.Log("¿ªÊ¼ÏÔÊ¾");
        WaitForSeconds loop = new WaitForSeconds(looptime* Time.deltaTime);
        for (float tc=0;tc<2 ; )
        {
            yield return loop;
            tc += Time.deltaTime;
            if (this.GetComponent<TextMeshProUGUI>().color.a > 0)
                this.GetComponent<TextMeshProUGUI>().color -= new Color(0, 0, 0, Time.deltaTime);
            else
                break;
        }
        this.gameObject.SetActive(false);
       
    
  
    
    }
}
