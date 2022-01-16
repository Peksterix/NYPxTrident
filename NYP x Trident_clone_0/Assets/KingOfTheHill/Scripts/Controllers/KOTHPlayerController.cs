using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KOTHPlayerController : NetworkBehaviour
{
    private float playerSpeed = 6f;
    private float jumpHeight = 8f;
    public CharacterController characterController;
    private float Horizontal;
    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private float gravity = -4.9f;
    private bool beingSprayed;
    private float zPosition;
    private Vector3 movementOffSet;

    private void OnValidate()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        characterController.enabled = false;
    }

    public override void OnStartLocalPlayer()
    {
        CameraManager.Instance.AssignCameraTarget(this.transform, this.transform);
        characterController.enabled = true;
        beingSprayed = false;
        zPosition = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        groundedPlayer = characterController.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        Horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && groundedPlayer)
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);

        if (Input.GetButtonDown("Fire1"))
            CmdShoot();
        else if (Input.GetButtonUp("Fire1"))
            CmdStopShoot();

        playerVelocity.y += gravity * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        if (transform.position.z != zPosition)
        {
            movementOffSet.z = (zPosition - transform.position.z) * 0.05f;
        }
        characterController.Move(movementOffSet);


        Vector3 move = new Vector3(Horizontal, 0, 0);
        characterController.Move(move * Time.fixedDeltaTime * playerSpeed);

        if (move != Vector3.zero)
            gameObject.transform.forward = move;

        characterController.Move(playerVelocity * Time.fixedDeltaTime);
    }

    public void PushBack(float force, Vector3 dir)
    {
        //GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.VelocityChange);
        characterController.Move(dir * Time.deltaTime * force);
    }

    public void SetIsBeingSprayed(bool bs)
    {
        beingSprayed = bs;
    }

    [Command]
    public void CmdShoot()
    {
        RPCShoot();
    }

    [Command]
    public void CmdStopShoot()
    {
        RPCStopShoot();
    }

    [ClientRpc]
    public void RPCShoot()
    {
        this.GetComponentInChildren<ParticleSystem>().Play();
    }

    [ClientRpc]
    public void RPCStopShoot()
    {
        this.GetComponentInChildren<ParticleSystem>().Stop();
    }
}
