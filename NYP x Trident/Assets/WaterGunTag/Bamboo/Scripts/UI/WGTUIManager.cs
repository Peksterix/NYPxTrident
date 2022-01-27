using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Bamboo.Utility;

namespace Bamboo.WGT
{
    public class WGTUIManager : Singleton<WGTUIManager>
    {
        [SerializeField] Text playerNameText;
        [SerializeField] Text playerPointText;

        void Start()
        {
            if (NetworkServer.active && !NetworkClient.active) Destroy(this);
            playerNameText.text = LocalPlayerDataManager.Instance.PlayerName;
        }

        public void OnLocalPlayerScored(int newAmount)
        {
            playerPointText.text = newAmount.ToString();
        }
    }
}
