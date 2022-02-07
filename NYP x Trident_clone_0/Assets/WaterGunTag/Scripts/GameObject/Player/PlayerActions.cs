//==============================================
//Day           :10/29
//Creator       :HashizumeAtsuki
//Description   :�v���C���[�̏�ԊǗ�
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerActions : NetworkBehaviour
{
    //�ő�̗�
    [SerializeField] private int m_maxHp = 50;

    //�̗�
    [SyncVar(hook = nameof(SyncOnHpChange))]
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
    public bool isStunting => m_isStunting;

    //�ő�C�⎞��
    [SerializeField] private float m_maxStuntingTime = 3.0f;

    //�C�⎞��
    private float m_stuntingTime;

    //�ǂ�������l���ǂ���
    [SyncVar(hook = nameof(SyncOnIsChaserChange))]
    private bool m_isChase;
    public bool isChase => m_isChase;

    //�|�C���g
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

        //�C�⎞�Ԃ̏���
        if(m_isStunting)
        {
            UpdateStuntingTime();
            return;
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
        if (m_timeToRecovery > 0)
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
    [Server]
    public bool HitWater(int damage)
    {
        m_hp -= damage;
        m_timeToRecovery = m_maxTimeToRecovery;

        return m_hp <= 0;
    }

    //-------------------------------------
    //������l�ɂȂ������̏����@
    //What to do when you're the one running away
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
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
    //�ǂ�������l�ɂȂ������̏����@
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
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
    //�C�⎞�Ԃ̏����@
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
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
