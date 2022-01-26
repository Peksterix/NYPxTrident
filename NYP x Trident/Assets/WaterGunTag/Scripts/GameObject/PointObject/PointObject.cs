//==============================================
//Day           :11/04
//Creator       :HashizumeAtsuki
//Description   :�|�C���g������A�C�e���i�I�j�̃N���X
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointObject : GameObjectBase
{
    //�ő�̗�
    [SerializeField] private int m_maxHp = 50;

    //�̗�
    private int m_hp;

    //�|�C���g��
    [SerializeField] private int m_point;

    //�j�󂳂ꂽ���̔���
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
    //�����������������@
    //Treatment of water hit
    //����     :�_���[�W��  Damage
    //�߂�l   :�Ȃ��@None
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
