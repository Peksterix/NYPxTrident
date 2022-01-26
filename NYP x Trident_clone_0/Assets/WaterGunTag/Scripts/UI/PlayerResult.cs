//==============================================
//Day           :12/20
//Creator       :HashizumeAtsuki
//Description   :プレイヤーのリザルト
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResult : MonoBehaviour
{
    //順位テキスト
    [SerializeField] private GameObject m_rank;
    //プレイヤーナンバー
    [SerializeField] private GameObject m_playerNum;
    //ポイント
    [SerializeField] private GameObject m_pointNum;
    //ポイントゲージ
    [SerializeField] private GameObject m_pointGauge;

    // Start is called before the first frame update
    void Start()
    {
        m_rank.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-------------------------------------
    //データの受け取り
    //
    //引数     :順位 int rank,プレイヤーナンバー int playernum,ポイント int point
    //戻り値   :なし　None
    //-------------------------------------

    public void PlayerResultSetDate(int rank,int playernum,int point)
    {
        m_rank.GetComponent<Text>().text = rank.ToString() + "st";
        m_playerNum.GetComponent<Text>().text = playernum.ToString() + "P";
        m_pointGauge.GetComponent<ResultPointGauge>().SetPoint(point);
    }

}
