//==============================================
//Day           :12/15
//Creator       :HashizumeAtsuki
//Description   :���U���g���̃|�C���g�Q�[�W
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPointGauge : MonoBehaviour
{

    //���̃Q�[���̌��E�X�R�A
    [SerializeField] int m_highScore = 100;

    //�Q�[�W�̑����鉉�o����
    private float m_upGauge = 0.0f;

    //�|�C���g�e�L�X�g
    [SerializeField] private GameObject m_resultPointText;

    //���ʃe�L�X�g
    [SerializeField] private GameObject m_rank;

    //�|�C���g
    private int m_point;

    //�����𐮐��ɂ���
    private int m_intUpGauge;

    //���ʕ\�����I��������̔���t���O
    private bool m_isRankFlag;

    // Start is called before the first frame update
    void Start()
    {
        m_upGauge = 0.0f;
        m_intUpGauge = 0;
        m_isRankFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        GetComponent<Image>().fillAmount = m_upGauge / m_highScore;
        //�e�L�X�g�̃|�W�V�����v�Z
        float y = (GetComponent<RectTransform>().sizeDelta.y * GetComponent<Image>().fillAmount) - 250;

        if (m_upGauge <= m_point)
        {
            m_upGauge += Time.deltaTime * 20;
        }
        else
        {
            m_upGauge = m_point;
            m_rank.SetActive(true);
            m_rank.SetActive(true);
            m_rank.GetComponent<RectTransform>().localPosition = new Vector3(0, y + 200, 0);

            m_isRankFlag = true;
        }

        m_intUpGauge = (int)m_upGauge;

        m_resultPointText.GetComponent<Text>().text = m_intUpGauge.ToString();

      
        m_resultPointText.GetComponent<RectTransform>().localPosition = new Vector3(0, y + 100, 0);

        
    }

    public void SetPoint(int point)
    {
        m_point = point;
    }

    public bool GetIsRankFlag()
    {
        return m_isRankFlag;
    }

}
