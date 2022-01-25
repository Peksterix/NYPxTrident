using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Graph : MonoBehaviour
{
	private const int max = 300; 
	private int current;      
	public Slider slider;       // �V�[���ɔz�u����Slider�i�[�p

	// Use this for initialization
	void Start()
	{
		slider.maxValue = max;
		current = 0; 
		slider.value = 0;   
	}
	void FixedUpdate()
	{
		if (current < max)
		{
			current++;
			slider.value = current;
		}
	}
}