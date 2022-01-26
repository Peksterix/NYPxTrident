using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    [SerializeField] GameObject pointMana;
    [SerializeField] GameObject timer;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.GetComponent<THGameTime>().GetIsFinish())
        {
            //pointMana.GetComponent<PointManager>().PlayerList
        }
    }
}
