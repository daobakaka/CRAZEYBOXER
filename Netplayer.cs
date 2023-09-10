using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Netplayer : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 3;
    private float s;
    void Start()
    {
        Debug.Log("已添加试用脚本");
    }

    // Update is called once per frame
    void Update()
    {
        s = Input.GetAxis("Vertical");
        //  h = Input.GetAxis("Horizontal");
        if (s >= 0)
        {
            transform.Translate(0, 0, s * speed * Time.deltaTime, Space.Self);

        }
        else
        { transform.Translate(0, 0, s * 0.4f * speed * Time.deltaTime, Space.Self); }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localEulerAngles += new Vector3(0, speed * 10 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localEulerAngles += new Vector3(0, -speed * 10 * Time.deltaTime, 0);

        }
    }
}
