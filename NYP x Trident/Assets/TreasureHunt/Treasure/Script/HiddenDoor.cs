using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenDoor : MonoBehaviour
{
    [SerializeField] GameObject parentObj;   //�e���i�[�p
    public Image image;

    bool upFlag;
    Vector3 wallPos;

    // Start is called before the first frame update
    void Start()
    {
        upFlag = false;
        wallPos = parentObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //parentObj.transform.position = wallPos;
        //�㏸���ł͂Ȃ��ꍇ�͎��s 
        if (upFlag == true)
        {
            StartCoroutine("UpWall");
        }
    }

    //�ǂ̉�]
    IEnumerator UpWall()
    {
        for (int turn = 0; turn < 180; turn += 2)
        {
            parentObj.transform.position+=new Vector3(0, -0.03f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        upFlag = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            image.enabled = true;

            if (Input.GetKey(KeyCode.Space))
            {
                upFlag = true;
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

