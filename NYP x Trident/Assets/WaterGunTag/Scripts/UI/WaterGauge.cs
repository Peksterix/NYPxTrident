using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterGauge : MonoBehaviour
{

    //�v���C���[�̐��S�C
    private GameObject m_playerWaterGun;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�̐��̍ő��
        int playerMaxWaterGauge = m_playerWaterGun.GetComponent<WaterGun>().GetMaxWaterGaugeNum();
        //�v���C���[�̌��݂̐��̗�
        int playerWaterGauge = m_playerWaterGun.GetComponent<WaterGun>().GetWaterGaugeNum();

        GetComponent<Image>().fillAmount = (float)playerWaterGauge / playerMaxWaterGauge;
    }

    public void SetPlayerWaterGun(GameObject playerwatergun)
    {
        m_playerWaterGun = playerwatergun;
    }

}
