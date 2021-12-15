using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointScript : MonoBehaviour
{
    public int score;
    Text scoreText; //得点の文字の変数

    // Start is called before the first frame update
    void Start()
    {
        scoreText = (Text)FindObjectOfType(typeof(Text));
        score = 0;
        //ScoreTextの文字をScore:Scoreの値にする
        scoreText.text = "× " + score.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        //ScoreTextの文字をScore:Scoreの値にする
        scoreText.text = "× " + score.ToString();
    }
    public void AddScore()
    {
        score++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Treasure")
        {
            AddScore();
            Destroy(other.gameObject);
        }
    }
}
