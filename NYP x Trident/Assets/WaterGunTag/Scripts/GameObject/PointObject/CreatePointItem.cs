//==============================================
//Day           :11/08
//Creator       :HashizumeAtsuki
//Description   :�|�C���g������A�C�e���i�I�j�𐶐�����N���X
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePointItem : MonoBehaviour
{
    //�������镨�̃��X�g
    [SerializeField] private List<GameObjectBase> m_pointItem = new List<GameObjectBase>();

    //�������镨�̃��X�g�i���A�j
    [SerializeField] private List<GameObjectBase> m_rarePointItem = new List<GameObjectBase>();

    //�����܂ł̎��ԁi�ő��j
    [SerializeField] private float m_createTime_Fast = 5;
    //���������m��
    [SerializeField] private float m_createProbability = 10000.0f;
    //���A���̂���������镨�̊m��
    [SerializeField] private float m_createRare = 100.0f;

    //��������
    private GameObject m_time;

    //�������ꂽ��
    private GameObject m_createObj;

    //�����܂ł̎��Ԍv��
    private float m_createTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //�V�[�h�l����
        Random.InitState(System.DateTime.Now.Millisecond);
        m_createTime = m_createTime_Fast;
        m_time = GameObject.Find("Time");
    }

    // Update is called once per frame
    void Update()
    {
        //�������Ԃ��I����Ă����牽�����Ȃ�
        if (m_time.GetComponent<GameTime>().GetIsFinish())
        {
            return;
        }
            //�q������Ȃ牽�����Ȃ��ŏI���
            if (0<this.gameObject.transform.childCount)
        {
            m_createTime = m_createTime_Fast;
            return;
        }

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
                m_createObj = Instantiate(m_rarePointItem[Random.Range(0, m_rarePointItem.Count)].gameObject, this.transform.position, Quaternion.identity);
                m_createObj.transform.parent = this.transform;
            }
            else //�ʏ�A�C�e������
            {
                m_createObj = Instantiate(m_pointItem[Random.Range(0, m_rarePointItem.Count)].gameObject, this.transform.position, Quaternion.identity);
                m_createObj.transform.parent = this.transform;
            }
               
        }
    }
}
