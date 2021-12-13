using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterGauge : MonoBehaviour
{

    //プレイヤーの水鉄砲
    private GameObject m_playerWaterGun;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの水の最大量
        int playerMaxWaterGauge = m_playerWaterGun.GetComponent<WaterGun>().GetMaxWaterGaugeNum();
        //プレイヤーの現在の水の量
        int playerWaterGauge = m_playerWaterGun.GetComponent<WaterGun>().GetWaterGaugeNum();

        GetComponent<Image>().fillAmount = (float)playerWaterGauge / playerMaxWaterGauge;
    }

    public void SetPlayerWaterGun(GameObject playerwatergun)
    {
        m_playerWaterGun = playerwatergun;
    }

}
