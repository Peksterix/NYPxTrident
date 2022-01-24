using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KOTHPlayerController : NetworkBehaviour
{
    // jamming everything in here cos i cant really be bothered anymore lmao

    private float playerSpeed = 6f;
    private float jumpHeight = 2f;
    public CharacterController characterController;
    private float Horizontal;
    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private float gravity = -12f;
    private bool beingSprayed;
    private float zPosition;
    private Vector3 movementOffSet;
    [SyncVar]
    private bool hasWaterbomb;

    public GameObject powerupImage;
    public GameObject waterbombPrefab;

    private void OnValidate()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        characterController.enabled = false;
    }

    public override void OnStartLocalPlayer()
    {
        powerupImage = GameObject.Find("Canvas/AdditionalUI/PowerupUI/PowerupImage");
        CameraManager.Instance.AssignCameraTarget(this.transform, this.transform);
        characterController.enabled = true;
        beingSprayed = false;
        zPosition = transform.position.z;
        transform.rotation = Quaternion.Euler(0, 90, 0);
        hasWaterbomb = false;
        characterController.detectCollisions = false;
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

        if (Horizontal < 0)
            gameObject.transform.localScale = new Vector3(-0.0001f, 1, 1);
        else if (Horizontal > 0)
            gameObject.transform.localScale = new Vector3(0.0001f, 1, 1);


        if (Input.GetButtonDown("Jump") && groundedPlayer)
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);

        if (Input.GetButtonDown("Fire1"))
            CmdShoot();
        else if (Input.GetButtonUp("Fire1"))
            CmdStopShoot();

        // Remove later
        if (Input.GetKeyDown(KeyCode.Q))
            CmdToggleWaterBomb();

        if (hasWaterbomb)
        {
            powerupImage.SetActive(true);

            if (Input.GetButton("Fire2"))
            {
                CmdThrowWaterbomb();
            }
        }
        else
            powerupImage.SetActive(false);

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
        playerVelocity.y += gravity * Time.fixedDeltaTime;
    }

    public void PushBack(float force, Vector3 dir)
    {
        //GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.VelocityChange);
        characterController.Move(dir * Time.fixedDeltaTime * force);
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

    public void ThrowWaterBomb()
    {
        Vector3 offset;

        if (gameObject.transform.localScale.x < 0)
            offset = new Vector3(-0.3f, 0, 0);
        else
            offset = new Vector3(0.3f, 0, 0);

        GameObject newWaterbomb = Instantiate(waterbombPrefab, GetComponentInChildren<ParticleSystem>().gameObject.transform.position + offset, Quaternion.identity);
        NetworkServer.Spawn(newWaterbomb);
        newWaterbomb.GetComponent<Rigidbody>().AddForce(15 * transform.forward.normalized, ForceMode.VelocityChange);
        hasWaterbomb = false;
    }

    //[ClientRpc]
    //public void RpcThrowWaterBomb()
    //{
    //    ThrowWaterBomb();
    //}

    [Command]
    public void CmdThrowWaterbomb()
    {
        if(hasWaterbomb)
            ThrowWaterBomb();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerups") && isLocalPlayer && !hasWaterbomb)
        {
            CmdToggleWaterBomb();
            NetworkServer.Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("OutOfBounds") && isLocalPlayer)
            MoveToSpawnPoint();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Platforms") && isLocalPlayer)
        {
            if (other.gameObject.GetComponentInParent<MoveablePlatformController>().isHighestPlatform)
                GetComponent<PlayerScore>().playerScore++;
        }
    }

    void MoveToSpawnPoint()
    {
        int rand = Random.Range(0, 4);

        characterController.enabled = false;
        transform.position = KOTHSpawnManager.Instance.SpawnPoints[rand].transform.position;
        GetComponent<NetworkTransform>().CmdTeleport(KOTHSpawnManager.Instance.SpawnPoints[rand].transform.position);
        characterController.enabled = true;
    }

    [Command]
    void CmdToggleWaterBomb()
    {
        hasWaterbomb = true;
    }
}
