using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class HiddenWall : NetworkBehaviour
{
    PlayerCon playerScript;
    public GameObject camera;

    public int count;

    GameObject player; //ƒvƒŒƒCƒ„[î•ñŠi”[—p
    //‰ñ“]’†‚©‚Ç‚¤‚©
    public bool rotFlag;

    // Start is called before the first frame update
    void Start()
    {
        rotFlag = false;
        count = 0;
    }

    private void FixedUpdate()
    {
        //‰ñ“]’†‚Å‚Í‚È‚¢ê‡‚ÍŽÀs 
        if (rotFlag)
        {
            CmdMove();
            count++;
            if (count >= 90)
            {
                rotFlag = false;
                count = 0;
                playerScript.camera.transform.position = playerScript.cameraPos.transform.position;
                playerScript.camera.transform.rotation = playerScript.cameraPos.transform.rotation;
            }
        }
    }

    [Command(requiresAuthority = false)]
    void CmdMove()
    {
        transform.Rotate(new Vector3(0, -2.0f, 0));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;

            if (Input.GetKey(KeyCode.Space))
            {
                rotFlag = true;
                playerScript = player.GetComponent<PlayerCon>();
                playerScript.camera.transform.position = camera.transform.position;
                playerScript.camera.transform.rotation = camera.transform.rotation;
            }

        }
    }
}
