//==============================================
//Day           :10/29
//Creator       :HashizumeAtsuki
//Description   :水鉄砲のクラス
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    //ParticleSystem型を変数psで宣言します。
    private ParticleSystem m_ps;
    //GameObject型で変数objを宣言します。
    [SerializeField] public GameObject m_obj;
    //水鉄砲を撃っているかのフラグ
    private bool m_isShotWaterGun;

    //水の最大保有量
    [SerializeField] private int m_maxWaterGaugeNum = 2000;

    //水の保有量
    [SerializeField] private int m_waterGaugeNum;


    void Start()
    {
        //GetComponentInChildrenで子要素も含めた
        //ParticleSystemにアクセスして変数psで参照します。
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
    //水鉄砲発射　
    //Water pistol firing
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    public virtual void ShotWater()
    {
        //水がなければ水を打てないようにする
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

        //水鉄砲を撃っている間は水が減る
        if (m_isShotWaterGun)
        {
            m_waterGaugeNum--;
        }
    }

    //水の回復
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
