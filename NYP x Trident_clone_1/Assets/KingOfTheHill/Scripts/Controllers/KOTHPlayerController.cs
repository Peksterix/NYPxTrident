using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KOTHPlayerController : NetworkBehaviour
{
    private float playerSpeed = 2f;
    private float jumpHeight = 1f;
    public CharacterController characterController;
    private float Horizontal;
    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private float gravity = -9.81f;
    private bool beingSprayed;

    private void OnValidate()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        characterController.enabled = false;
    }

    public override void OnStartLocalPlayer()
    {
        characterController.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        beingSprayed = false;
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


        playerVelocity.y += gravity * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        Vector3 move = new Vector3(Horizontal, 0, 0);
        characterController.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
            gameObject.transform.forward = move;

        characterController.Move(playerVelocity * Time.deltaTime);
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
