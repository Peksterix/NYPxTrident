//==============================================
//Day           :12/15
//Creator       :HashizumeAtsuki
//Description   :���U���g�\��
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WGTResult : MonoBehaviour
{
    
    //�|�C���g�\�[�g�̂��߂Ɏg���\����
    private struct PlayerPoint
    {
        public int point;
        public int playerNum;
        public int rank;
    };

    //�L�����o�X
    [SerializeField] private GameObject m_canvas;

    //���o��UI�I�u�W�F�N�g
    [SerializeField] private List<GameObject> m_createUIObject = new List<GameObject>();

    //�����UI�I�u�W�F�N�g
    private List<GameObject> m_createdUIObject = new List<GameObject>();

    //�v���C���[�}�l�[�W���[
    [SerializeField] private GameObject m_playerManager;
    //��������
    [SerializeField] public GameObject m_time;

    //�I���\������J�E���g����^�C�}�[
    private float m_finishTimer = 0.0f;

    //���ʕ\���ɕς��鎞��
    [SerializeField] private float m_drawWinnerTime = 2.0f;

    //�X�y�[�X�L�[�Ń^�C�g���ɖ߂��悤�ɂȂ鎞��
    [SerializeField] private float m_returnTitleTime = 2.0f;

    //���҂̖��O
    private string m_winnerName;

    //�X�y�[�X�Ń^�C�g���ɖ߂�邩�̃t���O
    private bool m_isReturnTitle;

    //�I�������u�Ԃ̃t���O
    private bool m_isFinishGame;

    //���ʔ��\�̊J�n�t���O
    private bool m_isStartResult;

    //PushSpace���o��t���O
    private bool m_isPushSpace;

    //���ʕ\���̃v���C���[���Ƃ̊Ԋu
    private const int PLAYER_RESULT_INTERVAL = 200;

    PlayerPoint[] playerPoints;

    //�v���C���[�̌��ʃ��X�g
    private List<GameObject> m_playerResultList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        m_isReturnTitle = false;
        m_isFinishGame = false;
        m_isStartResult = false;
        m_isPushSpace = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_time.GetComponent<GameTime>().GetIsFinish())
        {
            return;
        }
        //���҂����肷��
        if (!m_isFinishGame)
        {
            m_isFinishGame = true;

            //���ʌ���
            Ranking();

            //Finish�̐���
            GameObject finishText = Instantiate(m_createUIObject[0]);
            finishText.transform.SetParent(m_canvas.transform, false);

            m_createdUIObject.Add(finishText);

        }
       

        //Finish�̕\��
        if (m_finishTimer > m_drawWinnerTime)
        {
            //UI�̕ύX
            if (!m_isStartResult)
            {
                m_isStartResult = true;
                for (int i = 0; i < m_createdUIObject.Count; i++)
                {
                    Destroy(m_createdUIObject[i]);

                }
                m_createdUIObject.Clear();

                //�w�i�̐���
                GameObject backGround = Instantiate(m_createUIObject[1]);
                backGround.transform.SetParent(m_canvas.transform, false);

                m_createdUIObject.Add(backGround);

                QuickSort(playerPoints, 0, playerPoints.Length - 1,true);

                //�v���C���[�̃��X�g���擾����
                List<GameObjectBase> playerList = m_playerManager.GetComponent<PlayerManager>().GetPlayerList();
                for (int i = 0; i < playerPoints.Length; i++)
                {
                    //���ʂ̐���
                    GameObject playerResult = Instantiate(m_createUIObject[2]);
                    playerResult.transform.SetParent(backGround.transform, false);

                    //X���W�̌v�Z
                    int x = -(PLAYER_RESULT_INTERVAL/2) * (playerPoints.Length - 1) + (i * PLAYER_RESULT_INTERVAL);
                    playerResult.GetComponent<RectTransform>().localPosition = new Vector3(x, 0, 0);

                    //�|�C���g�@
                   

                    playerResult.GetComponent<PlayerResult>().PlayerResultSetDate(playerPoints[i].rank, playerPoints[i].playerNum, playerPoints[i].point);
                    m_createdUIObject.Add(playerResult);

                    m_playerResultList.Add(playerResult);
                }
            }



        }

        int finishCount = 0;

        for (int i=0;i<m_playerResultList.Count;i++)
        {
           
            if(m_playerResultList[i].GetComponentInChildren<ResultPointGauge>().GetIsRankFlag())
            {
                finishCount++;
            }
           
        }

      
       

        //�^�C�g���ɖ߂��悤�ɂ���
        if (m_finishTimer >= m_returnTitleTime + m_drawWinnerTime)
        {
            if(!m_isPushSpace)
            {
                //PushSpace�̐���
                GameObject pushSpace = Instantiate(m_createUIObject[3]);
                pushSpace.transform.SetParent(m_canvas.transform, false);

                m_createdUIObject.Add(pushSpace);
            }
            m_isReturnTitle = true;
            m_isPushSpace = true;

        }


        if (finishCount == m_playerResultList.Count|| m_finishTimer <= m_drawWinnerTime)
        {
            //�J�E���g�J�n
            m_finishTimer += Time.deltaTime;
        }
       


    }

    //-------------------------------------
    //���ʌ���
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    private void Ranking()
    {
        //�v���C���[�̃��X�g���擾����
        List<GameObjectBase> playerList = m_playerManager.GetComponent<PlayerManager>().GetPlayerList();
        playerPoints = new PlayerPoint[playerList.Count];


        //�v���C���[�̃f�[�^�ۑ�
        for (int i = 0; i < playerList.Count; i++)
        {
            //�v���C���[�̃|�C���g�ۑ�
            playerPoints[i].point = playerList[i].GetComponent<PlayerActions>().m_point;
            //�v���C���[�̔ԍ��ۑ�
           
            playerPoints[i].playerNum = playerList[i].GetComponent<WGTPlayerController>().m_playerNum;


        }

        //�\�[�g������
        QuickSort(playerPoints, 0, playerPoints.Length - 1,false);

        //���݂̏���
        int rank = 0;
        //���̎��̏���
        int nextRank = rank;

        //�����L���O����
        for (int i = playerPoints.Length-1 ; i >= 0; i--)
        {

            rank++;
            nextRank = rank;

            playerPoints[i].rank = nextRank;
            for (int j = i-1; j >= 0; j--)
            {
                if(playerPoints[i].point== playerPoints[j].point)
                {
                    i--;
                    playerPoints[i].rank = nextRank;
                    rank++;
                   
                }
                
                
            }
          
            
        }        
    }

    //-------------------------------------
    //���ׂẴv���C���[���I�������Ƃ��̏���
    //
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    private void ResultFinishAllPlayer()
    {
        //���̃V�[���֍s��
    }


    //-------------------------------------
    //�N�C�b�N�\�[�g�@
    //
    //����     :PlayerPoint[] �\�[�g�Ώۂ̔z��,int �\�[�g�͈͂̍ŏ��̃C���f�b�N�X,int�@�\�[�g�͈͂̍Ō�̃C���f�b�N�X,�v���C���[���ɂ��邩�̃t���O
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    private void QuickSort(PlayerPoint[] array, int left, int right, bool isplayernum)
    {

        //�m�F�͈͂�1�v�f�����Ȃ��ꍇ�͏����𔲂���
        if (left >= right)
        {
            return;
        }

        //������m�F���Ă����C���f�b�N�X���i�[
        int i = left;

        //�E����m�F���Ă����C���f�b�N�X���i�[
        int j = right;

        //�s�{�b�g�I���Ɏg���z��̐^�񒆂̃C���f�b�N�X���v�Z
        int mid = (left + right) / 2;

        if(isplayernum)
        {
            //�s�{�b�g�����肵�܂��B
            int pivot = GetMediumValue(array[i].playerNum, array[mid].playerNum, array[j].playerNum);

            while (true)
            {
                //�s�{�b�g�̒l�ȏ�̒l�����v�f��������m�F
                while (array[i].playerNum < pivot)
                {
                    i++;
                }

                //�s�{�b�g�̒l�ȉ��̒l�����v�f���E����m�F
                while (array[j].playerNum > pivot)
                {
                    j--;
                }

                // ������m�F�����C���f�b�N�X���A�E����m�F�����C���f�b�N�X�ȏ�ł���ΊO����while���[�v�𔲂���
                if (i >= j)
                {
                    break;
                }

                // �����łȂ���΁A�������v�f������
                PlayerPoint temp = array[j];
                array[j] = array[i];
                array[i] = temp;

                // �������s�Ȃ����v�f�̎��̗v�f�ɃC���f�b�N�X��i�߂�
                i++;
                j--;

            }
            // �����͈̔͂ɂ��čċA�I�Ƀ\�[�g���s��
            QuickSort(array, left, i - 1, isplayernum);

            // �E���͈̔͂ɂ��čċA�I�Ƀ\�[�g���s��
            QuickSort(array, j + 1, right, isplayernum);
        }
        else
        {
            //�s�{�b�g�����肵�܂��B
            int pivot = GetMediumValue(array[i].point, array[mid].point, array[j].point);

            while (true)
            {
                //�s�{�b�g�̒l�ȏ�̒l�����v�f��������m�F
                while (array[i].point < pivot)
                {
                    i++;
                }

                //�s�{�b�g�̒l�ȉ��̒l�����v�f���E����m�F
                while (array[j].point > pivot)
                {
                    j--;
                }

                // ������m�F�����C���f�b�N�X���A�E����m�F�����C���f�b�N�X�ȏ�ł���ΊO����while���[�v�𔲂���
                if (i >= j)
                {
                    break;
                }

                // �����łȂ���΁A�������v�f������
                PlayerPoint temp = array[j];
                array[j] = array[i];
                array[i] = temp;

                // �������s�Ȃ����v�f�̎��̗v�f�ɃC���f�b�N�X��i�߂�
                i++;
                j--;

            }
            // �����͈̔͂ɂ��čċA�I�Ƀ\�[�g���s��
            QuickSort(array, left, i - 1, isplayernum);

            // �E���͈̔͂ɂ��čċA�I�Ƀ\�[�g���s��
            QuickSort(array, j + 1, right, isplayernum);
        }
       

       
    }

    //-------------------------------------
    //�����œn���ꂽ�l�̒����璆���l��Ԃ��܂��B
    //
    //����     :int �m�F�͈͂̍ŏ��̗v�f,int ���m�F�͈͂̐^�񒆂̗v�f,int �m�F�͈͂̍Ō�̗v�f
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    private int GetMediumValue(int top, int mid, int bottom)
    {
        if (top < mid)
        {
            if (mid < bottom)
            {
                return mid;
            }
            else if (bottom < top)
            {
                return top;
            }
            else
            {
                return bottom;
            }
        }
        else
        {
            if (bottom < mid)
            {
                return mid;
            }
            else if (top < bottom)
            {
                return top;
            }
            else
            {
                return bottom;
            }
        }
    }

    public bool GetIsReturnTitle()
    {
        return m_isReturnTitle;
    }


    
}
