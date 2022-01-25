//==============================================
//Day           :12/14
//Creator       :HashizumeAtsuki
//Description   :スタート時のカウントダウン
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class StartCountDown : NetworkBehaviour
{
    //キャンバス
    [SerializeField] private GameObject m_canvas;

    //カウントダウンの数字の初期値
    [SerializeField] private int m_startCountDownNum = 4;

    //カウントダウンの数字
    [SyncVar] private int m_countDownNum;

    //1秒をとるための時間
    [SyncVar] private float m_timeCount = 0;

    //終了したか
    [SyncVar] private bool m_isFinish;

    //作り出すUIオブジェクト
    //[SerializeField] private List<GameObject> m_createUIObject = new List<GameObject>();

    // Instead of making UI objects a spawnable object, its easier to leave UI objects that will only be used once in the canvas. 
    // For networking, it won't be as heavy on the server as well.
    //作ったUIオブジェクト
    //private List<GameObject> m_createdUIObject = new List<GameObject>();

    [SerializeField] private GameObject m_CountdownUIObject;

    //ゲームマネージャー
    private GameObject m_wgtGameManager;

    //最初の大きさ
    [SerializeField] private float m_startSize = 10.0f;

    //最終的な大きさ
    [SerializeField] private float m_finishSize = 1.0f;

    //larpの数字
    [SyncVar] private float m_larpT = 0.0f;

    void Start()
    {
        m_wgtGameManager = GameObject.Find("WGTGameManager");
        //m_wgtGameManager.GetComponent<WGTGameManager>().SetIsStopGame(true);
        WGTGameManager.SetCurrGameState(WGTGameManager.GameState.Ended);

        if (!isServer)
            return;

        RestartTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isFinish || !isServer)
        {
            return;
        }

        if (m_wgtGameManager == null)
        {
            m_wgtGameManager = GameObject.Find("WGTGameManager");
        }

        if (m_wgtGameManager == null)
            return;

        UpdateTime();
        UpdateText();
    }

    //-------------------------------------
    //時間のリセット
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    public void RestartTime()
    {

        //背景の生成
        //GameObject textObject = Instantiate(m_createUIObject[0]);
        //textObject.transform.SetParent(m_canvas.transform, false);
        //m_createdUIObject.Add(textObject);

        //m_CountdownUIObject = Instantiate(m_createUIObject[0], m_canvas.transform);
        //NetworkServer.Spawn(m_CountdownUIObject);

        m_countDownNum = m_startCountDownNum;
        m_isFinish = false;

        m_timeCount = 0;
        m_larpT = 0.0f;
    }

    //-------------------------------------
    //時間を進める
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    [ClientRpc]
    private void UpdateTime()
    {
        if (m_CountdownUIObject == null)
            return;

        if (m_timeCount <= 0.5f)
        {
            m_larpT = m_timeCount * 2;
            // m_createdUIObject[0].GetComponent<RectTransform>().localScale = new Vector3(
            //Mathf.Lerp(m_startSize, m_finishSize, m_larpT),
            //Mathf.Lerp(m_startSize, m_finishSize, m_larpT),
            //Mathf.Lerp(m_startSize, m_finishSize, m_larpT));

            m_CountdownUIObject.GetComponent<RectTransform>().localScale = new Vector3(
           Mathf.Lerp(m_startSize, m_finishSize, m_larpT),
           Mathf.Lerp(m_startSize, m_finishSize, m_larpT),
           Mathf.Lerp(m_startSize, m_finishSize, m_larpT));
        }

        if (m_countDownNum >= 0 && m_timeCount >= 1)
        {
            m_countDownNum--;
            m_timeCount = 0;
        }

        if (m_countDownNum < 0 && !m_isFinish)
        {
            //for (int i = 0; i < m_createdUIObject.Count; i++)
            //{
            //    Destroy(m_createdUIObject[i]);

            //}
            //m_createdUIObject.Clear();

            m_CountdownUIObject.SetActive(false);
            //m_wgtGameManager.GetComponent<WGTGameManager>().SetIsStopGame(false);
            WGTGameManager.SetCurrGameState(WGTGameManager.GameState.Ongoing);
            m_isFinish = true;
        }

        if (m_countDownNum == 0)
        {
            //m_wgtGameManager.GetComponent<WGTGameManager>().SetIsStopGame(false);
            WGTGameManager.SetCurrGameState(WGTGameManager.GameState.Ongoing);
        }

        m_timeCount += Time.deltaTime;
    }

    //-------------------------------------
    //テキストの更新
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    [ClientRpc]
    private void UpdateText()
    {
        if (m_isFinish)
        {
            Destroy(m_CountdownUIObject);
            return;
        }

        if (m_CountdownUIObject == null)
            return;

        //Text timeText = m_createdUIObject[0].GetComponent<Text>();
        Text timeText = m_CountdownUIObject.GetComponent<Text>();
        if (m_countDownNum == 4)
        {

            timeText.text = "";
            return;

        }

        if (m_countDownNum == 0)
        {
            timeText.text = "START!";
        }
        else
        {
            timeText.text = m_countDownNum.ToString();
        }
    }

    //-------------------------------------
    //カウントダウンが終了したかの取得
    //
    //引数     :なし　None
    //戻り値   :カウントダウンが終了したかの判定　
    //-------------------------------------
    public bool GetIsFinish()
    {
        return m_isFinish;
    }
}