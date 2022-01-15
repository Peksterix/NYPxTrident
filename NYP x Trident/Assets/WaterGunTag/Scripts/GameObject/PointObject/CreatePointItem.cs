//==============================================
//Day           :11/08
//Creator       :HashizumeAtsuki
//Description   :�|�C���g������A�C�e���i�I�j�𐶐�����N���X
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class CreatePointItem : NetworkBehaviour
{
    //�������镨�̃��X�g
    [SerializeField] private List<GameObjectBase> m_pointItem = new List<GameObjectBase>();

    //�������镨�̃��X�g�i���A�j
    [SerializeField] private List<GameObjectBase> m_rarePointItem = new List<GameObjectBase>();

    [SerializeField] private List<Transform> m_pointItemSpawnPoints = new List<Transform>();

    //�����܂ł̎��ԁi�ő��j
    [SerializeField] private float m_createTime_Fast = 5;
    //���������m��
    [SerializeField] private float m_createProbability = 2000.0f;
    //���A���̂���������镨�̊m��
    [SerializeField] private float m_createRare = 10.0f;

    public GameObject m_pointObjectContainer;

    //��������
    private GameObject m_time;

    //�������ꂽ��
    private GameObject m_createObj;

    //�����܂ł̎��Ԍv��
    [SyncVar]
    private float m_createTime = 0.0f;

    //�Q�[���}�l�[�W���[
    private GameObject m_wgtGameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
            return;
        //�V�[�h�l����
        Random.InitState(System.DateTime.Now.Millisecond);
        m_createTime = m_createTime_Fast;
        m_time = GameObject.Find("Time");
        m_wgtGameManager = GameObject.Find("WGTGameManager");
    }

    // Update is called once per frame
    void Update()
    { 
        //�������Ԃ��I����Ă����牽�����Ȃ�
        //if (m_wgtGameManager.GetComponent<WGTGameManager>().GetIsStopGame())
        if (WGTGameManager.GetCurrGameState() == WGTGameManager.GameState.Ended || !isServer)
        {
            return;
        }

        //�q������Ȃ牽�����Ȃ��ŏI���
        //if (0<this.gameObject.transform.childCount)
        //{
        //    m_createTime = m_createTime_Fast;
        //    return;
        //}

        m_createTime-= Time.deltaTime;

        //�����܂ł̎��Ԃ��o���Ă��Ȃ���Ή������Ȃ�
        if (m_createTime>=0.0f)
        {
            return;
        }

        
        //�A�C�e���̐�������
        if(Random.Range(0.0f, m_createProbability) <=1.0f)
        {
            //���A�A�C�e������
            if (Random.Range(0.0f, m_createRare) <= 1.0f)
            {
                SpawnRarePointItem();
            }
            else //�ʏ�A�C�e������
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
