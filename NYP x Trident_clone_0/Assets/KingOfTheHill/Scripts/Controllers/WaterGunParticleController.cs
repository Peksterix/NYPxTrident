using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WaterGunParticleController : MonoBehaviour
{
    private float particleForce;

    // Start is called before the first frame update
    void Start()
    {
        particleForce = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Vector3 dir = transform.position - other.transform.position;

            dir = -dir.normalized;

            other.gameObject.GetComponent<KOTHPlayerController>().PushBack(particleForce, dir);
        }
    }
}
