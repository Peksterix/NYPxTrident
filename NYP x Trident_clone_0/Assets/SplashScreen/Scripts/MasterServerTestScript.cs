using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MasterServerTestScript : MonoBehaviour
{
    [Header("Server Stuff")]
    [SerializeField] TMP_InputField ServerGameModeInput;
    [SerializeField] TMP_Text ServerNetworkAddressText;
    [SerializeField] TMP_Text ServerReturnedCode;
    [SerializeField] Button RegisterServerButton;

    [Header("Client Stuff")]
    [SerializeField] TMP_InputField ClientCodeInput;
    [SerializeField] TMP_InputField ClientGameModeInput;
    [SerializeField] TMP_Text ClientReturnedNetworkAddress;
    [SerializeField] Button SendCodeButton;

    void Start()
    {
        MasterServerCommunicator.Instance.OnServerRegistered.AddListener(OnRegisterServer);
        MasterServerCommunicator.Instance.OnClientGetNetworkAddress.AddListener(OnGetCode);
        MasterServerCommunicator.Instance.OnClientGetNetworkAddressFail.AddListener(Fail);
        MasterServerCommunicator.Instance.OnServerRegisteredFail.AddListener(Fail);

        RegisterServerButton.onClick.AddListener(()=>MasterServerCommunicator.Instance.RegisterServer(int.Parse(ServerGameModeInput.text)));
        SendCodeButton.onClick.AddListener(()=>MasterServerCommunicator.Instance.CodeToServer(ClientCodeInput.text, int.Parse(ClientGameModeInput.text)));
    }

    void OnDestroy()
    {
        MasterServerCommunicator.Instance.OnServerRegistered.RemoveListener(OnRegisterServer);
        MasterServerCommunicator.Instance.OnClientGetNetworkAddress.RemoveListener(OnGetCode);
        MasterServerCommunicator.Instance.OnClientGetNetworkAddressFail.RemoveListener(Fail);
        MasterServerCommunicator.Instance.OnServerRegisteredFail.RemoveListener(Fail);
    }

    void OnRegisterServer(string code)
    {
        ServerNetworkAddressText.text = MasterServerCommunicator.Instance.ServerIP;
        ServerReturnedCode.text = code;
    }

    void OnGetCode(string networkAddress)
    {
        ClientReturnedNetworkAddress.text = networkAddress;
    }

    void Fail(string error, string code)
    {
        Debug.LogError(string.Format("Error: {0}, Message: {1}", error, code));
    }
}
