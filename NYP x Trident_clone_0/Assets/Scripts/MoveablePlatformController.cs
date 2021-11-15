using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MoveablePlatformController : MonoBehaviour
{
    private ParticleSystem Waterspout;
    private float speed;
    public float PlatformLowestY;
    public float PlatformHighestY;
    private GameObject platform;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        platform = transform.GetChild(0).gameObject;
        Waterspout = transform.GetComponentInChildren<ParticleSystem>();
        Waterspout.Play();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void FixedUpdate()
    {
        if (platform.transform.position.y < GetRandomTargetPlatformHeight())
            platform.transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
    }

    public float GetRandomTargetPlatformHeight()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        return Random.Range(PlatformLowestY, PlatformHighestY);
    }
}
