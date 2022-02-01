using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MoveablePlatformController : NetworkBehaviour
{
    private ParticleSystem Waterspout;
    private float speed;
    private Transform platform;
    private float platformTargetHeight;
    [SyncVar]
    public bool isHighestPlatform;
    [SyncVar]
    public float platformLifetime;

    public void Init(float height)
    {
        speed = 2f;
        platform = transform.GetChild(0).gameObject.transform;

        platformLifetime = 30;
        Waterspout = transform.GetComponentInChildren<ParticleSystem>();
        Waterspout.Play();
        platformTargetHeight = height;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHighestPlatform)
            transform.GetChild(0).GetComponentInChildren<ParticleSystem>().Play();
        else
            transform.GetChild(0).GetComponentInChildren<ParticleSystem>().Stop();

        //platformLifetime -= Time.deltaTime * 1;

        //if (platformLifetime <= 0)
        //    NetworkServer.Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (!isServer)
            return;

        if (platform.position.y < platformTargetHeight)
            platform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
    }



    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
