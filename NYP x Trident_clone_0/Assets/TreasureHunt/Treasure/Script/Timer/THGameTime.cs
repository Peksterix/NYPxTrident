using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class THGameTime : NetworkBehaviour
{
    // Start is called before the first frame update
    //制限時間の最大時間
    [SerializeField] private int m_maxTime = 180;

    //現在の制限時間
    [SyncVar] private int m_time;

    //1秒をとるための時間
    private float m_timeCount = 0;

    //終了したか
    private bool m_isFinish;

    //タイムゲージ用の時間
    private float m_floatTime;

    //最初の大きさ
    [SerializeField] private float m_startSize = 1.0f;

    //最終的な大きさ
    [SerializeField] private float m_finishSize = 0.1f;

    //larpの数字
    private float m_larpT = 0.0f;

    //演出を起こすフラグ
    private bool m_isAction = false;

    //カウントダウン情報格納用
    CountDown countDownScript;
    GameObject countDownText;

    void Start()
    {
        if (!isServer)
            return;

        countDownText = GameObject.Find("CountDownObject");
        countDownScript = countDownText.GetComponent<CountDown>();
        RestartTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;

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

        //下のif文で時間を進めていいか判断する
        if (countDownScript.countDownFlag)
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



        if (m_time > 0 && !m_isFinish && m_timeCount >= 1)
        {
            m_time--;
            m_timeCount = 0;
            m_isAction = true;
            m_larpT = 0.0f;
        }

        if (m_time <= 0 && !m_isFinish)
        {
           
            //時間切れの処理（ゲームを止める）
            //ここに記載
            m_isFinish = true;
        }

        float deltaTime = Time.deltaTime;

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
