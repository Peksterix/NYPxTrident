using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Graph : MonoBehaviour
{
	private GameObject pointMana;

	GameObject[] sliderObjects;

	private const int max = 30; 
	private float current;      
	public Slider slider;       // シーンに配置したSlider格納用
	private int point=0;

	public Text playerName;

	// Use this for initialization
	void Start()
	{
		pointMana = GameObject.Find("PointManager");
		slider.maxValue = max;
		current = 0; 
		slider.value = 0;
		sliderObjects = GameObject.FindGameObjectsWithTag("Slider");
		foreach (var chara in pointMana.GetComponent<PointManager>().PlayerList)
		{
			if (sliderObjects.Length ==chara.ID)
			{
				point = chara.Score;
				playerName.text = chara.ID+"P";
			}
		}
	}
	void FixedUpdate()
	{

		if (current < point)
		{
			current+=0.1f;
			slider.value = current;
		}

		if(Input.GetKeyDown(KeyCode.Z))
		{
			Debug.Log(playerName.text);
		}
	}
}