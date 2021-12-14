//==============================================
//Day           :11/09
//Creator       :HashizumeAtsuki
//Description   :���҂̕\��
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawWinner : MonoBehaviour
{
    //�|�C���g�\�[�g�̂��߂Ɏg���\����
    private struct PlayerPoint
    {
        public int point;
        public string name;
    };

    //�w�i
    [SerializeField] public GameObject m_backGround;
    //PushSpace
    [SerializeField] public GameObject m_pushSpace;

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

    //Finish�̃t�H���g�T�C�Y
    private int m_finishFontSize = 300;

    //���҂̃t�H���g�T�C�Y
    private int m_winnerFontSize = 200;

    // Start is called before the first frame update
    void Start()
    {
        m_isReturnTitle = false;
        m_isFinishGame = false;
        m_backGround.GetComponent<Image>().enabled = false;
        m_pushSpace.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_time.GetComponent<GameTime>().GetIsFinish())
        {
            
            //���҂����肷��
            if (!m_isFinishGame)
            {
                m_isFinishGame = true;
                //�w�i��`�悷��
                m_backGround.GetComponent<Image>().enabled = true;
                //�v���C���[�̃��X�g���擾����
                List<GameObjectBase> playerList = m_playerManager.GetComponent<PlayerManager>().GetPlayerList();
                PlayerPoint[] playerPoints = new PlayerPoint[playerList.Count];

               
                //�v���C���[�̃f�[�^�ۑ�
                for (int i = 0; i < playerList.Count; i++)
                {
                    //�v���C���[�̃|�C���g�ۑ�
                    playerPoints[i].point = playerList[i].GetComponent<PlayerActions>().m_point;
                    //�v���C���[�̖��O�ۑ�
                    int playerNum = i + 1;
                    playerPoints[i].name = playerNum.ToString() + "P";

                   
                }

                //�\�[�g������
                QuickSort(playerPoints, 0, playerPoints.Length - 1);

                //���ҕ\���̃e�L�X�g�\��
                m_winnerName = "WINNER\n";
                m_winnerName += playerPoints[playerPoints.Length - 1].name+" POINT:"+playerPoints[playerPoints.Length - 1].point;
                
                for (int i = playerPoints.Length - 1; i >0; i--)
                {
                   
                    //1�ʂ�2�l�ȏア���珟�҂𑝂₷
                    if (playerPoints[i].point == playerPoints[i-1].point)
                    {
                        m_winnerName += "\n" + playerPoints[i - 1].name + " POINT:" + playerPoints[i - 1].point; 
                    }
                    else
                    {
                      
                        break;
                    }

                }

                //��������
                if (playerPoints[0].point == playerPoints[playerPoints.Length - 1].point)
                {
                    m_winnerName = "DRAW";
                }
            }
            //�J�E���g�J�n
            m_finishTimer += Time.deltaTime;

            //Finish�̕\��
            if (m_finishTimer <= m_drawWinnerTime)
            {
                Text timeText = this.GetComponent<Text>();
                timeText.text = "FINISH";
              
                timeText.fontSize = m_finishFontSize;
            }
            else
            {
                //���҂̕\��
                Text timeText = this.GetComponent<Text>();
              
                timeText.fontSize = m_winnerFontSize;
                timeText.text = m_winnerName;
            }

            //�^�C�g���ɖ߂��悤�ɂ���
            if(m_finishTimer>=m_returnTitleTime+m_drawWinnerTime)
            {
                m_isReturnTitle = true;
                m_pushSpace.GetComponent<Text>().enabled = true;
            }



        }
    }


    //-------------------------------------
    //�N�C�b�N�\�[�g�@
    //
    //����     :PlayerPoint[] �\�[�g�Ώۂ̔z��,int �\�[�g�͈͂̍ŏ��̃C���f�b�N�X,int�@�\�[�g�͈͂̍Ō�̃C���f�b�N�X
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    private void QuickSort(PlayerPoint[] array, int left, int right)
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
        QuickSort(array, left, i - 1);

        // �E���͈̔͂ɂ��čċA�I�Ƀ\�[�g���s��
        QuickSort(array, j + 1, right);
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
