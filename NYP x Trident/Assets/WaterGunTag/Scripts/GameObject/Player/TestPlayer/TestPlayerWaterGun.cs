//完成品では使用しない
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerWaterGun : MonoBehaviour
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

        m_ps.Stop();
        m_isShotWaterGun = false;
    }


    void Update()
    {
        if(this.gameObject.transform.root.GetComponent<TestPlayerController>().
            m_time.GetComponent<GameTime>().GetIsFinish()||
            this.gameObject.transform.root.GetComponent<PlayerActions>().GetIsStunting())
        {
            m_ps.Stop();
            m_isShotWaterGun = false;
        }

    }

    //-------------------------------------
    //水鉄砲発射　
    //Water pistol firing
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    public void ShotWater()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_ps.Play();
            m_isShotWaterGun = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_ps.Stop();
            m_isShotWaterGun = false;
        }
    }

    public bool GetIsShotWaterGun()
    {
        return m_isShotWaterGun;
    }

}
