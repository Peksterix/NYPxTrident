using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Utility;
using UnityEngine.Events;

public sealed class LocalPlayerHandle : Singleton<LocalPlayerHandle>
{
    public string playerName;

    private void Start()
    {
        playerName = LocalPlayerDataManager.Instance.PlayerName;
    }
}