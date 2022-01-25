using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Bamboo.Utility;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    CinemachineVirtualCamera VC;
    Transform lookat;

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void AssignCameraTarget(Transform lookat, Transform follow)
    {
        VC.LookAt = lookat;
        VC.Follow = follow;
    }
}
