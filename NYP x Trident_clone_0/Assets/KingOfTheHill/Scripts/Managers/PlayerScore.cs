using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Mirror;

public class PlayerScore : NetworkBehaviour
{
    [Header("Player Details")]
    [SyncVar(hook = nameof(SyncScore))]
    public int playerScore;
    [SyncVar]
    public string playerName;

    [Header("UI References")]
    [SerializeField]
    GameObject playerScorePanel;
    [SerializeField]
    Transform playerScoreContainer;

    GameObject newPlayerScorePanel;

    private void Start()
    {
        playerScoreContainer = GameObject.Find("Canvas/ScoreContainer").transform;
        newPlayerScorePanel = Instantiate(playerScorePanel, playerScoreContainer);
        newPlayerScorePanel.transform.Find("PlayerScore").GetComponent<TextMeshProUGUI>().text = playerScore.ToString();
    }

    private void Update()
    {
        newPlayerScorePanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerName;
    }

    void SyncScore(int oldScore, int newScore)
    {
        playerScore = newScore;

        if (!newPlayerScorePanel)
            return;

        newPlayerScorePanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = playerScore.ToString();
    }
}
