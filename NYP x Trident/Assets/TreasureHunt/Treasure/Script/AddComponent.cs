using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddComponent : MonoBehaviour
{
    public void AddCom()
    {
        foreach (Transform child in transform)
        {
            // �q�v�f�̖��O���
            Debug.Log(child.name);
            GameObject obj = child.gameObject;
            obj.AddComponent<MeshCollider>();
            obj.AddComponent<SAMeshColliderBuilder>();
            
 }
    }
}
