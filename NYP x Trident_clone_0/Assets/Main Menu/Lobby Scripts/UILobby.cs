using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILobby : MonoBehaviour
{
    public static UILobby instance;

    [Header("Gamemode Select")]
    [SerializeField]
    GameObject gamemodeSelectCanvas;

    [Header("Host & Join")]
    [SerializeField] 
    TMP_InputField usernameInput;
    [SerializeField] 
    TMP_InputField joinMatchInput;
    [SerializeField]
    Button joinButton;
    [SerializeField]
    Button hostButton;
    [SerializeField]
    GameObject lobbyCanvas;
    [SerializeField]
    GameObject connectionCanvas;

    [Header("Lobby")]
    [SerializeField]
    Transform UIPlayerParent;
    [SerializeField]
    GameObject UIPlayerPrefab;
    [SerializeField]
    TextMeshProUGUI matchIDText;
    [SerializeField]
    GameObject StartGameButton;
    [SerializeField]
    TextMeshProUGUI LobbyGamemodeHeader;

    GameObject localPlayerLobbyUI;
    Match.MatchType selectedMatchType;

    private void Awake()
    {
        instance = this;
        this.gameObject.SetActive(true);
    }

    public void Host()
    {
        joinMatchInput.interactable = false;
        joinButton.interactable = false;
        hostButton.interactable = false;

        LobbyPlayer.localPlayer.HostGame(usernameInput.text, selectedMatchType);
    }

    public void HostSuccess(bool success, string matchID, Match.MatchType MatchGamemode)
    {
        if(success)
        {
            lobbyCanvas.SetActive(true);
            connectionCanvas.SetActive(false);

            if (localPlayerLobbyUI != null) 
                Destroy(localPlayerLobbyUI);
            localPlayerLobbyUI = SpawnPlayerUIPrefab(LobbyPlayer.localPlayer);

            matchIDText.text = matchID;
            StartGameButton.SetActive(true);

            switch(MatchGamemode)
            {
                case Match.MatchType.KOTH:
                    LobbyGamemodeHeader.text = "King Of The Hill";
                    break;
                case Match.MatchType.WGT:
                    LobbyGamemodeHeader.text = "Water Gun Tag";
                    break;
                case Match.MatchType.TH:
                    LobbyGamemodeHeader.text = "Treasure Hunt";
                    break;
            }    
        }
         
        else
        {
            joinMatchInput.interactable = true;
            joinButton.interactable = true;
            hostButton.interactable = true;
        }
    }

    public void Join()
    {
        joinMatchInput.interactable = false;
        joinButton.interactable = false;
        hostButton.interactable = false;

        LobbyPlayer.localPlayer.JoinGame(joinMatchInput.text.ToUpper(), usernameInput.text, selectedMatchType);
    }

    public void JoinSuccess(bool success, string matchID, Match.MatchType currGamemode)
    {
        if (success)
        {
            lobbyCanvas.SetActive(true);
            connectionCanvas.SetActive(false);

            if (localPlayerLobbyUI != null)
                Destroy(localPlayerLobbyUI);
            localPlayerLobbyUI = SpawnPlayerUIPrefab(LobbyPlayer.localPlayer);
            
            matchIDText.text = matchID;

            switch (currGamemode)
            {
                case Match.MatchType.KOTH:
                    LobbyGamemodeHeader.text = "King Of The Hill";
                    break;
                case Match.MatchType.WGT:
                    LobbyGamemodeHeader.text = "Water Gun Tag";
                    break;
                case Match.MatchType.TH:
                    LobbyGamemodeHeader.text = "Treasure Hunt";
                    break;
            }
        }

        else
        {
            joinMatchInput.interactable = true;
            joinButton.interactable = true;
            hostButton.interactable = true;
        }
    }

    public GameObject SpawnPlayerUIPrefab(LobbyPlayer player)
    {
        GameObject newUIPlayer = Instantiate(UIPlayerPrefab, UIPlayerParent);
        newUIPlayer.GetComponent<UIPlayer>().SetPlayer(player);
        newUIPlayer.transform.SetSiblingIndex(player.playerIndex - 1);

        return newUIPlayer;
    }

    public void BeginGame()
    {
        LobbyPlayer.localPlayer.BeginGame();
    }

    public void SetSelectedMatchTypeKOTH()
    {
        selectedMatchType = Match.MatchType.KOTH;
        connectionCanvas.SetActive(true);
        gamemodeSelectCanvas.SetActive(false);
    }

    public void SetSelectedMatchTypeWGT()
    {
        selectedMatchType = Match.MatchType.WGT;
        connectionCanvas.SetActive(true);
        gamemodeSelectCanvas.SetActive(false);
    }

    public void SetSelectedMatchTypeTH()
    {
        selectedMatchType = Match.MatchType.TH;
        connectionCanvas.SetActive(true);
        gamemodeSelectCanvas.SetActive(false);
    }

    public void ResetSelectedMatchType()
    {
        selectedMatchType = Match.MatchType.NONE;
    }

    public void disableGamemodeSelectCanvas()
    {
        gamemodeSelectCanvas.SetActive(false);
    }

    public void enableGamemodeSelectCanvas()
    {
        gamemodeSelectCanvas.SetActive(true);
    }

    public void disableConnectionCanvas()
    {
        connectionCanvas.SetActive(false);
    }

    public void enableConnectionCanvas()
    {
        connectionCanvas.SetActive(true);
    }

}
