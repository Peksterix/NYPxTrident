using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class NetworkRoomManagerExt : NetworkRoomManager
{
    public enum GameType
    {
        KOTH = 0,
        WGT= 1,
        TH = 2,
        NONE = 3
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
    public string Username;

    [Header("Lobby UI")]
    public Canvas lobbyUI;

    /*
     * To include prefabs for all gamemodes.
     */
    [Header("Spawnable Prefabs")]
    public GameObject KOTHPlayerPrefab;
    public GameObject WGTPlayerPrefab;
    public GameObject THPlayerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
    {
        if (gameType == GameType.KOTH)
        {
            gamePlayer.GetComponent<PlayerScore>().playerName = roomPlayer.GetComponent<NetworkRoomPlayerExt>().username;
            return base.OnRoomServerSceneLoadedForPlayer(conn, roomPlayer, gamePlayer);
        }
        else
            return base.OnRoomServerSceneLoadedForPlayer(conn, roomPlayer, gamePlayer);
    }

    public override void OnRoomServerPlayersReady()
    {
        base.OnRoomServerPlayersReady();
    }

    public void OnStartButtonClick()
    {
        if (mode != NetworkManagerMode.Host) return;

        switch (gameType)
        {
            case GameType.KOTH:
                playerPrefab = KOTHPlayerPrefab;
                lobbyUI.enabled = false;
                ServerChangeScene(KOTHGameScene);
                break;
            case GameType.WGT:
                playerPrefab = WGTPlayerPrefab;
                lobbyUI.enabled = false;
                ServerChangeScene(WGTGameScene);
                break;
            case GameType.TH:
                playerPrefab = THPlayerPrefab;
                lobbyUI.enabled = false;
                ServerChangeScene(THGameScene);
                break;
            default:
                break;
        }
    }

    public override void OnClientChangeScene(string newSceneName, SceneOperation sceneOperation, bool customHandling)
    {
        base.OnClientChangeScene(newSceneName, sceneOperation, customHandling);
        
        // Switch case doesnt work for some reason, but if statements do.

        //switch (newSceneName)
        //{
        //    case KOTHGameScene:
        //        lobbyUI.enabled = false;
        //        break;
        //    case "WGTGameScene":
        //        lobbyUI.enabled = false;
        //        break;
        //    case "THGameScene":
        //        lobbyUI.enabled = false;
        //        break;
        //    default:
        //        break;
        //}

        if(newSceneName == KOTHGameScene)
        {
            playerPrefab = KOTHPlayerPrefab;
            lobbyUI.enabled = false;
        }
        if(newSceneName == WGTGameScene)
        {
            playerPrefab = WGTPlayerPrefab;
            lobbyUI.enabled = false;
        }
        if(newSceneName == THGameScene)
        {
            playerPrefab = THPlayerPrefab;
            lobbyUI.enabled = false;
        }
    }
}
