using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PointScript : NetworkBehaviour
{
    [SyncVar] public int score;
     public Text scoreText; //���_�̕����̕ϐ�

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        //ScoreText�̕�����Score:Score�̒l�ɂ���
        scoreText.text = "POINT�~" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //ScoreText�̕�����Score:Score�̒l�ɂ���
        scoreText.text = "POINT�~" + score.ToString();
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
