using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NetworkMatch))]
public class LobbyPlayer : NetworkBehaviour
{
    public static LobbyPlayer localPlayer;

    [SyncVar]
    public string matchID;
    [SyncVar]
    public int playerIndex;
    [SyncVar]
    public string username;

    [SerializeField]
    GameObject playerLobbyUI;

    NetworkMatch networkMatch;
    Guid netIDGuid;

    private void Awake()
    {
        networkMatch = GetComponent<NetworkMatch>();
    }

    public override void OnStartServer()
    {
        netIDGuid = netId.ToString().ToGuid();
        networkMatch.matchId = netIDGuid;
    }

    public override void OnStartClient()
    {
        if (isLocalPlayer)
        {
            localPlayer = this;
        }
        else
        {
            // Spawns other player's UI prefab
            playerLobbyUI = UILobby.instance.SpawnPlayerUIPrefab(this);
        }
    }

    /*
     * Host Match
     */

    public void HostGame(string _username, Match.MatchType _matchType)
    {
        string matchID = MatchMaker.GetRandomMatchID();
        CmdHostGame(matchID, _username, _matchType);
    }

    [Command]
    void CmdHostGame(string _matchID, string _username, Match.MatchType _matchType)
    {
        matchID = _matchID;
        username = _username;
        if(MatchMaker.instance.HostGame(_matchID, this, out playerIndex, _matchType))
        {
            Debug.Log($"Game hosted successfully!");
            networkMatch.matchId = _matchID.ToGuid();
            TargetHostGame(true, _matchID, playerIndex, username, _matchType);
        }
        else
        {
            Debug.Log($"Game hosted failed!");
            TargetHostGame(false, _matchID, playerIndex, username, _matchType);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool success, string _matchID, int _playerIndex, string _username, Match.MatchType _matchType)
    {
        this.playerIndex = _playerIndex;
        username = _username;
        matchID = _matchID;
        Debug.Log($"MatchID: {matchID} == {_matchID}");
        UILobby.instance.HostSuccess(success, matchID, _matchType);
    }

    /*
     * Join Match
     */
    public void JoinGame(string _inputID, string _nameInput, Match.MatchType _selectedMatchType)
    {
        CmdJoinGame(_inputID, _nameInput, _selectedMatchType);
    }

    [Command]
    void CmdJoinGame(string _matchID, string _nameInput, Match.MatchType _selectedMatchType)
    {
        matchID = _matchID;
        username = _nameInput;
        if(MatchMaker.instance.JoinGame(_matchID, this, out playerIndex, _selectedMatchType))
        {
            Debug.Log($"Game joined successfully!");
            networkMatch.matchId = _matchID.ToGuid();
            TargetJoinGame(true, _matchID, playerIndex, username, _selectedMatchType);

            if (isServer && playerLobbyUI != null)
                playerLobbyUI.SetActive(true);
        }
        else
        {
            Debug.Log($"Game joined failed!");
            TargetJoinGame(false, _matchID, playerIndex, username, _selectedMatchType);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string _matchID, int _playerIndex, string _nameInput, Match.MatchType matchType)
    {
        this.playerIndex = _playerIndex;
        matchID = _matchID;
        username = _nameInput;
        Debug.Log($"MatchID: {matchID} == {_matchID}");
        UILobby.instance.JoinSuccess(success, _matchID, matchType);
    }

    /*
     * Begin Match
     */

    public void BeginGame()
    {
        CmdBeginGame();
    }

    [Command]
    void CmdBeginGame()
    {
        MatchMaker.instance.BeginGame(matchID);
        Debug.Log($"Game Starting!");
        
    }

    public void StartGame(Match.MatchType _matchType)
    {
        TargetBeginGame(_matchType);
    }

    [TargetRpc]
    void TargetBeginGame(Match.MatchType _matchType)
    {
        Debug.Log($"MatchID: {matchID}");
        UILobby.instance.UnloadLobbyObjects();

        switch (_matchType)
        {
            case Match.MatchType.KOTH:
                SceneManager.LoadScene("KingOfTheHillGameScene", LoadSceneMode.Additive);
                break;
            case Match.MatchType.WGT:
                SceneManager.LoadScene("WaterGunTagPlayScene", LoadSceneMode.Additive);
                break;
            case Match.MatchType.TH:
                SceneManager.LoadScene("THGameScene", LoadSceneMode.Additive);
                break;
            default:
                break;
        }
    }
}
