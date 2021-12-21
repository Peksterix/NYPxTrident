//==============================================
//Day           :12/15
//Creator       :HashizumeAtsuki
//Description   :リザルト背景の移動
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBackGround : MonoBehaviour
{
    //初期Y座標
    [SerializeField] float m_startYPos = -1200.0f;

    //larpの数字
    private float m_larpT = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_larpT = 0.0f;
        GetComponent<RectTransform>().localPosition = new Vector3(0, Mathf.Lerp(m_startYPos, 0.0f, m_larpT), 0);
       
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<RectTransform>().localPosition = new Vector3(0, Mathf.Lerp(m_startYPos, 0.0f, m_larpT), 0);






        m_larpT += Time.deltaTime*2;
    }
}
