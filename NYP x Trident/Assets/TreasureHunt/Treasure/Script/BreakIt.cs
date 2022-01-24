using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakIt : MonoBehaviour
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
        if(other.gameObject.tag=="Player")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if(Random.Range(1, num+1) % probability==0)
                {
                    Vector3 pos = this.gameObject.transform.position;
                    pos.y++;
                    Instantiate(treasure, pos, Quaternion.identity);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
