using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class WaterGunTagPlayScene : NetworkBehaviour
{
    [SerializeField] private GameObject m_drawWinner;
    public List<GameObject> WGTSpawnPoints = new List<GameObject>();

    void Start()
    {
        if (!isServer)
            return;

        RegisterSpawnPoints();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && m_drawWinner.GetComponent<DrawWinner>().GetIsReturnTitle())
        //{
        //    ChangePlayScene();
        //}
    }

    // Not needed anymore after merging

    //void ChangePlayScene()
    //{
    //    SceneManager.LoadScene("WaterGunTagTitleScene");
    //}

    // Registers all spawn points in the server
    void RegisterSpawnPoints()
    {
        for (int i = 0; i < WGTSpawnPoints.Count; ++i)
        {
            NetworkManager.RegisterStartPosition(WGTSpawnPoints[i].transform);
        }
    }
}
