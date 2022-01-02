using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class KOTHNetworkGamestateManager : NetworkBehaviour
{
    List<LobbyPlayer> players = new List<LobbyPlayer>();

    private void Start()
    {
    }

    public void AddPlayer (LobbyPlayer _player, Guid matchID)
    {
        players.Add(_player);
    }
}
