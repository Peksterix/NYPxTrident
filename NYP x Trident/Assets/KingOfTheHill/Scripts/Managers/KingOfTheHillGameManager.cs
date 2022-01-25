using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class KingOfTheHillGameManager : NetworkBehaviour
{
    [Header("Game Variables")]
    public float GameTime;
    private bool gameOngoing;

    [Header("Game Objects")]
    [SerializeField]
    private TextMeshProUGUI UITimer;

    [Header("Timer Variables")]
    private int Minute;
    private int Seconds;

    [Header("Player Spawning")]
    [SerializeField]
    List<GameObject> SpawnPoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
            return;

        gameOngoing = true;
        Minute = 0;
        Seconds = 0;

        //RegisterSpawnPoints();
    }

    public override void OnStartServer()
    {
        RegisterSpawnPoints();
    }

    void RegisterSpawnPoints()
    {
        for (int i = 0; i < SpawnPoints.Count; ++i)
        {
            NetworkManager.RegisterStartPosition(SpawnPoints[i].transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;

        if (gameOngoing)
        {
            GameTime -= Time.deltaTime * 1;
            ConvertToMinutes(GameTime);
        }
    }

    [ClientRpc]
    void ConvertToMinutes(float timeInSeconds)
    {
        Minute = (int)timeInSeconds / 60;
        Seconds = (int)timeInSeconds % 60;

        if (Seconds >= 10)
            UITimer.text = Minute + ":" + Seconds;
        else
            UITimer.text = Minute + ":0" + Seconds;

        if(timeInSeconds <= 0)
        {
            UITimer.text = "Game Over";
            gameOngoing = false;
        }
    }
}
