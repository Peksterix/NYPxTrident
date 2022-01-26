//==============================================
//Day           :12/06
//Creator       :HashizumeAtsuki
//Description   :水のチャージ場
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWater : MonoBehaviour
{
    //水の補給量
    [SerializeField] int m_waterChargeNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //当たり判定の処理
    private void OnTriggerStay(Collider other)
    {
        //プレイヤーに当たったら水の回復をできるようにする
        if (other.transform.CompareTag("Player"))
        {
            if (!other.gameObject.GetComponent<WGTPlayerController>().m_isInoperable)
            {
                if (!other.gameObject.GetComponentInChildren<WaterGun>().GetIsShotWaterGun())
                {

                    if (Input.GetMouseButtonUp(1))
                    {
                        GetComponent<AudioSource>().Stop();
                    }
                    //for Audio
                    else if (Input.GetMouseButtonDown(1))
                    {
                        GetComponent<AudioSource>().Play();
                        other.gameObject.GetComponentInChildren<WaterGun>().ChargeWaterGauge(m_waterChargeNum);
                    }
                    else if (Input.GetMouseButton(1))
                    {

                        other.gameObject.GetComponentInChildren<WaterGun>().ChargeWaterGauge(m_waterChargeNum);
                    }
                   



                }

            }

        }
    }

}
