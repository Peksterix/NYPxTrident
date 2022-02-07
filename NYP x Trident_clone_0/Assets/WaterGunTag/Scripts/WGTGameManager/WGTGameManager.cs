//==============================================
//Day           :12/14
//Creator       :HashizumeAtsuki
//Description   :�Q�[���S�̂̊Ǘ�
//               
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Utility;

public static class WGTGameManager
{
    public enum GameState
    {
        Starting,
        Ongoing,
        Ended
    }

    //�Q�[���v���C�̒�~
    //private bool m_isStopGame = false;

    static GameState currGameState;

    public static GameState GetCurrGameState()
    {
        return currGameState;
    }

    public static void SetCurrGameState(GameState gameState)
    {
        currGameState = gameState;
    }


    //public bool GetIsStopGame()
    //{
    //    return m_isStopGame;
    //}

    //public void SetIsStopGame(bool isstopgame)
    //{
    //    m_isStopGame = isstopgame;
    //}
}
