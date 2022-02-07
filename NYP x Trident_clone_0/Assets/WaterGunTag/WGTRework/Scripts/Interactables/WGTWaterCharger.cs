using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;
using TMPro;
using Mirror;
using DG.Tweening;
using System.Security.Cryptography;
using UnityEngine.Events;

namespace WGTRework
{ 
    public class WGTWaterCharger : MonoBehaviour
    {
        [SerializeField] float refillRate;

        void Start()
        {
            if (!NetworkServer.active)
            {
                Destroy(this);
                return;
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                WGTPlayerController otherPlayerController = other.GetComponent<WGTPlayerController>();
                otherPlayerController.RefillAmmo(refillRate);
                //otherPlayerController.CmdAbleToChargeWater();
            }
        }
    }
}
