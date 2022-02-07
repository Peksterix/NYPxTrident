using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] Canvas MainMenuCanvas;
    [SerializeField] Canvas GamemodeSelectionCanvas;
    [SerializeField] Canvas ConnectionCanvas;
    [SerializeField] TMP_InputField UsernameInputField;
    [SerializeField] TMP_InputField MatchIDInputField;
    [SerializeField] TextMeshProUGUI ConnectionResultText;


    private void Start()
    {
        MasterServerCommunicator.Instance.OnClientGetNetworkAddress.AddListener(OnJoinSuccess);
        MasterServerCommunicator.Instance.OnClientGetNetworkAddressFail.AddListener(Fail);

        MasterServerCommunicator.Instance.OnServerRegistered.AddListener(OnHostSuccess);
        MasterServerCommunicator.Instance.OnServerRegisteredFail.AddListener(Fail);
    }

    /*
     *  Main Menu Functions
    */
    public void StartButton()
    {
        MainMenuCanvas.enabled = false;
        GamemodeSelectionCanvas.enabled = true;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    /*
     *  Gamemode Select Functions
    */
    public void KOTHGamemodeSelect()
    {
        NetworkRoomManagerExt.Instance.gameType = NetworkRoomManagerExt.GameType.KOTH;
        GamemodeSelectionCanvas.enabled = false;
        ConnectionCanvas.enabled = true;
    }

    public void WGTGamemodeSelect()
    {
        NetworkRoomManagerExt.Instance.gameType = NetworkRoomManagerExt.GameType.WGT;
        GamemodeSelectionCanvas.enabled = false;
        ConnectionCanvas.enabled = true;
    }

    public void THGamemodeSelect()
    {
        NetworkRoomManagerExt.Instance.gameType = NetworkRoomManagerExt.GameType.TH;
        GamemodeSelectionCanvas.enabled = false;
        ConnectionCanvas.enabled = true;
    }

    public void GamemodeSelectBackButton()
    {
        NetworkRoomManagerExt.Instance.gameType = NetworkRoomManagerExt.GameType.NONE;
        GamemodeSelectionCanvas.enabled = false;
        MainMenuCanvas.enabled = true;
    }

    /*
     *  Connection Functions
    */
    public void Host()
    {
        MasterServerCommunicator.Instance.RegisterServer((int)NetworkRoomManagerExt.Instance.gameType);
    }

    public void Join()
    {
        MasterServerCommunicator.Instance.CodeToServer(MatchIDInputField.text, (int)NetworkRoomManagerExt.Instance.gameType);

        if (MatchIDInputField.text == "")
        {
            ConnectionResultText.gameObject.SetActive(true);
            ConnectionResultText.text = "Enter a match ID!";
        }
    }

    public void ConnectionBackButton()
    {
        UsernameInputField.text = "";
        MatchIDInputField.text = "";
        ConnectionCanvas.enabled = false;
        GamemodeSelectionCanvas.enabled = true;
    }

    /*
     * Server Functions
     */
    public void OnHostSuccess(string matchID)
    {
        NetworkRoomManagerExt.Instance.MatchID = matchID;
        LocalPlayerDataManager.Instance.PlayerName = UsernameInputField.text;
        NetworkRoomManagerExt.Instance.lobbyUI.enabled = true;
        NetworkRoomManagerExt.Instance.StartHost();
    }

    public void OnJoinSuccess(string networkAdddress)
    {
        NetworkRoomManagerExt.Instance.networkAddress = networkAdddress;
        NetworkRoomManagerExt.Instance.MatchID = MatchIDInputField.text;
        LocalPlayerDataManager.Instance.PlayerName = UsernameInputField.text;
        NetworkRoomManagerExt.Instance.lobbyUI.enabled = true;
        NetworkRoomManagerExt.Instance.StartClient();
    }

    void Fail(string error, string code)
    {
        // TODO: make UI based on failed conditions, check Index in Master Server File
        if (code == "GameModeMismatch")
        {
            ConnectionResultText.gameObject.SetActive(true);
            ConnectionResultText.text = "Gamemode does not match!";
        }
        if (code == "DoesNotExist")
        {
            ConnectionResultText.gameObject.SetActive(true);
            ConnectionResultText.text = "Could not find match with inputted ID";
        }
        if(code == "InvalidPayload")
        {
            ConnectionResultText.gameObject.SetActive(true);
            ConnectionResultText.text = "Could not connect to server";
        }

        Debug.LogError(string.Format("Error: {0}, Message: {1}", error, code));
    }
}
