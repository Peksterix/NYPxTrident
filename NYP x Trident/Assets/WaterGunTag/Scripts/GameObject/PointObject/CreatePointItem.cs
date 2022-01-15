//==============================================
//Day           :11/08
//Creator       :HashizumeAtsuki
//Description   :ポイントが入るアイテム（的）を生成するクラス
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class CreatePointItem : NetworkBehaviour
{
    //生成する物のリスト
    [SerializeField] private List<GameObjectBase> m_pointItem = new List<GameObjectBase>();

    //生成する物のリスト（レア）
    [SerializeField] private List<GameObjectBase> m_rarePointItem = new List<GameObjectBase>();

    [SerializeField] private List<Transform> m_pointItemSpawnPoints = new List<Transform>();

    //生成までの時間（最速）
    [SerializeField] private float m_createTime_Fast = 5;
    //生成される確率
    [SerializeField] private float m_createProbability = 2000.0f;
    //レアものが生成される物の確率
    [SerializeField] private float m_createRare = 10.0f;

    public GameObject m_pointObjectContainer;

    //制限時間
    private GameObject m_time;

    //生成された物
    private GameObject m_createObj;

    //生成までの時間計測
    [SyncVar]
    private float m_createTime = 0.0f;

    //ゲームマネージャー
    private GameObject m_wgtGameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
            return;
        //シード値生成
        Random.InitState(System.DateTime.Now.Millisecond);
        m_createTime = m_createTime_Fast;
        m_time = GameObject.Find("Time");
        m_wgtGameManager = GameObject.Find("WGTGameManager");
    }

    // Update is called once per frame
    void Update()
    { 
        //制限時間が終わっていたら何もしない
        //if (m_wgtGameManager.GetComponent<WGTGameManager>().GetIsStopGame())
        if (WGTGameManager.GetCurrGameState() == WGTGameManager.GameState.Ended || !isServer)
        {
            return;
        }

        //子がいるなら何もしないで終わる
        //if (0<this.gameObject.transform.childCount)
        //{
        //    m_createTime = m_createTime_Fast;
        //    return;
        //}

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
                SpawnRarePointItem();
            }
            else //通常アイテム生成
            {
                SpawnPointItem();
            }
               
        }
    }

    [ClientRpc]
    void SpawnPointItem()
    {
        Transform PointItemSpawnPoint = m_pointItemSpawnPoints[Random.Range(0, m_pointItemSpawnPoints.Count)];

        if (PointItemSpawnPoint.childCount > 0)
            return;

        m_createObj = Instantiate(m_pointItem[Random.Range(0, m_pointItem.Count)].gameObject, 
            PointItemSpawnPoint.position,
            Quaternion.identity,
            PointItemSpawnPoint);

        NetworkServer.Spawn(m_createObj);
    }

    [ClientRpc]
    void SpawnRarePointItem()
    {
        Transform RarePointItemSpawnPoint = m_pointItemSpawnPoints[Random.Range(0, m_pointItemSpawnPoints.Count)];

        if (RarePointItemSpawnPoint.childCount > 0)
            return;

        //m_createObj = Instantiate(m_rarePointItem[Random.Range(0, m_rarePointItem.Count)].gameObject, this.transform.position, Quaternion.identity);
        m_createObj = Instantiate(m_rarePointItem[Random.Range(0, m_rarePointItem.Count)].gameObject,
           RarePointItemSpawnPoint.position,
           Quaternion.identity,
           RarePointItemSpawnPoint);

        NetworkServer.Spawn(m_createObj);
    }
}
