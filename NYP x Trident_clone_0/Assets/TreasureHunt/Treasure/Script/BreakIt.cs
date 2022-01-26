using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BreakIt : NetworkBehaviour
{
    public GameObject treasure;
    public int probability;
    public int num;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isServer)
            return;

        if(other.gameObject.tag=="Player")
        {
            if (other.gameObject.GetComponent<PlayerCon>().isPressingSpace)
            {
                if(Random.Range(1, num+1) % probability==0)
                {
                    Vector3 pos = this.gameObject.transform.position;
                    pos.y++;
                    GameObject newGO = Instantiate(treasure, this.gameObject.transform.position, Quaternion.identity);
                    NetworkServer.Spawn(newGO);
                }
                NetworkServer.Destroy(this.gameObject);
            }
        }
    }
}
