using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCamera : NetworkBehaviour
{
    void Start()
    {
        transform.Find("Camera").gameObject.SetActive(isLocalPlayer);
    }
}
