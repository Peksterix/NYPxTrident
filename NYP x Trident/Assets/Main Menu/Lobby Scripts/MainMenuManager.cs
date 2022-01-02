using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    Canvas MainMenuCanvas;
    [SerializeField]
    Canvas GamemodeSelectionCanvas;
    [SerializeField]
    Canvas ConnectionCanvas;

    [SerializeField]
    TMP_InputField UsernameInputField;
    [SerializeField]
    TMP_InputField MatchIDInputField;


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
    }

    /*
     * Server Functions
     */
    public void OnHostSuccess(string matchID)
    {
        NetworkRoomManagerExt.Instance.MatchID = matchID;
        NetworkRoomManagerExt.Instance.StartHost();
    }

    public void OnJoinSuccess(string networkAdddress)
    {
        NetworkRoomManagerExt.Instance.networkAddress = networkAdddress;
        NetworkRoomManagerExt.Instance.StartHost();
    }

    void Fail(string error, string code)
    {
        // TODO: make UI based on failed conditions, check Index in Master Server File
        //if(code == "GameModeMismatch")
        //{

        //}
        Debug.LogError(string.Format("Error: {0}, Message: {1}", error, code));
    }
}
