//==============================================
//Day           :12/07
//Creator       :HashizumeAtsuki
//Description   :�|�[�YUI
//               
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    //PauseUI�̏��
    enum PauseUIState
    {
        None=0,
        Start,
        Update,
        End,
    }

    //�L�����o�X
    [SerializeField] private GameObject m_canvas;

    //����
    [SerializeField] private GameObject m_time;

    //�v���C���[
    private GameObject m_player;

    //���o��UI�I�u�W�F�N�g
    [SerializeField] private List<GameObject> m_createUIObject=new List<GameObject>();

    //�����UI�I�u�W�F�N�g
    private List<GameObject> m_createdUIObject = new List<GameObject>();

    //PauseUI�̏��
    private PauseUIState m_pauseUIState;

    // Start is called before the first frame update
    void Start()
    {
        m_pauseUIState = PauseUIState.None;
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԑ؂�̂Ƃ��|�[�Y��ʂ��J���Ă�����I������
        if(m_time.GetComponent<GameTime>().GetIsFinish()&& m_pauseUIState != PauseUIState.None)
        {
            m_pauseUIState = PauseUIState.End;
        }

        switch (m_pauseUIState)
        {
            case PauseUIState.None:
               
               
                break;
            case PauseUIState.Start:
                //�w�i�̐���
                GameObject backScreen = Instantiate(m_createUIObject[0]);
                 backScreen.transform.SetParent(m_canvas.transform,false);

                m_createdUIObject.Add(backScreen);
                //�S�̃}�b�v�̐���
                GameObject bigMap = Instantiate(m_createUIObject[1]);
                bigMap.transform.SetParent(m_canvas.transform, false);

                m_createdUIObject.Add(bigMap);
                m_pauseUIState = PauseUIState.Update;
                break;
            case PauseUIState.Update:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    m_pauseUIState = PauseUIState.End;
                }
                break;
            case PauseUIState.End:
                for (int i = 0; i < m_createdUIObject.Count; i++)
                {
                    Destroy(m_createdUIObject[i]);
                   
                }
                m_createdUIObject.Clear();
                m_player.GetComponent<WGTPlayerController>().m_isInoperable = false;
                m_pauseUIState = PauseUIState.None;
                break;

        }


      
    }

    //-------------------------------------
    //�L�[���͂ɂ��|�[�Y��ʂ̕\��
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    public void InputKeyPause()
    {
        if(m_pauseUIState!=PauseUIState.None)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_pauseUIState = PauseUIState.Start;
            m_player.GetComponent<WGTPlayerController>().m_isInoperable = true;
            m_player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


    public void SetPlayer(GameObject player)
    {
        m_player = player;
    }

}
