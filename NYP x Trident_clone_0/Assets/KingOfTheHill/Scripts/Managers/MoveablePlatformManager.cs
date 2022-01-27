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
    private GameObject CurrentHighestPlatform;

    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
            return;

        timeElapsed = 15f;
        attemptingToSpawnPlatform = false;
        platformXOffset = PlatformToSpawn.transform.localScale.x / 2; // divide by 2 because (0, 0) is at the center of the object
        CurrentHighestPlatform = platformParent.transform.GetChild(0).gameObject;
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
            if (platformParent.transform.childCount <= 10 && !attemptingToSpawnPlatform)
            {
                SpawnMoveablePlatforms();
            }
        }

        SetNewHighestPlatform();
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
                Instantiate(PlatformToSpawn, new Vector3(posX, PlatformToSpawn.transform.position.y, 0),
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
    }

    void SetNewHighestPlatform()
    {
        GameObject platform;

        for (int i = 2; i < platformParent.transform.childCount; ++i)
        {
            platform = platformParent.transform.GetChild(i).GetChild(0).gameObject;
            if (platform.transform.position.y > CurrentHighestPlatform.transform.GetChild(0).position.y && platform != null)
            {
                if (platform == null)
                    return;

                CurrentHighestPlatform = platform;
            }
            else
            {
                if (platform == null)
                    return;

                platform.GetComponentInParent<MoveablePlatformController>().isHighestPlatform = false;
            }
        }

        if (CurrentHighestPlatform.CompareTag("Platforms") && CurrentHighestPlatform != null)
            CurrentHighestPlatform.GetComponentInParent<MoveablePlatformController>().isHighestPlatform = true;
        else
            return;
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
        bool isHitLeft;
        bool isHitRight;
        isHitLeft = Physics.Raycast(new Vector3(ToSpawnPlatformX, targetHeight, 0), Vector3.left, out hitLeft, Mathf.Infinity, LayerMask.GetMask("PlatformRaycast"));
        isHitRight = Physics.Raycast(new Vector3(ToSpawnPlatformX, targetHeight, 0), Vector3.right, out hitRight, Mathf.Infinity, LayerMask.GetMask("PlatformRaycast"));

        return isHitLeft || isHitRight;
    }

    public float GetRandomTargetPlatformHeight()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        return Random.Range(PlatformLowestY, PlatformHighestY);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    }
}
