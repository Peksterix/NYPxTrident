using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    LobbyPlayer player;

    public void SetPlayer(LobbyPlayer player)
    {
        this.player = player;
        text.text = this.player.username;
        //text.text = "Player " + player.playerIndex.ToString();
    }
}
