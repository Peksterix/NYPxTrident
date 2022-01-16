using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Platform
{
    public enum PlatformType
    {
        Main,
        Normal,
        Special
    }

    public GameObject platform;
    public float platformLifetime;
    public float specialPlatformLifetime;
}

public class MoveablePlatformManager : NetworkBehaviour
{
    public GameObject platformParent;
    public float PlatformLowestX;
    public float PlatformHighestX;
    private float platformXOffset;
    public float PlatformLowestY;
    public float PlatformHighestY;

    private float timeElapsed;
    private bool attemptingToSpawnPlatform;
    public GameObject PlatformToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
            return;

        timeElapsed = 10f;
        attemptingToSpawnPlatform = false;
        platformXOffset = PlatformToSpawn.transform.localScale.x / 2; // divide by 2 because (0, 0) is at the center of the object
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;

        if (timeElapsed >= 0f)
            timeElapsed -= Time.deltaTime * 1;

        else
        {
            if (platformParent.transform.childCount <= 15 && !attemptingToSpawnPlatform)
                SpawnMoveablePlatforms();
        }
    }

    void SpawnMoveablePlatforms()
    {
        int safetyNet = 20;

        while (safetyNet > 0)
        {
            float posY = GetRandomTargetPlatformHeight();
            float posX = GetRandomTargetPlatformWidth();
            if (!HasOverlappingPlatform(posX, posY))
            {
                GameObject plat =
                Instantiate(PlatformToSpawn, new Vector3(posX, posY, 0),
                Quaternion.identity,
                platformParent.transform);

                plat.GetComponent<MoveablePlatformController>().Init(posY);
                NetworkServer.Spawn(plat);
                attemptingToSpawnPlatform = false;
                break;
            }

            safetyNet--;
        }

        timeElapsed = 10f;

        if (safetyNet <= 0)
            Debug.Log("Could not find with no overlap");


        //attemptingToSpawnPlatform = true;
        //float randomTargetWidth = GetRandomTargetPlatformWidth();
        //int overlappingPlatformCounter = 0;
        //bool platformSpawned = false;

        //while (!platformSpawned)
        //{
        //    float Height = 0.0f;

        //    // To compare X pos of the new platform to all current active platforms
        //    for (int i = 0; i < platformParent.transform.childCount; ++i)
        //    {
        //        if (platformParent.transform.childCount == 0)
        //            break;

        //        if (CheckOverlappingPlatform(platformParent.transform.GetChild(i), randomTargetWidth))
        //        {
        //            overlappingPlatformCounter++;
        //        }
        //    }

        //    if (overlappingPlatformCounter == 0) // platform is able to spawn
        //    {
        //        GameObject plat =
        //        Instantiate(PlatformToSpawn, new Vector3(randomTargetWidth, PlatformToSpawn.transform.position.y,
        //        PlatformToSpawn.transform.position.z),
        //        PlatformToSpawn.transform.rotation,
        //        platformParent.transform);
        //        plat.GetComponent<MoveablePlatformController>().Init(Height);
        //        NetworkServer.Spawn(plat);
        //        timeElapsed = 10f;
        //        attemptingToSpawnPlatform = false;
        //        platformSpawned = true;
        //    }
        //}
    }

    public float GetRandomTargetPlatformWidth()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        return Random.Range(PlatformLowestX, PlatformHighestX);
    }

    bool HasOverlappingPlatform(float ToSpawnPlatformX, float targetHeight)
    {
        RaycastHit hitRight;
        RaycastHit hitLeft;
        bool isHitLeft = false;
        bool isHitRight = false;
        isHitLeft = Physics.Raycast(new Vector3(ToSpawnPlatformX, targetHeight, 0), Vector3.left, out hitLeft, Mathf.Infinity, LayerMask.GetMask("PlatformRaycast"));
        isHitRight = Physics.Raycast(new Vector3(ToSpawnPlatformX, targetHeight, 0), Vector3.right, out hitRight, Mathf.Infinity, LayerMask.GetMask("PlatformRaycast"));

        return isHitLeft || isHitRight;

        // is overlapping an existing platform
        //if(ToSpawnPlatformX + platformXOffset >= ExistingPlatform.position.x - platformXOffset 
        //    && ToSpawnPlatformX - platformXOffset <= ExistingPlatform.position.x + platformXOffset)
        //{
        //    return true;
        //}

        // not overlapping an existing platform
    }

    public float GetRandomTargetPlatformHeight()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        return Random.Range(PlatformLowestY, PlatformHighestY);
    }
}
