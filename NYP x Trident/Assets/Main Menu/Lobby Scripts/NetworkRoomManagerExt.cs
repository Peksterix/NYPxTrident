using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkRoomManagerExt : NetworkRoomManager
{
    public enum GameType
    {
        KOTH = 0,
        WGT,
        TH
    }

    public static NetworkRoomManagerExt Instance => NetworkRoomManager.singleton as NetworkRoomManagerExt;

    [Header("Game Scenes")]
    [Scene]
    public string KOTHGameScene;
    [Scene]
    public string WGTGameScene;
    [Scene]
    public string THGameScene;

    public GameType gameType;
    public string MatchID;

    /*
     * To include prefabs for all gamemodes.
     */
    [Header("Spawnable Prefabs")]
    public GameObject placeholder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnRoomServerPlayersReady()
    {
        base.OnRoomServerPlayersReady();
    }
}
