//==============================================
//Day           :12/14
//Creator       :HashizumeAtsuki
//Description   :ÉQÅ[ÉÄëSëÃÇÃä«óù
//               
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bamboo.Utility;

public class WGTGameManager : Singleton<WGTGameManager>
{
    public enum GameState
    {
        Starting,
        Ongoing,
        Ended
    }

    public GameState currGameState;
    //ÉQÅ[ÉÄÉvÉåÉCÇÃí‚é~
    private bool m_isStopGame = false;

    // Start is called before the first frame update
    void Start()
    {
        currGameState = GameState.Starting;
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

    public GameState GetCurrGameState()
    {
        return currGameState;
    }

    public void GameCurrGameState(GameState gameState)
    {
        currGameState = gameState;
    }
}
