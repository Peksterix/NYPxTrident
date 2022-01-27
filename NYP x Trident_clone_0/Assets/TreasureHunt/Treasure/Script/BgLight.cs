using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLight : MonoBehaviour
{
    public Material sky;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientIntensity = 0.3f; // ��̖��邳��ݒ�
        RenderSettings.skybox = sky;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
