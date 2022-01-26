using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LookCamare();
    }

    // Update is called once per frame
    void Update()
    {
        LookCamare();
    }

    //ÉJÉÅÉâÇÃÇŸÇ§Ç…å¸Ç≠
    private void LookCamare()
    {
       
        Vector3 p = Camera.main.transform.position;
        p.y = transform.position.y;
        p.y = transform.root.rotation.y;

        
        transform.LookAt(p);

        transform.eulerAngles = new Vector3(0.0f, -transform.root.transform.rotation.y, 0.0f);
    }
}
