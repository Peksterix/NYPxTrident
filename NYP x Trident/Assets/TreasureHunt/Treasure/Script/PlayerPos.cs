using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPos : MonoBehaviour
{
    [SerializeField] GameObject player;   //プレイヤー情報格納用
    [SerializeField] new GameObject camera;   //カメラ情報格納用
    PlayerCon playerScript;
    

    //回転中かどうか
    [SerializeField] public bool coroutineBool = false;
    //どちらを向いているか
    [SerializeField] public int direction;//(0=前,1=右,2=後ろ,3=左,)

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
        //プレイヤーのポジションに同期
        transform.position = player.transform.position;
        targetPosition = camera.transform.position;
        camera.transform.position = new Vector3(targetPosition.x, targetPositionY.y, targetPosition.z);
        playerScript.SetPlayerRot(direction);
      
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //回転中ではない場合は実行 
            if (!coroutineBool)
            {
                coroutineBool = true;
                StartCoroutine("RightMove");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //回転中ではない場合は実行 
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

    //左にゆっくり回転して90°でストップ
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
