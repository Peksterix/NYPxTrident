//==============================================
//Day           :10/29
//Creator       :HashizumeAtsuki
//Description   :水鉄砲のクラス
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    //ParticleSystem型を変数psで宣言します。
    private ParticleSystem m_ps;
    //GameObject型で変数objを宣言します。
    [SerializeField] public GameObject m_obj;
    //水鉄砲を撃っているかのフラグ
    private bool m_isShotWaterGun;


    void Start()
    {
        //GetComponentInChildrenで子要素も含めた
        //ParticleSystemにアクセスして変数psで参照します。
        m_ps = m_obj.GetComponentInChildren<ParticleSystem>();

        GetPs().Stop();
        m_isShotWaterGun = false;

    }


    void Update()
    {

        if (this.gameObject.transform.root.GetComponent<WGTPlayerController>().
             m_time.GetComponent<GameTime>().GetIsFinish()|| 
             this.gameObject.transform.root.GetComponent<PlayerActions>().GetIsStunting())
        {
            GetPs().Stop();
            m_isShotWaterGun = false;
        }
    }

    //-------------------------------------
    //水鉄砲発射　
    //Water pistol firing
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    public virtual void ShotWater()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetPs().Play();
            m_isShotWaterGun = true;
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            GetPs().Stop();
            m_isShotWaterGun = false;
        }
    }

   public bool GetIsShotWaterGun()
    {
        return m_isShotWaterGun;
    }


    public ParticleSystem GetPs()
    {
        return m_ps;
    }
}
