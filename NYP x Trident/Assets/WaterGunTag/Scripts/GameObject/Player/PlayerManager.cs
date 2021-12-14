//==============================================
//Day           :10/29
//Creator       :HashizumeAtsuki
//Description   :プレイヤー全体の管理
//               
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //プレイヤーのリスト
    [SerializeField] private List<GameObjectBase> m_playerList = new List<GameObjectBase>();

    //プレイヤーのリストの追いかける人の番号
    private int m_chasePlayer;

    //プレイヤーの数を保存する
    private int m_isOnPlayer = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (m_playerList.Count == 0)
        {
            return;
        }

        //プレイヤーの初期化
        for (int i = 0; i < m_playerList.Count; i++)
        {
            m_playerList[i].GetComponent<PlayerActions>().ChangeRunningAway();

        }
        //追いかける人の決定
        //m_playerList[Random.Range(0, m_playerList.Count)].GetComponent<PlayerActions>().ChangeChase();

        //追いかける人の番号の保存
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
        //追いかける人が変わったら、保存する番号を変更する
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
