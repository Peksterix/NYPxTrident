//==============================================
//Day           :10/29
//Creator       :HashizumeAtsuki
//Description   :���S�C�̃N���X
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    //ParticleSystem�^��ϐ�ps�Ő錾���܂��B
    private ParticleSystem m_ps;
    //GameObject�^�ŕϐ�obj��錾���܂��B
    [SerializeField] public GameObject m_obj;
    //���S�C�������Ă��邩�̃t���O
    private bool m_isShotWaterGun;

    //���̍ő�ۗL��
    [SerializeField] private int m_maxWaterGaugeNum = 2000;

    //���ۗ̕L��
    [SerializeField] private int m_waterGaugeNum;


    void Start()
    {
        //GetComponentInChildren�Ŏq�v�f���܂߂�
        //ParticleSystem�ɃA�N�Z�X���ĕϐ�ps�ŎQ�Ƃ��܂��B
        m_ps = m_obj.GetComponentInChildren<ParticleSystem>();

        GetPs().Stop();
        m_isShotWaterGun = false;
        m_waterGaugeNum = m_maxWaterGaugeNum;

    }


    void Update()
    {

        if (this.gameObject.transform.root.GetComponent<WGTPlayerController>().
             m_time.GetComponent<GameTime>().GetIsFinish()|| 
             this.gameObject.transform.root.GetComponent<PlayerActions>().GetIsStunting())
        {
            GetPs().Stop();
            m_isShotWaterGun = false;
        }
    }

    //-------------------------------------
    //���S�C���ˁ@
    //Water pistol firing
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    public virtual void ShotWater()
    {
        //�����Ȃ���ΐ���łĂȂ��悤�ɂ���
        if (m_waterGaugeNum <= 0)
        {
            GetPs().Stop();
            m_isShotWaterGun = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GetPs().Play();
            m_isShotWaterGun = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            GetPs().Stop();
            m_isShotWaterGun = false;
        }

        //���S�C�������Ă���Ԃ͐�������
        if (m_isShotWaterGun)
        {
            m_waterGaugeNum--;
        }
    }

    //���̉�
    public void ChargeWaterGauge(int waterchargenum)
    {
        if (m_waterGaugeNum < m_maxWaterGaugeNum)
        {
            m_waterGaugeNum += waterchargenum;
        }
        if (m_waterGaugeNum > m_maxWaterGaugeNum)
        {
            m_waterGaugeNum = m_maxWaterGaugeNum;
        }
    }

    public bool GetIsShotWaterGun()
    {
        return m_isShotWaterGun;
    }


    public ParticleSystem GetPs()
    {
        return m_ps;
    }

    public int GetMaxWaterGaugeNum()
    {
        return m_maxWaterGaugeNum;
    }

    public int GetWaterGaugeNum()
    {
        return m_waterGaugeNum;
    }
}
