using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterGunTagPlayScene : MonoBehaviour
{
    [SerializeField] private GameObject m_drawWinner;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_drawWinner.GetComponent<DrawWinner>().GetIsReturnTitle())
        {
            ChangePlayScene();
        }
    }


    void ChangePlayScene()
    {
        SceneManager.LoadScene("WaterGunTagTitleScene");
    }
}
