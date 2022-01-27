using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);

        if (Camera.main)
            transform.LookAt(Camera.main.transform, Camera.main.transform.up);
    }
}
