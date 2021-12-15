//==============================================
//Day           :12/14
//Creator       :HashizumeAtsuki
//Description   :�^�C���Q�[�W
//               
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeGaugeUI : MonoBehaviour
{

    //��������
    private GameObject m_time;

    //���F�ɂȂ鎞��
    [SerializeField] private float m_yellowGaugeTime = 30.0f;

    //�ԐF�ɂȂ鎞��
    [SerializeField] private float m_redGaugeTime = 10.0f;



    // Start is called before the first frame update
    void Start()
    {
        m_time= GameObject.Find("Time");
    }

    // Update is called once per frame
    void Update()
    {
        //�����̐�������
        int maxTime = m_time.GetComponent<GameTime>().GetMaxTime();
        //���݂̐�������
        float nowTime = m_time.GetComponent<GameTime>().GetFloatTime();

        GetComponent<Image>().fillAmount = (float)nowTime / maxTime;

        if (nowTime <= m_yellowGaugeTime)
        {
            GetComponent<Image>().color = Color.yellow;
        }

        if (nowTime<= m_redGaugeTime)
        {
            GetComponent<Image>().color = Color.red;
        }
       
    }
}
