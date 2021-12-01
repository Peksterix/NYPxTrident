//==============================================
//Day           :11/08
//Creator       :HashizumeAtsuki
//Description   :ポイントが入るアイテム（的）を生成するクラス
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePointItem : MonoBehaviour
{
    //生成する物のリスト
    [SerializeField] private List<GameObjectBase> m_pointItem = new List<GameObjectBase>();

    //生成する物のリスト（レア）
    [SerializeField] private List<GameObjectBase> m_rarePointItem = new List<GameObjectBase>();

    //生成までの時間（最速）
    [SerializeField] private float m_createTime_Fast = 5;
    //生成される確率
    [SerializeField] private float m_createProbability = 10000.0f;
    //レアものが生成される物の確率
    [SerializeField] private float m_createRare = 100.0f;

    //制限時間
    private GameObject m_time;

    //生成された物
    private GameObject m_createObj;

    //生成までの時間計測
    private float m_createTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //シード値生成
        Random.InitState(System.DateTime.Now.Millisecond);
        m_createTime = m_createTime_Fast;
        m_time = GameObject.Find("Time");
    }

    // Update is called once per frame
    void Update()
    {
        //制限時間が終わっていたら何もしない
        if (m_time.GetComponent<GameTime>().GetIsFinish())
        {
            return;
        }
            //子がいるなら何もしないで終わる
            if (0<this.gameObject.transform.childCount)
        {
            m_createTime = m_createTime_Fast;
            return;
        }

        m_createTime-= Time.deltaTime;

        //生成までの時間が経っていなければ何もしない
        if (m_createTime>=0.0f)
        {
            return;
        }

        
        //アイテムの生成処理
        if(Random.Range(0.0f, m_createProbability) <=1.0f)
        {
            //レアアイテム生成
            if (Random.Range(0.0f, m_createRare) <= 1.0f)
            {
                m_createObj = Instantiate(m_rarePointItem[Random.Range(0, m_rarePointItem.Count)].gameObject, this.transform.position, Quaternion.identity);
                m_createObj.transform.parent = this.transform;
            }
            else //通常アイテム生成
            {
                m_createObj = Instantiate(m_pointItem[Random.Range(0, m_rarePointItem.Count)].gameObject, this.transform.position, Quaternion.identity);
                m_createObj.transform.parent = this.transform;
            }
               
        }
    }
}
