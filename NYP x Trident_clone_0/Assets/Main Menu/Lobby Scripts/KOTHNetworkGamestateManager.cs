using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KOTHNetworkGamestateManager : NetworkBehaviour
{
    List<LobbyPlayer> players = new List<LobbyPlayer>();

    public void AddPlayer (LobbyPlayer _player)
    {
        players.Add(_player);
    }
}
