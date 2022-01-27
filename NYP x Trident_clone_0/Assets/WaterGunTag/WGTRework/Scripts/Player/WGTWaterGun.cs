using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace WGTRework
{
    public class WGTWaterGun : MonoBehaviour
    {
        [SerializeField] WGTPlayerController owner;
        [SerializeField] ParticleSystem particleSystem;
        [SerializeField] float damage;

        public void Start()
        {
            if (!NetworkServer.active)
            {
                var collision = particleSystem.collision;
                collision.sendCollisionMessages = false;
            }
        }

        void LateUpdate()
        {
            transform.LookAt(transform.position + owner.transform.forward, Vector3.up);
        }

        private void OnParticleCollision(GameObject other)
        {
            // Returns if not local player
            // We are lazy, so instead of raycasting like a normal human being, we gonna let the clients handle the damage
            if (!NetworkServer.active) return;

            if (other.CompareTag("Player"))
            {
                WGTPlayerController otherPlayerController = other.GetComponent<WGTPlayerController>();
                if (owner.IsPlayerCatcher)
                    otherPlayerController.PlayerHit(damage);
            }

            if (other.CompareTag("Point") && !owner.IsPlayerCatcher)
            {
                WGTPointScript pointScript = other.GetComponent<WGTPointScript>();
                pointScript.TakeDamage(damage, owner);
            }
        }
    }
}
