using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;

public class Timer : NetworkBehaviour
{
    public Text TimerText;
    [SyncVar] public float totalTime;
    //カウントダウン情報格納用
    CountDown countDownScript;
    GameObject countDownText;

    // Start is called before the first frame update
    void Start()
    {
        countDownText = GameObject.Find("CountDownObject");
        countDownScript = countDownText.GetComponent<CountDown>();
    }

    // Update is called once per frame
    void Update()
    {
		if (countDownScript.countDownFlag)
		{
            totalTime -= Time.deltaTime;
            int time = (int)totalTime;
            TimerText.text = time.ToString();      
        }
	}
}

