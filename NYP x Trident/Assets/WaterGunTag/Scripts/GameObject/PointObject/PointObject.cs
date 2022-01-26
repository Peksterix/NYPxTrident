//==============================================
//Day           :11/04
//Creator       :HashizumeAtsuki
//Description   :ポイントが入るアイテム（的）のクラス
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointObject : GameObjectBase
{
    //最大体力
    [SerializeField] private int m_maxHp = 50;

    //体力
    private int m_hp;

    //ポイント数
    [SerializeField] private int m_point;

    //破壊されたかの判定
    private bool m_isDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        m_hp = m_maxHp;
        m_isDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<MeshRenderer>().material.color = new Color(
              1.0f - (1.0f * ((float)m_maxHp - (float)m_hp) / m_maxHp),
              1.0f - (1.0f * ((float)m_maxHp - (float)m_hp) / m_maxHp),
              1.0f,
              1
              );

        if(m_isDestroy)
        {
           
            Destroy(this.gameObject);
           
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

        if (m_hp <= 0)
        {
            m_isDestroy = true;
           
        }
    }

    public bool GetIsDestroy()
    {
        return m_isDestroy;
    }

    public int GetPoint()
    {
        return m_point;
    }


}
