using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    public static int Score; //���_�̕ϐ�
    public Text ScoreText; //���_�̕����̕ϐ�

    // Start is called before the first frame update
    void Start()
    {
        Score = 0; //���_��0�ɂ���
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score:" + Score.ToString(); //ScoreText�̕�����Score:Score�̒l�ɂ���

    }

    public void AddScore()
    {
        Score += 100;
    }

    public static int GetScore()
    {
        return Score;
    }
}

