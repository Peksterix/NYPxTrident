using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace Bamboo.WGT
{
    public class WGTPlayerUIHandle : NetworkBehaviour
    {
        [TargetRpc]
        public void BeginCountdown(float countdownTime, float timeCountdownStartedOnServer)
        {
            // Show countdown
            StartCoroutine(WGTUIManager.Instance.BeginCountdown(timeCountdownStartedOnServer, countdownTime));
        }

        [TargetRpc]
        public void BeginGameTime(float gameTime, float timeGameStartedOnServer)
        {
            StartCoroutine(WGTUIManager.Instance.BeginGameTimer(timeGameStartedOnServer, gameTime));
        }

        [TargetRpc]
        public void GameEnd()
        {
            WGTUIManager.Instance.GameEnd();
        }
    }
}