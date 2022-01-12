using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class NetworkRoomPlayerExt : NetworkRoomPlayer
{
    GameObject newPlayerUI;

    [SyncVar(hook = nameof(NameChange))]
    string username = "defaultName";

    public override void OnStartClient()
    {
        if (newPlayerUI == null)
            newPlayerUI = RoomUIManager.Instance.SpawnPlayerUIPrefab();

        if (isLocalPlayer)
        {
            this.gameObject.AddComponent<LocalPlayerHandle>();
            CmdchangeName(LocalPlayerDataManager.Instance.PlayerName);
        }

        newPlayerUI.GetComponentInChildren<TMP_Text>().text = username;
    }

    void NameChange(string oldName, string newName)
    {
        //if (newPlayerUI == null)
        //    newPlayerUI = RoomUIManager.Instance.SpawnPlayerUIPrefab();

        username = newName;
        newPlayerUI.GetComponentInChildren<TMP_Text>().text = newName;
    }

    [Command]
    void CmdchangeName(string newName)
    {
        username = newName;
    }
}
