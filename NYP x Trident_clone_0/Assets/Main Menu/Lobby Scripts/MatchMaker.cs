using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Security.Cryptography;
using System.Text;

[System.Serializable]
public class Match
{
    public enum MatchType
    {
        NONE,
        KOTH,
        WGT,
        TH
    }

    public string matchID;
    public List<LobbyPlayer> players = new List<LobbyPlayer>();
    public MatchType matchType;

    public Match(string matchID, LobbyPlayer player, MatchType matchType)
    {
        this.matchID = matchID;
        players.Add(player);
        this.matchType = matchType;
    }

    public Match() { }

    public MatchType GetMatchType()
    {
        return matchType;
    }
}

public class MatchMaker : NetworkBehaviour
{
    public static MatchMaker instance;

    public SyncList<Match> matches = new SyncList<Match>();
    public SyncList<string> matchIDs = new SyncList<string>();

    [SerializeField]
    GameObject KOTHManagerPrefab;

    private void Awake()
    {
        instance = this;
    }

    public bool HostGame(string _matchID, LobbyPlayer _player, out int playerIndex, Match.MatchType _matchType)
    {
        playerIndex = -1;
        if (!matchIDs.Contains(_matchID))
        {
            matchIDs.Add(_matchID);
            matches.Add(new Match(_matchID, _player, _matchType));
            Debug.Log($"Match Created");
            playerIndex = 1;
            return true;
        }

        else
        {
            Debug.Log($"Match ID already exists");
            return false;
        }
    }
    
    public bool JoinGame(string _matchID, LobbyPlayer _player, out int playerIndex, Match.MatchType _matchType)
    {
        playerIndex = -1;
        if (matchIDs.Contains(_matchID))
        {
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].matchID == _matchID)
                {
                    if (matches[i].matchType != _matchType)
                    {
                        Debug.Log($"Match Type does not match!");
                        return false;
                    }

                    matches[i].players.Add(_player);
                    playerIndex = matches[i].players.Count;
                    break;
                }
            }
            Debug.Log($"Match Joined");
            return true;
        }

        else
        {
            Debug.Log($"Match ID does not exist");
            return false;
        }
    }

    public void BeginGame(string _matchID)
    {
        GameObject newGameManager = Instantiate(KOTHManagerPrefab);
        NetworkServer.Spawn(newGameManager);
        newGameManager.GetComponent<NetworkMatch>().matchId = _matchID.ToGuid();
        KOTHNetworkGamestateManager KOTHManager = newGameManager.GetComponent<KOTHNetworkGamestateManager>();

        for(int i = 0; i < matches.Count; i++)
        {
            if(matches[i].matchID == _matchID)
            {
                foreach(var player in matches[i].players)
                {
                    LobbyPlayer _player = player.GetComponent<LobbyPlayer>();
                    KOTHManager.AddPlayer(_player);
                    _player.StartGame();
                }
                break;
            }
        }
    }

    public static string GetRandomMatchID()
    {
        string _id = string.Empty;

        for(int i = 0; i < 5; i++)
        {
            int random = UnityEngine.Random.Range(0, 36);

            if (random < 26) // 26 and below are alphabets in ASCII
                _id += (char)(random + 65); // adding 65 to capitalize the letter (ASCII)
            else
                _id += (random - 26).ToString();
        }
        Debug.Log($"Random Match ID: {_id}");
        return _id;
    }
}

public static class MatchExtensions
{
    public static Guid ToGuid(this string id)
    {
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        byte[] inputBytes = Encoding.Default.GetBytes(id);
        byte[] hashBytes = provider.ComputeHash(inputBytes);

        return new Guid(hashBytes);
    }
}