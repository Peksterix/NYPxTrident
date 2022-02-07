using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Utility;

public class RoomUIManager : Singleton<RoomUIManager>
{
    [SerializeField]
    Transform RoomPlayerUIParent;
    [SerializeField]
    GameObject UIRoomPlayerPrefab;

    protected override void OnAwake()
    {
        _persistent = false;
        base.OnAwake();
    }

    private void Start()
    {
       
    }

    public GameObject SpawnPlayerUIPrefab()
    {
        GameObject newRoomPlayerUI = Instantiate(UIRoomPlayerPrefab, RoomPlayerUIParent);

        return newRoomPlayerUI;
    }
}
