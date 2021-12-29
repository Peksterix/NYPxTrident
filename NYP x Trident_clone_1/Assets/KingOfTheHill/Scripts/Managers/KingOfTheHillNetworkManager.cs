using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KingOfTheHillNetworkManager : NetworkManager
{
    [Header("King Of The Hill Variables")]

    [Header("Player Start Positions")]
    public GameObject PlayerSpawnPoints;

    [Header("Managers")]
    [SerializeField]
    private GameObject platformManager;
    [SerializeField]
    private GameObject gameManager;

    public override void OnStartServer()
    {
        for (int i = 0; i < PlayerSpawnPoints.transform.childCount; ++i)
        {
            RegisterStartPosition(PlayerSpawnPoints.transform.GetChild(i));
        }

        platformManager.SetActive(true);
        gameManager.SetActive(true);
    }
}
