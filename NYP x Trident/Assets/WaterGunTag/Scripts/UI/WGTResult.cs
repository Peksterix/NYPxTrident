//==============================================
//Day           :12/15
//Creator       :HashizumeAtsuki
//Description   :リザルト表示
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WGTResult : MonoBehaviour
{
    
    //ポイントソートのために使う構造体
    private struct PlayerPoint
    {
        public int point;
        public int playerNum;
        public int rank;
    };

    //キャンバス
    [SerializeField] private GameObject m_canvas;

    //作り出すUIオブジェクト
    [SerializeField] private List<GameObject> m_createUIObject = new List<GameObject>();

    //作ったUIオブジェクト
    private List<GameObject> m_createdUIObject = new List<GameObject>();

    //プレイヤーマネージャー
    [SerializeField] private GameObject m_playerManager;
    //制限時間
    [SerializeField] public GameObject m_time;

    //終了表示からカウントするタイマー
    private float m_finishTimer = 0.0f;

    //結果表示に変える時間
    [SerializeField] private float m_drawWinnerTime = 2.0f;

    //スペースキーでタイトルに戻れるようになる時間
    [SerializeField] private float m_returnTitleTime = 2.0f;

    //勝者の名前
    private string m_winnerName;

    //スペースでタイトルに戻れるかのフラグ
    private bool m_isReturnTitle;

    //終了した瞬間のフラグ
    private bool m_isFinishGame;

    //結果発表の開始フラグ
    private bool m_isStartResult;

    //PushSpaceが出るフラグ
    private bool m_isPushSpace;

    //結果表示のプレイヤーごとの間隔
    private const int PLAYER_RESULT_INTERVAL = 200;

    PlayerPoint[] playerPoints;

    //プレイヤーの結果リスト
    private List<GameObject> m_playerResultList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        m_isReturnTitle = false;
        m_isFinishGame = false;
        m_isStartResult = false;
        m_isPushSpace = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_time.GetComponent<GameTime>().GetIsFinish())
        {
            return;
        }
        //勝者を決定する
        if (!m_isFinishGame)
        {
            m_isFinishGame = true;

            //順位決定
            Ranking();

            //Finishの生成
            GameObject finishText = Instantiate(m_createUIObject[0]);
            finishText.transform.SetParent(m_canvas.transform, false);

            m_createdUIObject.Add(finishText);

        }
       

        //Finishの表示
        if (m_finishTimer > m_drawWinnerTime)
        {
            //UIの変更
            if (!m_isStartResult)
            {
                m_isStartResult = true;
                for (int i = 0; i < m_createdUIObject.Count; i++)
                {
                    Destroy(m_createdUIObject[i]);

                }
                m_createdUIObject.Clear();

                //背景の生成
                GameObject backGround = Instantiate(m_createUIObject[1]);
                backGround.transform.SetParent(m_canvas.transform, false);

                m_createdUIObject.Add(backGround);

                QuickSort(playerPoints, 0, playerPoints.Length - 1,true);

                //プレイヤーのリストを取得する
                List<GameObjectBase> playerList = m_playerManager.GetComponent<PlayerManager>().GetPlayerList();
                for (int i = 0; i < playerPoints.Length; i++)
                {
                    //結果の生成
                    GameObject playerResult = Instantiate(m_createUIObject[2]);
                    playerResult.transform.SetParent(backGround.transform, false);

                    //X座標の計算
                    int x = -(PLAYER_RESULT_INTERVAL/2) * (playerPoints.Length - 1) + (i * PLAYER_RESULT_INTERVAL);
                    playerResult.GetComponent<RectTransform>().localPosition = new Vector3(x, 0, 0);

                    //ポイント　
                   

                    playerResult.GetComponent<PlayerResult>().PlayerResultSetDate(playerPoints[i].rank, playerPoints[i].playerNum, playerPoints[i].point);
                    m_createdUIObject.Add(playerResult);

                    m_playerResultList.Add(playerResult);
                }
            }



        }

        int finishCount = 0;

        for (int i=0;i<m_playerResultList.Count;i++)
        {
           
            if(m_playerResultList[i].GetComponentInChildren<ResultPointGauge>().GetIsRankFlag())
            {
                finishCount++;
            }
           
        }

      
       

        //タイトルに戻れるようにする
        if (m_finishTimer >= m_returnTitleTime + m_drawWinnerTime)
        {
            if(!m_isPushSpace)
            {
                //PushSpaceの生成
                GameObject pushSpace = Instantiate(m_createUIObject[3]);
                pushSpace.transform.SetParent(m_canvas.transform, false);

                m_createdUIObject.Add(pushSpace);
            }
            m_isReturnTitle = true;
            m_isPushSpace = true;

        }


        if (finishCount == m_playerResultList.Count|| m_finishTimer <= m_drawWinnerTime)
        {
            //カウント開始
            m_finishTimer += Time.deltaTime;
        }
       


    }

    //-------------------------------------
    //順位決定
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    private void Ranking()
    {
        //プレイヤーのリストを取得する
        List<GameObjectBase> playerList = m_playerManager.GetComponent<PlayerManager>().GetPlayerList();
        playerPoints = new PlayerPoint[playerList.Count];


        //プレイヤーのデータ保存
        for (int i = 0; i < playerList.Count; i++)
        {
            //プレイヤーのポイント保存
            playerPoints[i].point = playerList[i].GetComponent<PlayerActions>().m_point;
            //プレイヤーの番号保存
           
            playerPoints[i].playerNum = playerList[i].GetComponent<WGTPlayerController>().m_playerNum;


        }

        //ソートをする
        QuickSort(playerPoints, 0, playerPoints.Length - 1,false);

        //現在の順位
        int rank = 0;
        //次の周の順位
        int nextRank = rank;

        //ランキング決定
        for (int i = playerPoints.Length-1 ; i >= 0; i--)
        {

            rank++;
            nextRank = rank;

            playerPoints[i].rank = nextRank;
            for (int j = i-1; j >= 0; j--)
            {
                if(playerPoints[i].point== playerPoints[j].point)
                {
                    i--;
                    playerPoints[i].rank = nextRank;
                    rank++;
                   
                }
                
                
            }
          
            
        }        
    }

    //-------------------------------------
    //すべてのプレイヤーが終了したときの処理
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    private void ResultFinishAllPlayer()
    {
        //次のシーンへ行く
    }


    //-------------------------------------
    //クイックソート　
    //
    //引数     :PlayerPoint[] ソート対象の配列,int ソート範囲の最初のインデックス,int　ソート範囲の最後のインデックス,プレイヤー順にするかのフラグ
    //戻り値   :なし　None
    //-------------------------------------
    private void QuickSort(PlayerPoint[] array, int left, int right, bool isplayernum)
    {

        //確認範囲が1要素しかない場合は処理を抜ける
        if (left >= right)
        {
            return;
        }

        //左から確認していくインデックスを格納
        int i = left;

        //右から確認していくインデックスを格納
        int j = right;

        //ピボット選択に使う配列の真ん中のインデックスを計算
        int mid = (left + right) / 2;

        if(isplayernum)
        {
            //ピボットを決定します。
            int pivot = GetMediumValue(array[i].playerNum, array[mid].playerNum, array[j].playerNum);

            while (true)
            {
                //ピボットの値以上の値を持つ要素を左から確認
                while (array[i].playerNum < pivot)
                {
                    i++;
                }

                //ピボットの値以下の値を持つ要素を右から確認
                while (array[j].playerNum > pivot)
                {
                    j--;
                }

                // 左から確認したインデックスが、右から確認したインデックス以上であれば外側のwhileループを抜ける
                if (i >= j)
                {
                    break;
                }

                // そうでなければ、見つけた要素を交換
                PlayerPoint temp = array[j];
                array[j] = array[i];
                array[i] = temp;

                // 交換を行なった要素の次の要素にインデックスを進める
                i++;
                j--;

            }
            // 左側の範囲について再帰的にソートを行う
            QuickSort(array, left, i - 1, isplayernum);

            // 右側の範囲について再帰的にソートを行う
            QuickSort(array, j + 1, right, isplayernum);
        }
        else
        {
            //ピボットを決定します。
            int pivot = GetMediumValue(array[i].point, array[mid].point, array[j].point);

            while (true)
            {
                //ピボットの値以上の値を持つ要素を左から確認
                while (array[i].point < pivot)
                {
                    i++;
                }

                //ピボットの値以下の値を持つ要素を右から確認
                while (array[j].point > pivot)
                {
                    j--;
                }

                // 左から確認したインデックスが、右から確認したインデックス以上であれば外側のwhileループを抜ける
                if (i >= j)
                {
                    break;
                }

                // そうでなければ、見つけた要素を交換
                PlayerPoint temp = array[j];
                array[j] = array[i];
                array[i] = temp;

                // 交換を行なった要素の次の要素にインデックスを進める
                i++;
                j--;

            }
            // 左側の範囲について再帰的にソートを行う
            QuickSort(array, left, i - 1, isplayernum);

            // 右側の範囲について再帰的にソートを行う
            QuickSort(array, j + 1, right, isplayernum);
        }
       

       
    }

    //-------------------------------------
    //引数で渡された値の中から中央値を返します。
    //
    //引数     :int 確認範囲の最初の要素,int 中確認範囲の真ん中の要素,int 確認範囲の最後の要素
    //戻り値   :なし　None
    //-------------------------------------
    private int GetMediumValue(int top, int mid, int bottom)
    {
        if (top < mid)
        {
            if (mid < bottom)
            {
                return mid;
            }
            else if (bottom < top)
            {
                return top;
            }
            else
            {
                return bottom;
            }
        }
        else
        {
            if (bottom < mid)
            {
                return mid;
            }
            else if (top < bottom)
            {
                return top;
            }
            else
            {
                return bottom;
            }
        }
    }

    public bool GetIsReturnTitle()
    {
        return m_isReturnTitle;
    }


    
}
