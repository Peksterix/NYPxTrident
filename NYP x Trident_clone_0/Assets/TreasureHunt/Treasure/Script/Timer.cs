using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timeTexts;
    public float totalTime = 10;
    public int retime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;
        retime = (int)totalTime;
        timeTexts.text = retime.ToString();
        if(retime<=0)
        {
            SceneManager.LoadScene("Result");
        }
    }
}
