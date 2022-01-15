using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenWall : MonoBehaviour
{
    [SerializeField] GameObject parentObj;   //�e���i�[�p
    [SerializeField] GameObject player;   //�v���C���[���i�[�p
    private Vector3 RotateAxis = Vector3.up;
    private float SpeedFactor = -1.5f;
    public Image image;

    bool rotFlag;
    //��]�����ǂ���
    [SerializeField] public bool coroutineBool;
    Vector3 wallPos;

    // Start is called before the first frame update
    void Start()
    {
        rotFlag = false;
        coroutineBool = false;
        wallPos = parentObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        parentObj.transform.position = wallPos;
        //��]���ł͂Ȃ��ꍇ�͎��s 
        if (rotFlag == true&& !coroutineBool)
        {
           
            coroutineBool = true;
            StartCoroutine("RotWall");
            
        }
    }

    //�ǂ̉�]
    IEnumerator RotWall()
    {
        for (int turn = 0; turn < 180; turn += 2)
        {
            parentObj.transform.Rotate(0, -2, 0);
            // �w��I�u�W�F�N�g�𒆐S�ɉ�]����
            player.transform.RotateAround(
                parentObj.transform.position,
                RotateAxis,
                360.0f / (1.0f / SpeedFactor) * Time.deltaTime
                );
            yield return new WaitForSeconds(0.01f);
        }
        coroutineBool = false;
        rotFlag = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            image.enabled = true;

            if (Input.GetKey(KeyCode.Space))
            {
                rotFlag = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            image.enabled = false;
        }
    }
}
