//==============================================
//Day           :12/14
//Creator       :HashizumeAtsuki
//Description   :�Q�[���S�̂̊Ǘ�
//               
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WGTGameManager : MonoBehaviour
{
    //�Q�[���v���C�̒�~
    private bool m_isStopGame = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool GetIsStopGame()
    {
        return m_isStopGame;
    }

    public void SetIsStopGame(bool isstopgame)
    {
        m_isStopGame = isstopgame;
    }
}
