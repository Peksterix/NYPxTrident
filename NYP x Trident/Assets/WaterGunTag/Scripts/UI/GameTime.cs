//==============================================
//Day           :11/1
//Creator       :HashizumeAtsuki
//Description   :�Q�[���̐�������
//               
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    // Start is called before the first frame update
    //�������Ԃ̍ő厞��
    [SerializeField] private int m_maxTime = 60;

    //���݂̐�������
    private int m_time;

    //1�b���Ƃ邽�߂̎���
    private float m_timeCount = 0;

    //�I��������
    private bool m_isFinish;

    //�^�C���Q�[�W�p�̎���
    private float m_floatTime;

    //�Q�[���}�l�[�W���[
    private GameObject m_wgtGameManager;

    //�ŏ��̑傫��
    [SerializeField] private float m_startSize = 1.0f;

    //�ŏI�I�ȑ傫��
    [SerializeField] private float m_finishSize = 0.1f;

    //larp�̐���
    private float m_larpT = 0.0f;

    //���o���N�����t���O
    private bool m_isAction = false;

    void Start()
    {
        m_wgtGameManager = GameObject.Find("WGTGameManager");
        RestartTime();
    }

    // Update is called once per frame
    void Update()
    {
        //�|�C���g���������Ƃ��ɋN�����A�N�V����
        if (m_isAction)
        {
            if (m_time % 10 == 0)
            {
                GetComponent<RectTransform>().localScale = new Vector3(
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 2)),
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 2)),
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 2))
              );

            }
            else if (m_time <= 10)
            {
                GetComponent<RectTransform>().localScale = new Vector3(
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 2)),
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 2)),
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 2))
              );

            }

            else
            {
                GetComponent<RectTransform>().localScale = new Vector3(
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 0.5f)),
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 0.5f)),
              m_startSize + (m_finishSize * ((1 + Mathf.Cos(m_larpT)) * 0.5f))
              );
            }
           

            if (m_larpT >= 2.0f)
            {
                m_isAction = false;
            }
            m_larpT += Time.deltaTime * 10.0f;
        }
       
        if (!m_wgtGameManager.GetComponent<WGTGameManager>().GetIsStopGame())
        {
            UpdeteTime();
        }
        UpdateText();

    }

    //-------------------------------------
    //�������Ԃ̃��Z�b�g
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    public void RestartTime()
    {
        m_time = m_maxTime;
        m_floatTime = m_maxTime;
        m_isFinish = false;

        m_timeCount = 0;
    }

    //-------------------------------------
    //�������Ԃ�i�߂�
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    private void UpdeteTime()
    {
        


        if (m_time>0&&!m_isFinish&& m_timeCount >=1)
        {
            m_time--;
            m_timeCount = 0;
            m_isAction = true;
            m_larpT = 0.0f;
        }
       
        if(m_time<=0&&!m_isFinish)
        {
            m_wgtGameManager.GetComponent<WGTGameManager>().SetIsStopGame(true);
            m_isFinish = true;
        }

        float deltaTime= Time.deltaTime;

        m_timeCount += deltaTime;
        m_floatTime -= deltaTime;
    }

    //-------------------------------------
    //�e�L�X�g�̍X�V
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    private void UpdateText()
    {
        Text timeText = this.GetComponent<Text>();
        timeText.text = m_time.ToString();
    }

    //-------------------------------------
    //�Q�[�����I���������̎擾
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Q�[�����I���������̔���@
    //-------------------------------------
    public bool GetIsFinish()
    {
        return m_isFinish;
    }

    public int GetMaxTime()
    {
        return m_maxTime;
    }

    public int GetTime()
    {
        return m_time;
    }

    public float GetFloatTime()
    {
        return m_floatTime;
    }
}
