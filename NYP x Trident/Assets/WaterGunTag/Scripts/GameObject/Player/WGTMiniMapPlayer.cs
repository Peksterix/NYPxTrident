//==============================================
//Day           :12/06
//Creator       :HashizumeAtsuki
//Description   :ミニマップに映るプレイヤー
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WGTMiniMapPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //自分でなければマップに表示しない
        //It won't show up on the map unless it's you.
        // this.gameObject.layer = 7;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
