//==============================================
//Day           :10/29
//Creator       :HashizumeAtsuki
//Description   :�v���C���[�̏�ԊǗ�
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    //�ő�̗�
    [SerializeField] private int m_maxHp = 50;

    //�̗�
    private int m_hp;

    //�_���[�W���󂯂Ă���񕜂���܂ł̎��Ԃ̍ő�l
    [SerializeField] private int m_maxTimeToRecovery = 1800;

    //�_���[�W���󂯂Ă���񕜂���܂ł̎���(0�ŉ񕜊J�n)
    private int m_timeToRecovery;

    //�����񕜂̊Ԋu
    [SerializeField] private int m_automaticRecoveryInterval = 240;

    //�����񕜗�
    [SerializeField] private int m_healHp = 1;

    //�C���ԉ��̔���
    private bool m_isStunting;

    //�ő�C�⎞��
    [SerializeField] private float m_maxStuntingTime = 3.0f;

    //�C�⎞��
    private float m_stuntingTime;

    //�ǂ�������l���ǂ���
    private bool m_isChase;

    //�|�C���g
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
        //�C�⎞�Ԃ̏���
        if(m_isStunting)
        {
            UpdateStuntingTime();
            return;
        }

        //HP��0�ȉ��Ȃ�S�ɂȂ�
        if(m_hp<=0&&!m_isChase)
        {
            ChangeChase();
            m_isStunting = true;
        }

        //�S�ɂȂ�����F��ς���
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

        //�����񕜂���
        if(
            m_hp< m_maxHp&&
            Time.frameCount% m_automaticRecoveryInterval == 0&&
            m_timeToRecovery <= 0&&
            !m_isChase)
        {
            m_hp += m_healHp;
        }

        //�_���[�W���󂯂Ă���񕜂���܂ł̎��Ԃ�i�߂�
        if (m_timeToRecovery>0)
        {
            m_timeToRecovery--;
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
       
        m_timeToRecovery = m_maxTimeToRecovery;
    }

    //-------------------------------------
    //������l�ɂȂ������̏����@
    //What to do when you're the one running away
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    public void ChangeRunningAway()
    {
        m_hp = m_maxHp;
        m_isChase = false;
        m_isStunting = false;
        m_stuntingTime = m_maxStuntingTime;
    }

    //-------------------------------------
    //�ǂ�������l�ɂȂ������̏����@
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
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
    //�C�⎞�Ԃ̏����@
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
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
