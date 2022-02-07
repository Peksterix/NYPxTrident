//==============================================
//Day           :12/14
//Creator       :HashizumeAtsuki
//Description   :ゲーム全体の管理
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

    //ゲームプレイの停止
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
