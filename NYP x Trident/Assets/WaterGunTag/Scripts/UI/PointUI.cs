using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointUI : MonoBehaviour
{
    //ポイントを表示するプレイヤー
    private GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text pointText = this.GetComponent<Text>();
        pointText.text = m_player.GetComponent<PlayerActions>().m_point.ToString();
    }

    public void SetPlayer(GameObject player)
    {
        m_player = player;
    }
}
