//==============================================
//Day           :11/09
//Creator       :HashizumeAtsuki
//Description   :WaterGunTagのタイトルシーン
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterGunTitleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePlayScene();
        }
    }


    void ChangePlayScene()
    {
        SceneManager.LoadScene("WaterGunTagPlayScene");
    }
}
