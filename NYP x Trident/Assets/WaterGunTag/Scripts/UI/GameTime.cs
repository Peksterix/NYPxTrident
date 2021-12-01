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

    void Start()
    {
        RestartTime();
    }

    // Update is called once per frame
    void Update()
    {
        UpdeteTime();
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
        }
       
        if(m_time<=0&&!m_isFinish)
        {
            m_isFinish = true;
        }

        m_timeCount+=Time.deltaTime;
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


}
