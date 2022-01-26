using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WGChargeUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //6=Minimap
        if (this.gameObject.layer==6)
        {
            return;
        }
        Vector3 p = Camera.main.transform.position;




        transform.LookAt(p);

        transform.eulerAngles = new Vector3(0.0f, -transform.root.transform.rotation.y, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 lossScale = transform.lossyScale;
        Vector3 localScale = transform.localScale;
        //6=Minimap
        if (this.gameObject.layer == 6)
        {
          
            transform.localScale = new Vector3(
                  localScale.x / lossScale.x * 1.0f,
                  localScale.y / lossScale.y * 1.0f,
                  localScale.z / lossScale.z * 1.0f
                  );
            return;
        }

        

        transform.localScale = new Vector3(
              localScale.x / lossScale.x * 0.5f,
              localScale.y / lossScale.y * 0.5f,
              localScale.z / lossScale.z * 0.5f
              );


        Vector3 p = Camera.main.transform.position;

      

       transform.LookAt(p);



        transform.eulerAngles = new Vector3(0.0f, -transform.rotation.y, 0.0f);

    }


}
