//==============================================
//Day           :12/06
//Creator       :HashizumeAtsuki
//Description   :���̃`���[�W��
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWater : MonoBehaviour
{
    //���̕⋋��
    [SerializeField] int m_waterChargeNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�����蔻��̏���
    private void OnTriggerStay(Collider other)
    {
        //�v���C���[�ɓ��������琅�̉񕜂��ł���悤�ɂ���
        if (other.transform.CompareTag("Player"))
        {
            if (!other.gameObject.GetComponent<WGTPlayerController>().m_isInoperable)
            {
                if (Input.GetMouseButton(1))
                {
                    if (!other.gameObject.GetComponentInChildren<WaterGun>().GetIsShotWaterGun())
                    {
                        other.gameObject.GetComponentInChildren<WaterGun>().ChargeWaterGauge(m_waterChargeNum);
                    }
                }

               
                   
            }

        }
    }

}
