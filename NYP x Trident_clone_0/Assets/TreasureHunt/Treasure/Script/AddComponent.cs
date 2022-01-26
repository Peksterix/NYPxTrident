using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddComponent : MonoBehaviour
{
    public void AddCom()
    {
        foreach (Transform child in transform)
        {
            // Žq—v‘f‚Ì–¼‘O‚ð—ñ‹“
            Debug.Log(child.name);
            GameObject obj = child.gameObject;
            obj.AddComponent<MeshCollider>();
            obj.AddComponent<SAMeshColliderBuilder>();
            
 }
    }
}
