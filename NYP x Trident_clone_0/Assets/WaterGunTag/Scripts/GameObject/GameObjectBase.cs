//==============================================
//Day           :10/26
//Creator       :HashizumeAtsuki
//Description   :�Q�[���I�u�W�F�N�g�̊��N���X
//               Base class for game objects
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameObjectBase : NetworkBehaviour
{
    //���x Speed
    private float m_speed = 0;

    //�ő呬�x Maximum speed
    [SerializeField] float m_maxSpeed;

    //���� rotation
    private float m_rot;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
            return;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //-------------------------------------
    //���x�v�Z
    //Velocity calculation
    //����     :�Ȃ��@None
    //�߂�l   :���x�@Vel
    //-------------------------------------
    public Vector3 VelCalculation()
    {
        Vector3 vel = transform.forward * m_speed;

        return vel;
    }

    public float GetSpeed()
    {
        return m_speed;
    }

    public void SetSpeed(float speed)
    {
        m_speed = speed;
    }

    public float GetMaxSpeed()
    {
        return m_maxSpeed;
    }

    public void SetMaxSpeed(float maxspeed)
    {
        m_maxSpeed = maxspeed;
    }

    public float GetRot()
    {
        return m_rot;
    }

    public void SetRot(float rot)
    {
        m_rot = rot;
    }
}
