//�����i�ł͎g�p���Ȃ�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerWaterGun : MonoBehaviour
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
    //���S�C���ˁ@
    //Water pistol firing
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
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
