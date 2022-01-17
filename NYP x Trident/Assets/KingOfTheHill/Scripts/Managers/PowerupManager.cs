using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PowerupManager : NetworkBehaviour
{
    // No time to add more powerups, if there were more powerups, this list can be used, for now just spawn an individual prefab
    //[SerializeField]
    //private List<GameObject> powerUpPrefabs = new List<GameObject>();

    public GameObject WaterBombPrefab;
    public Transform PowerupParent;
    public float MinXPos;
    public float MaxXPos;

    private float TimeToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
            return;

        TimeToSpawn = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;

        TimeToSpawn -= Time.deltaTime * 1;

        if (TimeToSpawn <= 0)
        {
            SpawnPowerup();
            TimeToSpawn = 5f;
        }
    }

    [ClientRpc]
    void SpawnPowerup()
    {
        GameObject newPowerup = Instantiate(WaterBombPrefab, new Vector3(getRandomXPos(), PowerupParent.position.y, 0), Quaternion.identity, PowerupParent);
        NetworkServer.Spawn(newPowerup);
    }

    float getRandomXPos()
    {
        return Random.Range(MinXPos, MaxXPos);
    }
}
