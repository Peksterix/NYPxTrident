using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Lance : NetworkBehaviour
{
    const int COUNT = 180;
    const int DIFFERENCE = 30;
    const float OPERATION_SPEED = 0.5f;
    const float BACK_SPEED = OPERATION_SPEED/ DIFFERENCE;
    const int WAIT_TIME = 30;

    bool operationFlag;
    bool backFlag;
    bool waitFlag;
    int count;
    int waitTimer;
    Vector3 LancePos;

    // Start is called before the first frame update
    void Start()
    {
        operationFlag = false;
        backFlag = false;
        waitFlag = false;
        waitTimer = 0;
        LancePos =transform.position;
    }

    private void FixedUpdate()
    {
        if (operationFlag)
        {
            CmdMove();
            count+= DIFFERENCE;
            if (count >= COUNT)
            {
                operationFlag = false;
                waitFlag = true;
            }
        }
        else if(waitFlag)
        {
            waitTimer++;
            if(waitTimer>=WAIT_TIME)
            {
                waitFlag = false;
                waitTimer = 0;
                backFlag = true;
            }
        }
        else if (backFlag)
        {
            CmdMove();
            count--;
            if (count <= 0)
            {
                backFlag = false;
                count = 0;
                transform.position = LancePos;
            }
        }
    }

    [Command(requiresAuthority = false)]
    void CmdMove()
    {
        //À•W‚ð‘‚«Š·‚¦‚é
        if (operationFlag)
            transform.position += new Vector3(OPERATION_SPEED, 0, 0);
        else if(backFlag)
            transform.position += new Vector3(-BACK_SPEED, 0, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && backFlag==false)
        {
            operationFlag = true;
        }
    }
}
