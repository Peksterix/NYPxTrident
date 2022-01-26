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

    //‰ñ“]’†‚©‚Ç‚¤‚©
    [SerializeField] public bool coroutineBool = false;
    bool rightFlag;
    bool leftFlag;
    //‚Ç‚¿‚ç‚ðŒü‚¢‚Ä‚¢‚é‚©
    [SerializeField] public int direction;//(0=‘O,1=‰E,2=Œã‚ë,3=¶,)

    public override void OnStartLocalPlayer()
    {
        direction = 0;
        count = 0;
        leftFlag = false;
        rightFlag = false;
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    direction = 0;
    //    count = 0;
    //    leftFlag = false;
    //    rightFlag = false;
    //}

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            //‰ñ“]’†‚Å‚Í‚È‚¢ê‡‚ÍŽÀs 
            if (!coroutineBool)
            {
                coroutineBool = true;
                leftFlag = true;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            //‰ñ“]’†‚Å‚Í‚È‚¢ê‡‚ÍŽÀs 
            if (!coroutineBool)
            {
                coroutineBool = true;
                rightFlag = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) 
            return;

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

    void CmdRotationRight()
    {
        transform.Rotate(new Vector3(0, ROT, 0));
    }

    void CmdRotationLeft()
    {
        transform.Rotate(new Vector3(0, -ROT, 0));
    }
}
