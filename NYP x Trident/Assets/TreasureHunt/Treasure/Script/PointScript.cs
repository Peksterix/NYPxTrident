using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class PointScript : NetworkBehaviour
{
    [SyncVar] public int score;
    public Text scoreText; //“¾“_‚Ì•¶š‚Ì•Ï”
    public int playerNum;
    private GameObject TimerText;

    // Start is called before the first frame update
    void Start()
    {
        TimerText = GameObject.Find("GameTimer");
        PlayerSettings();
        playerNum = GameObject.Find("PointManager").GetComponent<PointManager>().PlayerList.Count;
        score = 0;
        //ScoreText‚Ì•¶š‚ğScore:Score‚Ì’l‚É‚·‚é
        scoreText.text = playerNum.ToString()+"P~" + score.ToString();
    }

    void PlayerSettings()
    {

        GameObject.Find("PointManager").GetComponent<PointManager>().PlayerList.Add
            (new Player(ID: playerNum = GameObject.Find("PointManager").GetComponent<PointManager>().PlayerList.Count + 1,
            Name: "test",//©‚±‚±‚É–¼‘O“ü‚ê‚éƒˆ`
            Score: 0));

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
        //ScoreText‚Ì•¶š‚ğScore:Score‚Ì’l‚É‚·‚é
        scoreText.text = playerNum.ToString() + "P~" + score.ToString();

        if(TimerText.GetComponent<THGameTime>().GetIsFinish())
        {
            PointSettings();
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
            Destroy(other.gameObject);
        }
    }
}
