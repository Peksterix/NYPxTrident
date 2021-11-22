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
    private NetworkManager NMinstance;
    private GameObject registeredPlatformPrefab;
    private float platformXOffset;

    private float timeElapsed;

    public override void OnStartServer()
    {
        NMinstance = NetworkManager.singleton;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 5f;
        platformXOffset = platformParent.transform.GetChild(1).localScale.x / 2; // divide by 2 because (0, 0) is at the center of the object

        foreach (GameObject platform in NMinstance.spawnPrefabs)
        {
            if (platform.name == "Moveable Platform")
                registeredPlatformPrefab = platform;     
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed >= 0f)
            timeElapsed -= Time.deltaTime * 1;

        else
        {
            if (platformParent.transform.childCount <= 7)
                SpawnMoveablePlatforms();
            timeElapsed = 5f;
        }
    }

    void SpawnMoveablePlatforms()
    {
        float randomTargetWidth = GetRandomTargetPlatformWidth();
        int overlappingPlatformCounter = 0;

        // To compare X pos of the new platform to all current active platforms
        // starting from 1 as the 0th index is the main platform
        for (int i = 1; i < platformParent.transform.childCount; ++i)
        {
            if (CheckOverlappingPlatform(platformParent.transform.GetChild(i), randomTargetWidth))
            {
                overlappingPlatformCounter++;
            }
        }

        if (overlappingPlatformCounter <= 0)
        {
            GameObject plat =
            Instantiate(registeredPlatformPrefab, new Vector3(randomTargetWidth, registeredPlatformPrefab.transform.position.y,
            registeredPlatformPrefab.transform.position.z),
            registeredPlatformPrefab.transform.rotation,
            platformParent.transform);
            NetworkServer.Spawn(plat);
        }

        else
            return;
    }

    public float GetRandomTargetPlatformWidth()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        return Random.Range(PlatformLowestX, PlatformHighestX);
    }

    bool CheckOverlappingPlatform(Transform ExistingPlatform, float ToSpawnPlatformX)
    {
        // is overlapping an existing platform
        if(ToSpawnPlatformX + platformXOffset >= ExistingPlatform.position.x - platformXOffset 
            && ToSpawnPlatformX - platformXOffset <= ExistingPlatform.position.x + platformXOffset)
        {
            return true;
        }

        // not overlapping an existing platform
        else
            return false;
    }
}
