using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KingOfTheHillNetworkManager : NetworkManager
{
    [Header("King Of The Hill Variables")]

    [Header("Player Start Positions")]
    public GameObject PlayerSpawnPoints;

    [Header("Platforms")]
    [SerializeField]
    private GameObject moveablePlatform;
    public GameObject platformParent;
    public float PlatformLowestX;
    public float PlatformHighestX;
        
    private ParticleSystem waterspout;
    private float timeElapsed;

    public override void OnStartServer()
    {
        timeElapsed = 5f;
        for (int i = 0; i < PlayerSpawnPoints.transform.childCount; ++i)
        {
            RegisterStartPosition(PlayerSpawnPoints.transform.GetChild(i));
        }

        SpawnMoveablePlatforms();
    }

    void SpawnMoveablePlatforms()
    {
        float randomTargetWidth = GetRandomTargetPlatformWidth();

        foreach (GameObject platform in spawnPrefabs)
        {
            if (platform.name == "Moveable Platform")
            {
                GameObject plat = Instantiate(platform, new Vector3(randomTargetWidth, 3.547268f, 0f), platform.transform.rotation, platformParent.transform);
                NetworkServer.Spawn(plat);
            }
        }
    }

    public float GetRandomTargetPlatformWidth()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        return Random.Range(PlatformLowestX, PlatformHighestX);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed -= Time.deltaTime * 2;
            
        if(timeElapsed <= 0f)
        {
            SpawnMoveablePlatforms();
            timeElapsed = 5f;
        }
    }
}
