//==============================================
//Day           :12/07
//Creator       :HashizumeAtsuki
//Description   :ポーズUI
//               
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    //PauseUIの状態
    enum PauseUIState
    {
        None=0,
        Start,
        Update,
        End,
    }

    //キャンバス
    [SerializeField] private GameObject m_canvas;

    //時間
    [SerializeField] private GameObject m_time;

    //プレイヤー
    private GameObject m_player;

    //作り出すUIオブジェクト
    [SerializeField] private List<GameObject> m_createUIObject=new List<GameObject>();

    //作ったUIオブジェクト
    private List<GameObject> m_createdUIObject = new List<GameObject>();

    //PauseUIの状態
    private PauseUIState m_pauseUIState;

    // Start is called before the first frame update
    void Start()
    {
        m_pauseUIState = PauseUIState.None;
    }

    // Update is called once per frame
    void Update()
    {
        //時間切れのときポーズ画面を開いていたら終了する
        if(m_time.GetComponent<GameTime>().GetIsFinish()&& m_pauseUIState != PauseUIState.None)
        {
            m_pauseUIState = PauseUIState.End;
        }

        switch (m_pauseUIState)
        {
            case PauseUIState.None:
               
               
                break;
            case PauseUIState.Start:
                //背景の生成
                GameObject backScreen = Instantiate(m_createUIObject[0]);
                 backScreen.transform.SetParent(m_canvas.transform,false);

                m_createdUIObject.Add(backScreen);
                //全体マップの生成
                GameObject bigMap = Instantiate(m_createUIObject[1]);
                bigMap.transform.SetParent(m_canvas.transform, false);

                m_createdUIObject.Add(bigMap);
                m_pauseUIState = PauseUIState.Update;
                break;
            case PauseUIState.Update:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    m_pauseUIState = PauseUIState.End;
                }
                break;
            case PauseUIState.End:
                for (int i = 0; i < m_createdUIObject.Count; i++)
                {
                    Destroy(m_createdUIObject[i]);
                   
                }
                m_createdUIObject.Clear();
                m_player.GetComponent<WGTPlayerController>().m_isInoperable = false;
                m_pauseUIState = PauseUIState.None;
                break;

        }


      
    }

    //-------------------------------------
    //キー入力によるポーズ画面の表示
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    public void InputKeyPause()
    {
        if(m_pauseUIState!=PauseUIState.None)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_pauseUIState = PauseUIState.Start;
            m_player.GetComponent<WGTPlayerController>().m_isInoperable = true;
            m_player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


    public void SetPlayer(GameObject player)
    {
        m_player = player;
    }

}
