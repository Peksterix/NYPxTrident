using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WGTRework
{
    public partial class WGTPlayerController
    {
        void OnPlayerHpChange(float oldVal, float newVal)
        {
            if (isPlayerCatcher) return;

            playerHP = newVal;
            playerSprite.color = new Color(
                    1.0f - (1.0f * ((float)playerMaxHP - (float)playerHP) / playerMaxHP),
                    1.0f - (1.0f * ((float)playerMaxHP - (float)playerHP) / playerMaxHP),
                    1.0f,
                    1
                    );
        }

        void OnAmmoCountChange(float oldVal, float newVal)
        {
            playerAmmo = newVal;

            if (!isLocalPlayer) return;
            WGTUIManager.Instance.UpdateWaterGaugeFill(newVal / playerMaxAmmo);
        }

        void OnPlayerIsShooting(bool oldVal, bool newVal)
        {
            isCurrentlyShooting = newVal;
            if (isCurrentlyShooting != waterGunParticleSystem.isPlaying)
                if (isCurrentlyShooting) waterGunParticleSystem.Play();
                else waterGunParticleSystem.Stop();
        }

        void OnPlayerIsCatcher(bool oldVal, bool newVal)
        {
            isPlayerCatcher = newVal;

            playerSprite.color = new Color(
                    1.0f - (1.0f * ((float)playerMaxHP - (float)playerHP) / playerMaxHP),
                    1.0f - (1.0f * ((float)playerMaxHP - (float)playerHP) / playerMaxHP),
                    1.0f,
                    1
                    );

            playerSprite.color = newVal ? Color.red : playerSprite.color;
        }

        void OnPlayerPointsChange(int oldVal, int newVal)
        {
            playerPoints = newVal;

            if (!isLocalPlayer) return;
            WGTUIManager.Instance.OnLocalPlayerScored(playerPoints);
        }

        //void OnPlayerIsChargingWater(bool oldVal, bool newVal)
        //{
        //    isCurrentlyChargingWater = newVal;
        //}

        //void OnPlayerIsAbleToChargeWater(bool oldVal, bool newVal)
        //{
        //    isAbleToChargeWater = newVal;
        //}
    }
}
