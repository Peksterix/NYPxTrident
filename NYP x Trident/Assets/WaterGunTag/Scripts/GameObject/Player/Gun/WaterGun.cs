//==============================================
//Day           :10/29
//Creator       :HashizumeAtsuki
//Description   :���S�C�̃N���X
//              
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    //ParticleSystem�^��ϐ�ps�Ő錾���܂��B
    private ParticleSystem m_ps;
    //GameObject�^�ŕϐ�obj��錾���܂��B
    [SerializeField] public GameObject m_obj;
    //���S�C�������Ă��邩�̃t���O
    private bool m_isShotWaterGun;


    void Start()
    {
        //GetComponentInChildren�Ŏq�v�f���܂߂�
        //ParticleSystem�ɃA�N�Z�X���ĕϐ�ps�ŎQ�Ƃ��܂��B
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
    //���S�C���ˁ@
    //Water pistol firing
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
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
