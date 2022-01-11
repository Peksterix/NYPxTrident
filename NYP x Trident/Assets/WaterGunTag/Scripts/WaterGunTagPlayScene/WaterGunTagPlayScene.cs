using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterGunTagPlayScene : MonoBehaviour
{
    //WGTResult
    [SerializeField] private GameObject m_wgtResult;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_wgtResult.GetComponent<WGTResult>().GetIsReturnTitle())
        {
            ChangePlayScene();
        }
    }


    void ChangePlayScene()
    {
        SceneManager.LoadScene("WaterGunTagTitleScene");
    }
}
