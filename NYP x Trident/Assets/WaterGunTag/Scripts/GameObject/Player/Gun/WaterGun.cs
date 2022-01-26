//==============================================
//Day           :10/29
//Creator       :HashizumeAtsuki
//Description   :水鉄砲のクラス
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WaterGun : MonoBehaviour
{
    //ParticleSystem型を変数psで宣言します。
    private ParticleSystem m_ps;
    //GameObject型で変数objを宣言します。
    [SerializeField] public GameObject m_obj;
    //水鉄砲を撃っているかのフラグ
    public bool m_isShotWaterGun;

    //水の最大保有量
    [SerializeField] public int m_maxWaterGaugeNum = 2000;

    //水の保有量
    [SerializeField] private int m_waterGaugeNum;

    //水の保有量
    [SerializeField] private int m_ShootWaterGaugeNum = 5;


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

        //if (this.gameObject.transform.root.GetComponent<WGTPlayerController>().
        //     m_wgtGameManager.GetComponent<WGTGameManager>().GetIsStopGame()||
        //     this.gameObject.transform.root.GetComponent<WGTPlayerController>().m_isInoperable ||
        //     this.gameObject.transform.root.GetComponent<PlayerActions>().GetIsStunting())
        if (WGTGameManager.GetCurrGameState() == WGTGameManager.GameState.Ended ||
             this.gameObject.transform.root.GetComponent<WGTPlayerController>().m_isInoperable ||
             this.gameObject.transform.root.GetComponent<PlayerActions>().isStunting)
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

    [Server]
    public bool ShootWaterGun(bool isShooting)
    {
        m_isShotWaterGun = isShooting;
        if (m_isShotWaterGun && m_waterGaugeNum >= 0)
        {
           
            GetPs().Play();
            m_waterGaugeNum-= m_ShootWaterGaugeNum;
        }
        else
        {
            
            m_isShotWaterGun = false;
            GetPs().Stop();
        }

        return m_isShotWaterGun;
    }

    [Client]
    public void FireWaterGunAnimation()
    {
        GetPs().Play();
    }

    [Client]
    public void HoldWaterGunAnimation()
    {
        GetPs().Stop();
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
