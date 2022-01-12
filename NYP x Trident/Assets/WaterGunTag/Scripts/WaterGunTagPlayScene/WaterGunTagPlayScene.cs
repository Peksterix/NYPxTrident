using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Mirror;

public class WaterGunTagPlayScene : NetworkBehaviour
{
    //WGTResult
    [SerializeField] private GameObject m_wgtResult;

    public List<GameObject> WGTSpawnPoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
            return;

        RegisterSpawnPoints();
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

    void RegisterSpawnPoints()
    {
        for (int i = 0; i < WGTSpawnPoints.Count; ++i)
        {
            NetworkManager.RegisterStartPosition(WGTSpawnPoints[i].transform);
        }
    }
}
