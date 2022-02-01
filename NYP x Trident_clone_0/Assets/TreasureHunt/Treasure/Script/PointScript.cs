using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class PointScript : NetworkBehaviour
{
    [SyncVar] public int score;
    public Text scoreText; //���_�̕����̕ϐ�
    public int playerNum;
    private GameObject TimerText;

    // Start is called before the first frame update
    void Start()
    {
        TimerText = GameObject.Find("GameTimer");
        PlayerSettings();
        playerNum = GameObject.Find("PointManager").GetComponent<PointManager>().PlayerList.Count;
        score = 0;
        //ScoreText�̕�����Score:Score�̒l�ɂ���
        scoreText.text = playerNum.ToString()+"P:" + score.ToString();
    }

    void PlayerSettings()
    {

        GameObject.Find("PointManager").GetComponent<PointManager>().PlayerList.Add
            (new Player(ID: playerNum = GameObject.Find("PointManager").GetComponent<PointManager>().PlayerList.Count + 1,
            Name: LocalPlayerHandle.Instance.playerName,//�������ɖ��O����郈�`
            Score: 0,
            Result: "LOSE"));
    }

    void PointSettings()
    {
        foreach (var chara in GameObject.Find("PointManager").GetComponent<PointManager>().PlayerList)
        {
            if(chara.ID== playerNum)
                chara.Score = score;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ScoreText�̕�����Score:Score�̒l�ɂ���
        scoreText.text = playerNum.ToString() + "P:" + score.ToString();

        if(TimerText.GetComponent<THGameTime>().GetIsFinish())
        {
            PointSettings();

            if(GameObject.Find("ResultObject").GetComponent<ResultScore>().score[0]==score)
            {
                scoreText.text = playerNum.ToString() + "P:WIN";
                scoreText.fontSize = 50;

            }
            else
            {
                scoreText.text = playerNum.ToString() + "P:LOSE";
                scoreText.fontSize = 50;

            }
        }
    }
    public void AddScore()
    {
        score++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Treasure")
        {
            AddScore();

            NetworkServer.Destroy(other.gameObject);
        }
    }
}
