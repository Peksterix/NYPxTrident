//==============================================
//Day           :10/29
//Creator       :HashizumeAtsuki
//Description   :プレイヤーの状態管理
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    //最大体力
    [SerializeField] private int m_maxHp = 50;

    //体力
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

    //最大気絶時間
    [SerializeField] private float m_maxStuntingTime = 3.0f;

    //気絶時間
    private float m_stuntingTime;

    //追いかける人かどうか
    private bool m_isChase;

    //ポイント
     public int m_point = 0;
    // Start is called before the first frame update
    void Start()
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
        //気絶時間の処理
        if(m_isStunting)
        {
            UpdateStuntingTime();
            return;
        }

        //HPが0以下なら鬼になる
        if(m_hp<=0&&!m_isChase)
        {
            ChangeChase();
            m_isStunting = true;
        }

        //鬼になったら色を変える
        if(m_isChase)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material.color = new Color(
                1.0f - (1.0f * ((float)m_maxHp - (float)m_hp) / m_maxHp),
                1.0f - (1.0f * ((float)m_maxHp - (float)m_hp) / m_maxHp),
                1.0f,
                1
                );
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
        if (m_timeToRecovery>0)
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
    public void HitWater(int damage)
    {
        m_hp -= damage;
       
        m_timeToRecovery = m_maxTimeToRecovery;
    }

    //-------------------------------------
    //逃げる人になった時の処理　
    //What to do when you're the one running away
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
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
    public void ChangeChase()
    {
        m_hp = m_maxHp;
        m_isChase = true;
        this.GetComponent<MeshRenderer>().material.color = new Color(
              1.0f - (1.0f * ((float)m_maxHp - (float)m_hp) / m_maxHp),
              1.0f - (1.0f * ((float)m_maxHp - (float)m_hp) / m_maxHp),
              1.0f,
              1
              );
    }

    //-------------------------------------
    //気絶時間の処理　
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    private void UpdateStuntingTime()
    {
        m_stuntingTime-=Time.deltaTime;


        this.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 1.0f, 0, 0.3f);

        if (m_stuntingTime<=0)
        {
            m_isStunting = false;
            m_stuntingTime = m_maxStuntingTime;
        }
    }


    public int GetHp()
    {
        return m_hp;
    }
    
    public void SetIsChase(bool ischase)
    {
        m_isChase = ischase;
    }

    public bool GetIsChase()
    {
        return m_isChase;
    }

    public bool GetIsStunting()
    {
        return m_isStunting;
    }


}
