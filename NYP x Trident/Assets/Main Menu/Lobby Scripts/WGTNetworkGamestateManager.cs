using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WGTNetworkGamestateManager : MonoBehaviour
{
    List<LobbyPlayer> players = new List<LobbyPlayer>();
    List<GameObject> spawnPoints = new List<GameObject>();

    public void AddPlayer(LobbyPlayer _player)
    {
        players.Add(_player);
    }

    void SpawnPlayers()
    {

    }
}
