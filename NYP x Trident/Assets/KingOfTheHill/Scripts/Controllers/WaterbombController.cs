using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WaterbombController : NetworkBehaviour
{
    public GameObject WaterExplosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Platforms") || collision.gameObject.CompareTag("DeafultPlatforms") || collision.gameObject.CompareTag("Player"))
        {
            GameObject newExplosionPrefab = Instantiate(WaterExplosionPrefab, this.transform.position, Quaternion.identity);
            NetworkServer.Spawn(newExplosionPrefab);
            NetworkServer.Destroy(this.gameObject);
        }
    }
}
