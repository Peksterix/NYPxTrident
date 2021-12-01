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

    void Start()
    {
        RestartTime();
    }

    // Update is called once per frame
    void Update()
    {
        UpdeteTime();
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
        }
       
        if(m_time<=0&&!m_isFinish)
        {
            m_isFinish = true;
        }

        m_timeCount+=Time.deltaTime;
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


}
