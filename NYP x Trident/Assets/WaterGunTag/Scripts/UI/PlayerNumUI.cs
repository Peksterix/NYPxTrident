//==============================================
//Day           :12/21
//Creator       :HashizumeAtsuki
//Description   :プレイヤーのナンバー表示
//               
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNumUI : MonoBehaviour
{
    //プレイヤー
    private GameObject m_player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = m_player.GetComponent<WGTPlayerController>().m_playerNum.ToString() + "P";
    }


    public void SetPlayer(GameObject player)
    {
        m_player = player;
    }
}
