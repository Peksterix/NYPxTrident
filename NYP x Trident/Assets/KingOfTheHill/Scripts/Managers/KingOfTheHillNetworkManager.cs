using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KingOfTheHillNetworkManager : NetworkBehaviour
{
    [Header("King Of The Hill Variables")]
    [SerializeField]
    GameObject KOTHPlayerPrefab;
    [SerializeField]
    GameObject playerParent;

    [Header("Player Start Positions")]
    public List<Transform> PlayerSpawnPoints = new List<Transform>();

    [Header("Managers")]
    [SerializeField]
    private GameObject platformManager;
    [SerializeField]
    private GameObject gameManager;

    private void Awake()
    {
        if (!isServer)
            return;
    }

    private void Start()
    {
    }

    //public override void OnStartServer()
    //{
    //    for (int i = 0; i < PlayerSpawnPoints.transform.childCount; ++i)
    //    {
    //        RegisterStartPosition(PlayerSpawnPoints.transform.GetChild(i));
    //    }

    //    platformManager.SetActive(true);
    //    gameManager.SetActive(true);
    //}
}
