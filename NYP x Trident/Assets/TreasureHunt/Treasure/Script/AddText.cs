using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddText : MonoBehaviour
{
    GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("PointCanvas");
        this.gameObject.transform.SetParent(canvas.gameObject.transform, false);
    }

}
