using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterExplosionController : MonoBehaviour
{
    Collider[] hits;


    private void Start()
    {
        hits = Physics.OverlapSphere(transform.position, 3);

        foreach(var player in hits)
        {
            if(player.CompareTag("Player"))
            {
                Vector3 dir = transform.position - player.transform.position;

                dir = -dir.normalized;

                player.GetComponent<ImpactReceiver>().AddImpact(dir, Mathf.Clamp(70, 20, 60));
            }
        }
    }
}
