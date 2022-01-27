//==============================================
//Day           :12/13
//Creator       :HashizumeAtsuki
//Description   :ポイント表示UI
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointUI : MonoBehaviour
{
    //ポイントを表示するプレイヤー
    private GameObject m_player;

    //得点が変わったことを判断する変数
    private int m_playerBeforePoint;

    //最初の大きさ
    [SerializeField] private float m_startSize = 1.0f;

    //最終的な大きさ
    [SerializeField] private float m_finishSize = 1.5f;

    //larpの数字
    private float m_larpT = 0.0f;

    //演出を起こすフラグ
    private bool m_isAction = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ポイントが増えたときに起こすアクション
        if(m_isAction)
        {
            GetComponent<RectTransform>().localScale = new Vector3(
               m_startSize * (m_finishSize * (1 + (Mathf.Cos(m_larpT) * 0.5f))),
               m_startSize * (m_finishSize * (1 + (Mathf.Cos(m_larpT) * 0.5f))),
               m_startSize * (m_finishSize * (1 + (Mathf.Cos(m_larpT) * 0.5f)))
               );

            if (m_larpT >= 2.0f)
            {
                m_isAction = false;
            }
            m_larpT += Time.deltaTime*20.0f;
        }
       
        if(m_playerBeforePoint!= m_player.GetComponent<PlayerActions>().m_point)
        {
            GetComponent<AudioSource>().Play();
            m_isAction = true;
            m_larpT = 0.0f;
        }

        m_playerBeforePoint = m_player.GetComponent<PlayerActions>().m_point;
        Text pointText = this.GetComponent<Text>();
        pointText.text = m_player.GetComponent<PlayerActions>().m_point.ToString();

    }

    public void SetPlayer(GameObject player)
    {
        m_player = player;
    }
}
