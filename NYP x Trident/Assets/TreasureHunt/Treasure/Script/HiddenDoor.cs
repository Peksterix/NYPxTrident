using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HiddenDoor : NetworkBehaviour
{
    const int COUNT = 130;
    public bool upFlag;
    int count;
    GameObject player;
    PlayerCon playerScript;


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
                player = other.gameObject;
                playerScript = player.GetComponent<PlayerCon>();
                upFlag = true;
            }
        }
    }

    [Command(requiresAuthority = false)]
    void CmdMove()
    {
        //座標を書き換える
        transform.position += new Vector3(0, -3, 0) * Time.deltaTime;

    }
}

