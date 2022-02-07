using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class LocalPlayerDataManager : Singleton<LocalPlayerDataManager>
{
    public string PlayerName;

    protected override void OnAwake()
    {
        base.OnAwake();
        _persistent = true;
    }
}
