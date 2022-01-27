//==============================================
//Day           :12/13
//Creator       :HashizumeAtsuki
//Description   :�|�C���g�\��UI
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointUI : MonoBehaviour
{
    //�|�C���g��\������v���C���[
    private GameObject m_player;

    //���_���ς�������Ƃ𔻒f����ϐ�
    private int m_playerBeforePoint;

    //�ŏ��̑傫��
    [SerializeField] private float m_startSize = 1.0f;

    //�ŏI�I�ȑ傫��
    [SerializeField] private float m_finishSize = 1.5f;

    //larp�̐���
    private float m_larpT = 0.0f;

    //���o���N�����t���O
    private bool m_isAction = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�|�C���g���������Ƃ��ɋN�����A�N�V����
        if(m_isAction)
        {
            GetComponent<RectTransform>().localScale = new Vector3(
               m_startSize * (m_finishSize * (1 + (Mathf.Cos(m_larpT) * 0.5f))),
               m_startSize * (m_finishSize * (1 + (Mathf.Cos(m_larpT) * 0.5f))),
               m_startSize * (m_finishSize * (1 + (Mathf.Cos(m_larpT) * 0.5f)))
               );

            if (m_larpT >= 2.0f)
            {
                m_isAction = false;
            }
            m_larpT += Time.deltaTime*20.0f;
        }
       
        if(m_playerBeforePoint!= m_player.GetComponent<PlayerActions>().m_point)
        {
            GetComponent<AudioSource>().Play();
            m_isAction = true;
            m_larpT = 0.0f;
        }

        m_playerBeforePoint = m_player.GetComponent<PlayerActions>().m_point;
        Text pointText = this.GetComponent<Text>();
        pointText.text = m_player.GetComponent<PlayerActions>().m_point.ToString();

    }

    public void SetPlayer(GameObject player)
    {
        m_player = player;
    }
}
