//==============================================
//Day           :12/14
//Creator       :HashizumeAtsuki
//Description   :�X�^�[�g���̃J�E���g�_�E��
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class StartCountDown : NetworkBehaviour
{
    //�L�����o�X
    [SerializeField] private GameObject m_canvas;

    //�J�E���g�_�E���̐����̏����l
    [SerializeField] private int m_startCountDownNum = 4;

    //�J�E���g�_�E���̐���
    [SyncVar] private int m_countDownNum;

    //1�b���Ƃ邽�߂̎���
    [SyncVar] private float m_timeCount = 0;

    //�I��������
    [SyncVar] private bool m_isFinish;

    //���o��UI�I�u�W�F�N�g
    //[SerializeField] private List<GameObject> m_createUIObject = new List<GameObject>();

    // Instead of making UI objects a spawnable object, its easier to leave UI objects that will only be used once in the canvas. 
    // For networking, it won't be as heavy on the server as well.
    //�����UI�I�u�W�F�N�g
    //private List<GameObject> m_createdUIObject = new List<GameObject>();

    [SerializeField] private GameObject m_CountdownUIObject;

    //�Q�[���}�l�[�W���[
    private GameObject m_wgtGameManager;

    //�ŏ��̑傫��
    [SerializeField] private float m_startSize = 10.0f;

    //�ŏI�I�ȑ傫��
    [SerializeField] private float m_finishSize = 1.0f;

    //larp�̐���
    [SyncVar] private float m_larpT = 0.0f;

    void Start()
    {
        m_wgtGameManager = GameObject.Find("WGTGameManager");
        //m_wgtGameManager.GetComponent<WGTGameManager>().SetIsStopGame(true);
        WGTGameManager.SetCurrGameState(WGTGameManager.GameState.Ended);

        if (!isServer)
            return;

        RestartTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isFinish || !isServer)
        {
            return;
        }

        if (m_wgtGameManager == null)
        {
            m_wgtGameManager = GameObject.Find("WGTGameManager");
        }

        if (m_wgtGameManager == null)
            return;

        UpdateTime();
        UpdateText();
    }

    //-------------------------------------
    //���Ԃ̃��Z�b�g
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    public void RestartTime()
    {

        //�w�i�̐���
        //GameObject textObject = Instantiate(m_createUIObject[0]);
        //textObject.transform.SetParent(m_canvas.transform, false);
        //m_createdUIObject.Add(textObject);

        //m_CountdownUIObject = Instantiate(m_createUIObject[0], m_canvas.transform);
        //NetworkServer.Spawn(m_CountdownUIObject);

        m_countDownNum = m_startCountDownNum;
        m_isFinish = false;

        m_timeCount = 0;
        m_larpT = 0.0f;
    }

    //-------------------------------------
    //���Ԃ�i�߂�
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    [ClientRpc]
    private void UpdateTime()
    {
        if (m_CountdownUIObject == null)
            return;

        if (m_timeCount <= 0.5f)
        {
            m_larpT = m_timeCount * 2;
            // m_createdUIObject[0].GetComponent<RectTransform>().localScale = new Vector3(
            //Mathf.Lerp(m_startSize, m_finishSize, m_larpT),
            //Mathf.Lerp(m_startSize, m_finishSize, m_larpT),
            //Mathf.Lerp(m_startSize, m_finishSize, m_larpT));

            m_CountdownUIObject.GetComponent<RectTransform>().localScale = new Vector3(
           Mathf.Lerp(m_startSize, m_finishSize, m_larpT),
           Mathf.Lerp(m_startSize, m_finishSize, m_larpT),
           Mathf.Lerp(m_startSize, m_finishSize, m_larpT));
        }

        if (m_countDownNum >= 0 && m_timeCount >= 1)
        {
            m_countDownNum--;
            m_timeCount = 0;
        }

        if (m_countDownNum < 0 && !m_isFinish)
        {
            //for (int i = 0; i < m_createdUIObject.Count; i++)
            //{
            //    Destroy(m_createdUIObject[i]);

            //}
            //m_createdUIObject.Clear();

            m_CountdownUIObject.SetActive(false);
            //m_wgtGameManager.GetComponent<WGTGameManager>().SetIsStopGame(false);
            WGTGameManager.SetCurrGameState(WGTGameManager.GameState.Ongoing);
            m_isFinish = true;
        }

        if (m_countDownNum == 0)
        {
            //m_wgtGameManager.GetComponent<WGTGameManager>().SetIsStopGame(false);
            WGTGameManager.SetCurrGameState(WGTGameManager.GameState.Ongoing);
        }

        m_timeCount += Time.deltaTime;
    }

    //-------------------------------------
    //�e�L�X�g�̍X�V
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    [ClientRpc]
    private void UpdateText()
    {
        if (m_isFinish)
        {
            Destroy(m_CountdownUIObject);
            return;
        }

        if (m_CountdownUIObject == null)
            return;

        //Text timeText = m_createdUIObject[0].GetComponent<Text>();
        Text timeText = m_CountdownUIObject.GetComponent<Text>();
        if (m_countDownNum == 4)
        {

            timeText.text = "";
            return;

        }

        if (m_countDownNum == 0)
        {
            timeText.text = "START!";
        }
        else
        {
            timeText.text = m_countDownNum.ToString();
        }
    }

    //-------------------------------------
    //�J�E���g�_�E�����I���������̎擾
    //
    //����     :�Ȃ��@None
    //�߂�l   :�J�E���g�_�E�����I���������̔���@
    //-------------------------------------
    public bool GetIsFinish()
    {
        return m_isFinish;
    }
}