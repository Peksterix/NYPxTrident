//==============================================
//Day           :11/09
//Creator       :HashizumeAtsuki
//Description   :勝者の表示
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawWinner : MonoBehaviour
{
    //ポイントソートのために使う構造体
    private struct PlayerPoint
    {
        public int point;
        public string name;
    };

    //背景
    [SerializeField] public GameObject m_backGround;
    //PushSpace
    [SerializeField] public GameObject m_pushSpace;

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

    //Finishのフォントサイズ
    private int m_finishFontSize = 300;

    //勝者のフォントサイズ
    private int m_winnerFontSize = 200;

    // Start is called before the first frame update
    void Start()
    {
        m_isReturnTitle = false;
        m_isFinishGame = false;
        m_backGround.GetComponent<Image>().enabled = false;
        m_pushSpace.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_time.GetComponent<GameTime>().GetIsFinish())
        {
            
            //勝者を決定する
            if (!m_isFinishGame)
            {
                m_isFinishGame = true;
                //背景を描画する
                m_backGround.GetComponent<Image>().enabled = true;
                //プレイヤーのリストを取得する
                List<GameObjectBase> playerList = m_playerManager.GetComponent<PlayerManager>().GetPlayerList();
                PlayerPoint[] playerPoints = new PlayerPoint[playerList.Count];

               
                //プレイヤーのデータ保存
                for (int i = 0; i < playerList.Count; i++)
                {
                    //プレイヤーのポイント保存
                    playerPoints[i].point = playerList[i].GetComponent<PlayerActions>().m_point;
                    //プレイヤーの名前保存
                    int playerNum = i + 1;
                    playerPoints[i].name = playerNum.ToString() + "P";

                   
                }

                //ソートをする
                QuickSort(playerPoints, 0, playerPoints.Length - 1);

                //勝者表示のテキスト表示
                m_winnerName = "WINNER\n";
                m_winnerName += playerPoints[playerPoints.Length - 1].name+" POINT:"+playerPoints[playerPoints.Length - 1].point;
                
                for (int i = playerPoints.Length - 1; i >0; i--)
                {
                   
                    //1位が2人以上いたら勝者を増やす
                    if (playerPoints[i].point == playerPoints[i-1].point)
                    {
                        m_winnerName += "\n" + playerPoints[i - 1].name + " POINT:" + playerPoints[i - 1].point; 
                    }
                    else
                    {
                      
                        break;
                    }

                }

                //引き分け
                if (playerPoints[0].point == playerPoints[playerPoints.Length - 1].point)
                {
                    m_winnerName = "DRAW";
                }
            }
            //カウント開始
            m_finishTimer += Time.deltaTime;

            //Finishの表示
            if (m_finishTimer <= m_drawWinnerTime)
            {
                Text timeText = this.GetComponent<Text>();
                timeText.text = "FINISH";
              
                timeText.fontSize = m_finishFontSize;
            }
            else
            {
                //勝者の表示
                Text timeText = this.GetComponent<Text>();
              
                timeText.fontSize = m_winnerFontSize;
                timeText.text = m_winnerName;
            }

            //タイトルに戻れるようにする
            if(m_finishTimer>=m_returnTitleTime+m_drawWinnerTime)
            {
                m_isReturnTitle = true;
                m_pushSpace.GetComponent<Text>().enabled = true;
            }



        }
    }


    //-------------------------------------
    //クイックソート　
    //
    //引数     :PlayerPoint[] ソート対象の配列,int ソート範囲の最初のインデックス,int　ソート範囲の最後のインデックス
    //戻り値   :なし　None
    //-------------------------------------
    private void QuickSort(PlayerPoint[] array, int left, int right)
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
        QuickSort(array, left, i - 1);

        // 右側の範囲について再帰的にソートを行う
        QuickSort(array, j + 1, right);
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
