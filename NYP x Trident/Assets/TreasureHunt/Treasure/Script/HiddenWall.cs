using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class HiddenWall : NetworkBehaviour
{

    public int count;

    GameObject player; //�v���C���[���i�[�p
    private Vector3 RotateAxis = Vector3.up;
    private float SpeedFactor = -1.5f;
    //��]�����ǂ���
    public bool rotFlag;

    // Start is called before the first frame update
    void Start()
    {
        rotFlag = false;
        count = 0;
    }

    private void FixedUpdate()
    {
        //��]���ł͂Ȃ��ꍇ�͎��s 
        if (rotFlag)
        {
            CmdMove();
            count++;
            if (count >= 30)
            {
                rotFlag = false;
                count = 0;
            }
        }
    }

    [Command(requiresAuthority = false)]
    void CmdMove()
    {
        transform.Rotate(new Vector3 (0, -6.0f, 0));
        // �w��I�u�W�F�N�g�𒆐S�ɉ�]����
        //player.transform.RotateAround(transform.position,RotateAxis,360.0f / (1.0f / SpeedFactor) * Time.deltaTime);
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            player = other.gameObject;

            if (Input.GetKey(KeyCode.Space))
            {
                rotFlag = true;
            }

        }
    }

   
}
