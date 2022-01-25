using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class THTimeGauge : NetworkBehaviour
{
    //制限時間
    private GameObject m_time;

    //黄色になる時間
    [SerializeField] private float m_yellowGaugeTime = 30.0f;

    //赤色になる時間
    [SerializeField] private float m_redGaugeTime = 10.0f;



    // Start is called before the first frame update
    void Start()
    {
        m_time = GameObject.Find("GameTimer");
    }

    // Update is called once per frame
    void Update()
    {
        //初期の制限時間
        int maxTime = m_time.GetComponent<THGameTime>().GetMaxTime();
        //現在の制限時間
        float nowTime = m_time.GetComponent<THGameTime>().GetFloatTime();

        GetComponent<Image>().fillAmount = (float)nowTime / maxTime;

        GetComponent<Image>().color = new Color(0,255,0);

        if (nowTime <= m_yellowGaugeTime)
        {
            GetComponent<Image>().color = Color.yellow;
        }

        if (nowTime <= m_redGaugeTime)
        {
            GetComponent<Image>().color = Color.red;
        }

    }
}
