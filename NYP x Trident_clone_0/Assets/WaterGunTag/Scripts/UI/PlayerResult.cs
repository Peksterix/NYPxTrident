//==============================================
//Day           :12/20
//Creator       :HashizumeAtsuki
//Description   :�v���C���[�̃��U���g
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResult : MonoBehaviour
{
    //���ʃe�L�X�g
    [SerializeField] private GameObject m_rank;
    //�v���C���[�i���o�[
    [SerializeField] private GameObject m_playerNum;
    //�|�C���g
    [SerializeField] private GameObject m_pointNum;
    //�|�C���g�Q�[�W
    [SerializeField] private GameObject m_pointGauge;

    // Start is called before the first frame update
    void Start()
    {
        m_rank.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-------------------------------------
    //�f�[�^�̎󂯎��
    //
    //����     :���� int rank,�v���C���[�i���o�[ int playernum,�|�C���g int point
    //�߂�l   :�Ȃ��@None
    //-------------------------------------

    public void PlayerResultSetDate(int rank,int playernum,int point)
    {
        m_rank.GetComponent<Text>().text = rank.ToString() + "st";
        m_playerNum.GetComponent<Text>().text = playernum.ToString() + "P";
        m_pointGauge.GetComponent<ResultPointGauge>().SetPoint(point);
    }

}
