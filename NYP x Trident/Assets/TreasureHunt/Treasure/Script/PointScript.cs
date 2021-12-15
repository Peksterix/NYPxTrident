using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointScript : MonoBehaviour
{
    public int score;
    Text scoreText; //“¾“_‚Ì•¶š‚Ì•Ï”

    // Start is called before the first frame update
    void Start()
    {
        scoreText = (Text)FindObjectOfType(typeof(Text));
        score = 0;
        //ScoreText‚Ì•¶š‚ğScore:Score‚Ì’l‚É‚·‚é
        scoreText.text = "~ " + score.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        //ScoreText‚Ì•¶š‚ğScore:Score‚Ì’l‚É‚·‚é
        scoreText.text = "~ " + score.ToString();
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
