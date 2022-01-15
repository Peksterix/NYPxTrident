using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPos : MonoBehaviour
{
    [SerializeField] GameObject player;   //�v���C���[���i�[�p
    [SerializeField] new GameObject camera;   //�J�������i�[�p
    PlayerCon playerScript;
    

    //��]�����ǂ���
    [SerializeField] public bool coroutineBool = false;
    //�ǂ���������Ă��邩
    [SerializeField] public int direction;//(0=�O,1=�E,2=���,3=��,)

    Vector3 targetPosition;
    Vector3 targetPositionY;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position;
        playerScript = player.GetComponent<PlayerCon>();
        direction = 0;

        targetPosition = camera.transform.position;
        targetPositionY = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�̃|�W�V�����ɓ���
        transform.position = player.transform.position;
        targetPosition = camera.transform.position;
        camera.transform.position = new Vector3(targetPosition.x, targetPositionY.y, targetPosition.z);
        playerScript.SetPlayerRot(direction);
      
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //��]���ł͂Ȃ��ꍇ�͎��s 
            if (!coroutineBool)
            {
                coroutineBool = true;
                StartCoroutine("RightMove");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //��]���ł͂Ȃ��ꍇ�͎��s 
            if (!coroutineBool)
            {
                coroutineBool = true;
                StartCoroutine("LeftMove");
            }
        }
       
        if (playerScript.hallFlag == true)
        {
            camera.transform.position = new Vector3(targetPosition.x, targetPositionY.y-1, targetPosition.z);
        }

    }

    IEnumerator RightMove()
    {
        for (int turn = 0; turn < 90; turn+=2)
        {
            transform.Rotate(0, 2, 0);
            yield return new WaitForSeconds(0.01f);
        }
        coroutineBool = false;
        playerScript.SetPlayerDir();
        direction++;
        if (direction > 3) direction = 0;
    }

    //���ɂ�������]����90���ŃX�g�b�v
    IEnumerator LeftMove()
    {
        for (int turn = 0; turn < 90; turn+=2)
        {
            transform.Rotate(0, -2, 0);
            yield return new WaitForSeconds(0.01f);
        }
        coroutineBool = false;
        playerScript.SetPlayerDir();
        direction--;
        if (direction < 0) direction = 3;
    }
}
