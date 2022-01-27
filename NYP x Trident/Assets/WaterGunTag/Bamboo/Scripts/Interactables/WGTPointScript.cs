using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Bamboo.WGT
{
    public class WGTPointScript : NetworkBehaviour
    {
        [SerializeField] [SyncVar(hook = nameof(OnPointHpChange))] public float pointHP;
        [SerializeField] [SyncVar(hook = nameof(OnPointMaxHpChange))] public float pointMaxHP;
        [SerializeField] ParticleSystem particleSystem;
        [SerializeField] float pointToHpScale = 1.0f;


        [Server]
        public void Init(float minPoints, float maxPoints, float val)
        {
            pointHP = val;
            pointMaxHP = val;
            particleSystem.gameObject.SetActive(val > minPoints + (maxPoints * 0.5f));
        }

        void OnPointHpChange(float oldVal, float newVal)
        {
            pointHP = newVal;

            this.GetComponent<MeshRenderer>().material.color = new Color(
                  1.0f - (1.0f * ((float)pointMaxHP - (float)pointHP) / pointMaxHP),
                  1.0f - (1.0f * ((float)pointMaxHP - (float)pointHP) / pointMaxHP),
                  1.0f,
                  1
                  ) * Color.yellow;
        }

        void OnPointMaxHpChange(float oldVal, float newVal)
        {
            pointMaxHP = newVal;

            this.GetComponent<MeshRenderer>().material.color = new Color(
                  1.0f - (1.0f * ((float)pointMaxHP - (float)pointHP) / pointMaxHP),
                  1.0f - (1.0f * ((float)pointMaxHP - (float)pointHP) / pointMaxHP),
                  1.0f,
                  1
                  ) * Color.yellow;
        }

        [Server]
        public void TakeDamage(float damage, WGTPlayerController player)
        {
            pointHP -= damage;
            if (pointHP <= 0)
            {
                player.AddPoints(pointMaxHP * pointToHpScale);
                NetworkServer.Destroy(gameObject);
            }
        }
    }
}
