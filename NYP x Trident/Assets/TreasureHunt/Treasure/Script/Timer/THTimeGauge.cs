using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

//Gameplay countdown background

public class THTimeGauge : NetworkBehaviour
{
    //��������
    private GameObject m_time;

    [SyncVar]
    float currTime;

    [SyncVar]
    int maxTime;

    //���F�ɂȂ鎞��
    [SerializeField] private float m_yellowGaugeTime = 30.0f;

    //�ԐF�ɂȂ鎞��
    [SerializeField] private float m_redGaugeTime = 10.0f;

    

    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
            return;

        m_time = GameObject.Find("GameTimer");

        //�����̐�������
        maxTime = m_time.GetComponent<THGameTime>().GetMaxTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;

        //���݂̐�������
        currTime = m_time.GetComponent<THGameTime>().GetFloatTime();

        UpdateGauge();

        ChangeTimeGaugeColor();

    }

    [ClientRpc]
    void UpdateGauge()
    {
        GetComponent<Image>().fillAmount = (float)currTime / maxTime;
    }

    [ClientRpc]
    void ChangeTimeGaugeColor()
    {
        GetComponent<Image>().color = new Color(0, 255, 0);

        if (currTime <= m_yellowGaugeTime)
        {
            GetComponent<Image>().color = Color.yellow;
        }

        if (currTime <= m_redGaugeTime)
        {
            GetComponent<Image>().color = Color.red;
        }
    }
}
