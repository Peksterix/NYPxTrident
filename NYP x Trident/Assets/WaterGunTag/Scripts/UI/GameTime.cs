//==============================================
//Day           :11/1
//Creator       :HashizumeAtsuki
//Description   :ゲームの制限時間
//               
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    // Start is called before the first frame update
    //制限時間の最大時間
    [SerializeField] private int m_maxTime = 60;

    //現在の制限時間
    private int m_time;

    //1秒をとるための時間
    private float m_timeCount = 0;

    //終了したか
    private bool m_isFinish;

    //タイムゲージ用の時間
    private float m_floatTime;

    //ゲームマネージャー
    private GameObject m_wgtGameManager;

    //最初の大きさ
    [SerializeField] private float m_startSize = 1.0f;

    //最終的な大きさ
    [SerializeField] private float m_finishSize = 0.1f;

    //larpの数字
    private float m_larpT = 0.0f;

    //演出を起こすフラグ
    private bool m_isAction = false;

    void Start()
    {
        m_wgtGameManager = GameObject.Find("WGTGameManager");
        RestartTime();
    }

    // Update is called once per frame
    void Update()
    {
        //ポイントが増えたときに起こすアクション
        if (m_isAction)
        {
            if (m_time % 10 == 0)
            {
                GetComponent<RectTransform>().localScale = new Vector3(
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 2)),
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 2)),
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 2))
              );

            }
            else if (m_time <= 10)
            {
                GetComponent<RectTransform>().localScale = new Vector3(
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 2)),
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 2)),
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 2))
              );

            }

            else
            {
                GetComponent<RectTransform>().localScale = new Vector3(
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 0.5f)),
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 0.5f)),
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 0.5f))
              );
            }
           

            if (m_larpT >= 2.0f)
            {
                m_isAction = false;
            }
            m_larpT += Time.deltaTime * 10.0f;
        }
       
        if (!m_wgtGameManager.GetComponent<WGTGameManager>().GetIsStopGame())
        {
            UpdeteTime();
        }
        UpdateText();

    }

    //-------------------------------------
    //制限時間のリセット
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    public void RestartTime()
    {
        m_time = m_maxTime;
        m_floatTime = m_maxTime;
        m_isFinish = false;

        m_timeCount = 0;
    }

    //-------------------------------------
    //制限時間を進める
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    private void UpdeteTime()
    {
        


        if (m_time>0&&!m_isFinish&& m_timeCount >=1)
        {
            m_time--;
            m_timeCount = 0;
            m_isAction = true;
            m_larpT = 0.0f;
        }
       
        if(m_time<=0&&!m_isFinish)
        {
            m_wgtGameManager.GetComponent<WGTGameManager>().SetIsStopGame(true);
            m_isFinish = true;
        }

        float deltaTime= Time.deltaTime;

        m_timeCount += deltaTime;
        m_floatTime -= deltaTime;
    }

    //-------------------------------------
    //テキストの更新
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    private void UpdateText()
    {
        Text timeText = this.GetComponent<Text>();
        timeText.text = m_time.ToString();
    }

    //-------------------------------------
    //ゲームが終了したかの取得
    //
    //引数     :なし　None
    //戻り値   :ゲームが終了したかの判定　
    //-------------------------------------
    public bool GetIsFinish()
    {
        return m_isFinish;
    }

    public int GetMaxTime()
    {
        return m_maxTime;
    }

    public int GetTime()
    {
        return m_time;
    }

    public float GetFloatTime()
    {
        return m_floatTime;
    }
}
