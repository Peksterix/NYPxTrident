//==============================================
//Day           :10/29
//Creator       :HashizumeAtsuki
//Description   :プレイヤーの状態管理
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerActions : NetworkBehaviour
{
    //最大体力
    [SerializeField] private int m_maxHp = 50;

    //体力
    [SyncVar(hook = nameof(SyncOnHpChange))]
    private int m_hp;

    //ダメージを受けてから回復するまでの時間の最大値
    [SerializeField] private int m_maxTimeToRecovery = 1800;

    //ダメージを受けてから回復するまでの時間(0で回復開始)
    private int m_timeToRecovery;

    //自動回復の間隔
    [SerializeField] private int m_automaticRecoveryInterval = 240;

    //自動回復量
    [SerializeField] private int m_healHp = 1;

    //気絶常態化の判定
    private bool m_isStunting;
    public bool isStunting => m_isStunting;

    //最大気絶時間
    [SerializeField] private float m_maxStuntingTime = 3.0f;

    //気絶時間
    private float m_stuntingTime;

    //追いかける人かどうか
    [SyncVar(hook = nameof(SyncOnIsChaserChange))]
    private bool m_isChase;
    public bool isChase => m_isChase;

    //ポイント
    public int m_point = 0;
    // Start is called before the first frame update

    #region
    void SyncOnHpChange(int oldVal, int newVal)
    {
        m_hp = newVal;

        if (!m_isChase)
        {
            this.GetComponent<MeshRenderer>().material.color = new Color(
                1.0f - (1.0f * ((float)m_maxHp - (float)m_hp) / m_maxHp),
                1.0f - (1.0f * ((float)m_maxHp - (float)m_hp) / m_maxHp),
                1.0f,
                1
                );
        }
    }

    void SyncOnIsChaserChange(bool oldVal, bool newVal)
    {
        m_isChase = newVal;
        this.GetComponent<MeshRenderer>().material.color = newVal ? Color.red : new Color(
                1.0f - (1.0f * ((float)m_maxHp - (float)m_hp) / m_maxHp),
                1.0f - (1.0f * ((float)m_maxHp - (float)m_hp) / m_maxHp),
                1.0f, 1);
    }
    #endregion

    public override void OnStartServer()
    {
        m_hp = m_maxHp;
        m_timeToRecovery = 0;
        m_stuntingTime = m_maxStuntingTime;
        m_isStunting = false;
        m_point = 0;
    }

    // Update is called once per frame
    void Update()
    {     
        if (!isServer) return;

        //気絶時間の処理
        if(m_isStunting)
        {
            UpdateStuntingTime();
            return;
        }

        //自動回復する
        if(
            m_hp< m_maxHp&&
            Time.frameCount% m_automaticRecoveryInterval == 0&&
            m_timeToRecovery <= 0&&
            !m_isChase)
        {
            m_hp += m_healHp;
        }

        //ダメージを受けてから回復するまでの時間を進める
        if (m_timeToRecovery > 0)
        {
            m_timeToRecovery--;
        }
    }

    //-------------------------------------
    //水が当たった処理　
    //Treatment of water hit
    //引数     :ダメージ量  Damage
    //戻り値   :なし　None
    //-------------------------------------
    [Server]
    public bool HitWater(int damage)
    {
        m_hp -= damage;
        m_timeToRecovery = m_maxTimeToRecovery;

        return m_hp <= 0;
    }

    //-------------------------------------
    //逃げる人になった時の処理　
    //What to do when you're the one running away
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    [Server]
    public void ChangeRunningAway()
    {
        m_hp = m_maxHp;
        m_isChase = false;
        m_isStunting = false;
        m_stuntingTime = m_maxStuntingTime;
    }

    //-------------------------------------
    //追いかける人になった時の処理　
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    [Server]
    public void ChangeChase()
    {
        m_hp = m_maxHp;
        m_isStunting = false;
        m_isChase = true;
        m_stuntingTime = m_maxStuntingTime;
    }

    //-------------------------------------
    //気絶時間の処理　
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    [Server]
    private void UpdateStuntingTime()
    {
        m_stuntingTime -= Time.deltaTime;


        this.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 1.0f, 0, 0.3f);

        if (m_stuntingTime <= 0)
        {
            m_isStunting = false;
            m_stuntingTime = m_maxStuntingTime;
        }
    }
}
