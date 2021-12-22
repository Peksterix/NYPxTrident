//==============================================
//Day           :12/15
//Creator       :HashizumeAtsuki
//Description   :リザルト時のポイントゲージ
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPointGauge : MonoBehaviour
{

    //このゲームの限界スコア
    [SerializeField] int m_highScore = 100;

    //ゲージの増える演出時間
    private float m_upGauge = 0.0f;

    //ポイントテキスト
    [SerializeField] private GameObject m_resultPointText;

    //順位テキスト
    [SerializeField] private GameObject m_rank;

    //ポイント
    private int m_point;

    //数字を整数にする
    private int m_intUpGauge;

    //順位表示が終わったかの判定フラグ
    private bool m_isRankFlag;

    // Start is called before the first frame update
    void Start()
    {
        m_upGauge = 0.0f;
        m_intUpGauge = 0;
        m_isRankFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        GetComponent<Image>().fillAmount = m_upGauge / m_highScore;
        //テキストのポジション計算
        float y = (GetComponent<RectTransform>().sizeDelta.y * GetComponent<Image>().fillAmount) - 250;

        if (m_upGauge <= m_point)
        {
            m_upGauge += Time.deltaTime * 20;
        }
        else
        {
            m_upGauge = m_point;
            m_rank.SetActive(true);
            m_rank.SetActive(true);
            m_rank.GetComponent<RectTransform>().localPosition = new Vector3(0, y + 200, 0);

            m_isRankFlag = true;
        }

        m_intUpGauge = (int)m_upGauge;

        m_resultPointText.GetComponent<Text>().text = m_intUpGauge.ToString();

      
        m_resultPointText.GetComponent<RectTransform>().localPosition = new Vector3(0, y + 100, 0);

        
    }

    public void SetPoint(int point)
    {
        m_point = point;
    }

    public bool GetIsRankFlag()
    {
        return m_isRankFlag;
    }

}
