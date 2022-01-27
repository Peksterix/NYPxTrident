using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Bamboo.WGT
{
    public class WGTPlayerData : NetworkBehaviour
    {
        // You should do ur relevant syncvar hooks here
        // i.e. when playerHP changes, it gets instance to the UI manager to change the material or smth
        // i.e. when playerPoints changes, it gets instance to the UI manager to change the score text


        [Header("Networked Properties")]
        [SyncVar] public int playerPoints;
        [SyncVar] public float playerHP;
        [SyncVar] public float playerAmmo;
        [SyncVar] public bool isPlayerCatcher;
    }
}
