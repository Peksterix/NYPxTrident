using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    [SerializeField] GameObject textUI;   //プレイヤー情報格納用
    Point pointScript;

    // Start is called before the first frame update
    void Start()
    {
        pointScript = textUI.GetComponent<Point>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            pointScript.AddScore();
            Destroy(this.gameObject);
        }
    }
}
