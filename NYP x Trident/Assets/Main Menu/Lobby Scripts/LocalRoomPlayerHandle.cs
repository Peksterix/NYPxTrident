using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Utility;
using UnityEngine.Events;

public class LocalRoomPlayerHandle : MonoBehaviour
{
    public string playerName;

    private void Start()
    {
        playerName = LocalPlayerDataManager.Instance.PlayerName;
    }
}
