using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Bamboo.WGT
{
    public partial class WGTPlayerController : NetworkBehaviour
    {
        // Server-only data
        static WGTPlayerController wgtCatcher = null;

        [Header("Cosmetic")]
        [SerializeField] ParticleSystem waterGunParticleSystem;
        [SerializeField] public Transform playerGunTransform;
        [SerializeField] public Transform playerSpriteTransform;
        [SerializeField] SpriteRenderer playerSprite;
        [SerializeField] float gunDistance = 1.2f;

        [Header("Movement")]
        [SerializeField] Rigidbody characterController;
        [SerializeField] float rotationSmoothTIme = 0.05f;
        [SerializeField] float rotationSpeed = 1.0f;
        [SerializeField] float movementSpeed = 8.0f;
        [SerializeField] float minSpeedDistance = 0.0f;
        [SerializeField] float maxSpeedDistance = 2.4f;
        [SerializeField] float playerToMouseDistance;

        [Header("Predefined Values")]
        [SerializeField] float playerMaxHP = 5.0f;
        [SerializeField] float playerMaxAmmo = 20.0f;
        [SerializeField] float ammoConsumptionRate = 1.0f;
        [SerializeField] float playerStunDuration = 1.0f;

        [Header("Networked Properties")]
        [SyncVar(hook = nameof(OnPlayerPointsChange))] private int playerPoints;
        [SyncVar(hook = nameof(OnPlayerHpChange))] private float playerHP;
        [SyncVar(hook = nameof(OnAmmoCountChange))] private float playerAmmo;
        [SyncVar(hook = nameof(OnPlayerIsCatcher))] private bool isPlayerCatcher;
        [SyncVar(hook = nameof(OnPlayerIsShooting))] private bool isCurrentlyShooting;

        private bool canMove = false;
        private bool wasShooting = false;
        public bool IsPlayerCatcher => isPlayerCatcher;

        public override void OnStartServer()
        {
            base.OnStartServer();
            playerPoints = 0;
            playerHP = playerMaxHP;
            playerAmmo = playerMaxAmmo;
            isPlayerCatcher = false;
            isCurrentlyShooting = false;
        }

        void Start()
        {
            name = "Player: " + netIdentity.netId.ToString();
        }

        public void Update()
        {
            if ((!isLocalPlayer || !canMove)) return;
            if (!Camera.main) return;

            RotatePlayer();
            MovePlayer();
            HandleShooting();
        }

        void LateUpdate()
        {
            RotateGun();
        }

        private void RotatePlayer()
        {
            // Get Depth
            float depth = (Camera.main.transform.position - transform.position).magnitude;

            //Get the Screen positions of the object
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth));

            // Get Direction
            Vector3 direction = worldPos - transform.position;
            direction.Normalize();

            //Get the angle between the points
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationSmoothTIme);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            playerToMouseDistance = (worldPos - transform.position).magnitude;
        }

        private void RotateGun()
        {
            playerGunTransform.transform.position = transform.position + (transform.forward * gunDistance);

            Vector3 direction = playerGunTransform.transform.position - transform.position;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            playerGunTransform.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));
        }

        private void MovePlayer()
        {
            float currSpeed = Mathf.Lerp(minSpeedDistance, maxSpeedDistance, playerToMouseDistance / maxSpeedDistance);
            characterController.velocity = (transform.forward * currSpeed);
        }

        private void HandleShooting()
        {
            if (Input.GetMouseButton(0))
            {
                CmdShootWaterGun();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                CmdStopWaterGun();
            }
        }

        #region Server Only
        [ContextMenu("Uncatch")]
        public void UnCatch()
        {
            TurnPlayerIntoCatcher(false);
        }

        [Server]
        public void TurnPlayerIntoCatcher(bool isCatcher)
        {
            playerHP = playerMaxHP;
            playerAmmo = playerMaxAmmo;
            isPlayerCatcher = isCatcher;

            if (isCatcher)
            {
                if (wgtCatcher != null)
                    wgtCatcher.TurnPlayerIntoCatcher(false);

                wgtCatcher = this;
                StunPlayer(playerStunDuration);
            }
        }

        [Server]
        public void StunPlayer(float duration)
        {
            StartCoroutine(_StunPlayer(duration));

            IEnumerator _StunPlayer(float d)
            {
                GetComponent<NetworkTransform>().clientAuthority = false;
                RpcAllowMovement(false);
                yield return new WaitForSeconds(d);
                GetComponent<NetworkTransform>().clientAuthority = true;
                RpcAllowMovement(true);
                yield break;
            }
        }

        [Server]
        public void PlayerHit(float damage)
        {
            if (isPlayerCatcher) return;

            if (playerHP - damage > 0)
                playerHP -= damage;
            else
            {
                isPlayerCatcher = true;
                TurnPlayerIntoCatcher(true);
            }
        }

        [Server]
        public void AddPoints(float points)
        {
            playerPoints += Mathf.CeilToInt(points);
        }

        [Server]
        public void RefillAmmo(float refillRate)
        {
            playerAmmo += refillRate * Time.deltaTime;
            playerAmmo = Mathf.Min(playerAmmo, playerMaxAmmo);
        }
        #endregion

        #region RPCs
        [TargetRpc]
        public void RpcInitPlayer(bool isInitialising)
        {
            if (isInitialising && isLocalPlayer) CameraManager.Instance.AssignCameraTarget(transform, transform);
            else if (!isInitialising && isLocalPlayer) CameraManager.Instance.AssignCameraTarget(null, null);

            playerGunTransform.gameObject.SetActive(isInitialising);
            playerSpriteTransform.gameObject.SetActive(isInitialising);
            GetComponent<NetworkTransform>().clientAuthority = isInitialising;
            canMove = isInitialising;
        }

        [TargetRpc]
        public void RpcAllowMovement(bool isAllowed)
        {
            canMove = isAllowed;
        }
        #endregion

        #region Commands
        [Command]
        public void CmdShootWaterGun()
        {
            isCurrentlyShooting = true;
            if (playerAmmo - Time.deltaTime * ammoConsumptionRate >= 0)
            {
                playerAmmo -= Time.deltaTime * ammoConsumptionRate;
                if (!waterGunParticleSystem.isPlaying)
                    waterGunParticleSystem.Play();
            }
            else if (!waterGunParticleSystem.isPlaying)
            {
                waterGunParticleSystem.Stop();
            }
        }

        [Command]
        public void CmdStopWaterGun()
        {
            isCurrentlyShooting = false;
            waterGunParticleSystem.Stop();
        }
        #endregion
    }
}

