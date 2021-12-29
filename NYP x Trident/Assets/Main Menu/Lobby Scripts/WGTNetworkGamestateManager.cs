using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WGTNetworkGamestateManager : MonoBehaviour
{
    List<LobbyPlayer> players = new List<LobbyPlayer>();

    public void AddPlayer(LobbyPlayer _player)
    {
        players.Add(_player);
    }
}
