using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MoveablePlatformController : NetworkBehaviour
{
    private ParticleSystem Waterspout;
    private float speed;
    public float PlatformLowestY;
    public float PlatformHighestY;
    private Transform platform;
    private float platformRandY;

    public void Init(float height)
    {
        speed = 2f;
        platform = transform.GetChild(0).gameObject.transform;

        Waterspout = transform.GetComponentInChildren<ParticleSystem>();
        Waterspout.Play();
        platformRandY = height;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (platform.position.y < platformRandY)
            platform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
    }
}
