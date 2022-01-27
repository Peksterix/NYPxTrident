using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Bamboo.WGT
{ 
    public class WGTPlayerController : NetworkBehaviour
    {
        [Header("Cosmetic")]
        [SerializeField] Transform playerGun;
        [SerializeField] float gunDistance = 1.2f;

        [Header("Movement")]
        [SerializeField] CharacterController characterController;
        [SerializeField] float rotationSmoothTIme = 0.05f;
        [SerializeField] float rotationSpeed = 1.0f;
        [SerializeField] float movementSpeed = 8.0f;
        [SerializeField] float minSpeedDistance = 0.0f;
        [SerializeField] float maxSpeedDistance = 2.4f;
        [SerializeField] float playerToMouseDistance;

        public override void OnStartServer()
        {
            base.OnStartServer();
        }


        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            CameraManager.Instance.AssignCameraTarget(transform, transform);
        }

        public void Update()
        {
            if (!isLocalPlayer && netIdentity.connectionToServer != null) return;
            if (!Camera.main) return;

            RotatePlayer();
            MovePlayer();
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
            playerGun.transform.position = transform.position + (transform.forward * gunDistance);

            Vector3 direction = playerGun.transform.position - transform.position;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            playerGun.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));
        }

        private void MovePlayer()
        {
            float currSpeed = Mathf.Lerp(minSpeedDistance, maxSpeedDistance, playerToMouseDistance / maxSpeedDistance);
            characterController.Move(transform.forward * currSpeed * Time.deltaTime);
        }
    }
}

