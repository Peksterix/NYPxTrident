using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PlayerPos : NetworkBehaviour
{
    const float ROT = 3.0f;
    const int THREE = 3;
    const int COUNT = 30;

    int count;

    //��]�����ǂ���
    [SerializeField] public bool coroutineBool = false;
    bool rightFlag;
    bool leftFlag;
    //�ǂ���������Ă��邩
    [SerializeField] public int direction;//(0=�O,1=�E,2=���,3=��,)

    // Start is called before the first frame update
    void Start()
    {
        direction = 0;
        count = 0;
        leftFlag = false;
        rightFlag = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //��]���ł͂Ȃ��ꍇ�͎��s 
            if (!coroutineBool)
            {
                coroutineBool = true;
                leftFlag = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //��]���ł͂Ȃ��ꍇ�͎��s 
            if (!coroutineBool)
            {
                coroutineBool = true;
                rightFlag = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        if(coroutineBool)
        {
            if(leftFlag)
                CmdRotationLeft();
            else if(rightFlag)
                CmdRotationRight();
            count++;
            if (count >= COUNT)
            {
                coroutineBool = false;
                count = 0;
                if(leftFlag)
                {
                    leftFlag = false;
                    direction--;
                    if (direction < 0) direction = THREE;
                }
                if (rightFlag)
                {
                    rightFlag = false;
                    direction++;
                    if (direction > THREE) direction = 0;
                }
            }
        }
    }

    [Command(requiresAuthority = false)]
    void CmdRotationRight()
    {
        transform.Rotate(new Vector3(0, ROT, 0));
    }
    [Command(requiresAuthority = false)]
    void CmdRotationLeft()
    {
        transform.Rotate(new Vector3(0, -ROT, 0));
    }
}
