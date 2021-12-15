using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HiddenDoor : NetworkBehaviour
{
    
    const int COUNT = 150;
    public bool upFlag;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        upFlag = false;
    }

    void FixedUpdate()
    {
        if (upFlag)
        {
            CmdMove();
            count++;
            if (count >= COUNT)
            {
                count = 0;
                upFlag = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                upFlag = true;
            }

        }
    }

    [Command(requiresAuthority =false)]
    void CmdMove()
    {
        //ç¿ïWÇèëÇ´ä∑Ç¶ÇÈ
        transform.position += new Vector3(0, -3, 0) * Time.deltaTime;
        
    }
}

