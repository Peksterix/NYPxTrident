//==============================================
//Day           :10/29
//Creator       :HashizumeAtsuki
//Description   :�v���C���[�S�̂̊Ǘ�
//               
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //�v���C���[�̃��X�g
    [SerializeField] private List<GameObjectBase> m_playerList = new List<GameObjectBase>();

    //�v���C���[�̃��X�g�̒ǂ�������l�̔ԍ�
    private int m_chasePlayer;

    //�v���C���[�̐���ۑ�����
    private int m_isOnPlayer = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (m_playerList.Count == 0)
        {
            return;
        }

        //�v���C���[�̏�����
        for (int i = 0; i < m_playerList.Count; i++)
        {
            m_playerList[i].GetComponent<PlayerActions>().ChangeRunningAway();

        }
        //�ǂ�������l�̌���
        //m_playerList[Random.Range(0, m_playerList.Count)].GetComponent<PlayerActions>().ChangeChase();

        //�ǂ�������l�̔ԍ��̕ۑ�
        int count = 0;
        for (int i = 0; i < m_playerList.Count; i++)
        {
            if (m_playerList[i].GetComponent<PlayerActions>().GetIsChase())
            {
                m_chasePlayer = count;
            }
            count++;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerList.Count == 0)
        {
            return;
        }
        else
        {
            if (m_isOnPlayer != m_playerList.Count)
            {
                m_isOnPlayer = m_playerList.Count;
                Start();
            }

        }
        //�ǂ�������l���ς������A�ۑ�����ԍ���ύX����
        int count = 0;
        for (int i=0;i<m_playerList.Count;i++)
        {         
            if (m_playerList[i].GetComponent<PlayerActions>().GetIsChase())
            {
                if (i!=m_chasePlayer)
                {
                    m_playerList[m_chasePlayer].GetComponent<PlayerActions>().ChangeRunningAway();

                    m_chasePlayer = count;

                }
            }
            count++;

        }
    }

    public List<GameObjectBase> GetPlayerList()
    {
        return m_playerList;
    }
}
