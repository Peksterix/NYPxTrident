using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    [SerializeField] GameObject pointMana;
    [SerializeField] GameObject timer;
    [SerializeField] GameObject slider;
    [SerializeField] GameObject canvas;
    private int playerNum;
    public int[] score = new int[5];

    [SerializeField] Image resultCanvas;


    // Start is called before the first frame update
    void Start()
    {
        resultCanvas.color = new Color(resultCanvas.color.r,resultCanvas.color.g,resultCanvas.color.b,0.0f); 
        playerNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.GetComponent<THGameTime>().GetIsFinish())
        {
            resultCanvas.color = new Color(resultCanvas.color.r, resultCanvas.color.g, resultCanvas.color.b, 0.5f);

            if (playerNum<pointMana.GetComponent<PointManager>().PlayerList.Count)
            {
                GameObject obj = Instantiate(slider, Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(canvas.gameObject.transform, false);
                playerNum++;
            }
            else if(playerNum == pointMana.GetComponent<PointManager>().PlayerList.Count)
            {
                foreach (var chara in pointMana.GetComponent<PointManager>().PlayerList)
                {
                    score[chara.ID - 1] = chara.Score;
                }
                var list = new List<int>();
                list.AddRange(score);
                list.Sort((a, b) => b - a);

                foreach (var chara in pointMana.GetComponent<PointManager>().PlayerList)
                {
                    if(score[0]==chara.Score)
                    {
                        chara.Result = "WIN";
                    }
                }

                playerNum++;
            }



        }
    }
}
