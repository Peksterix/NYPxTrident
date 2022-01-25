using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    [SerializeField] GameObject pointMana;
    [SerializeField] GameObject timer;

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject slider;
    int playerNum;

    // Start is called before the first frame update
    void Start()
    {
        playerNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            GameObject obj = Instantiate(slider, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(canvas.gameObject.transform, false);
        }
        if (timer.GetComponent<THGameTime>().GetIsFinish())
        {
            if (playerNum < pointMana.GetComponent<PointManager>().PlayerList.Count)
            {
                GameObject obj = Instantiate(slider, Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(canvas.gameObject.transform, false);
                playerNum++;
            }
            //pointMana.GetComponent<PointManager>().PlayerList
        }
    }
}
