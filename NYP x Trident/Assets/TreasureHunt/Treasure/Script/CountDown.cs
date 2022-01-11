using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CountDown : NetworkBehaviour
{
	public Text timerText;
	[SyncVar]  public float totalTime;
	int seconds;
	[SyncVar] public bool countDownFlag;

	// Use this for initialization
	void Start()
	{
		countDownFlag = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (totalTime > 0)
		{
			if (GameObject.FindGameObjectWithTag("Player") == true)
				totalTime -= Time.deltaTime;
			seconds = (int)totalTime + 1;
			timerText.text = seconds.ToString();
		}
		else if (totalTime <= -1)
		{
			timerText.enabled = false;
			countDownFlag = true;
		}
		else if(totalTime <= 0)
		{
			timerText.text = "START!!";
			totalTime -= Time.deltaTime;
			countDownFlag = true;
		}
	}
}
