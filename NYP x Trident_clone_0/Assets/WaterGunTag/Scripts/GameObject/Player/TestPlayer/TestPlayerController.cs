//�����i�ł͎g�p���Ȃ�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : GameObjectBase
{
    //�v���C���[�̑��x Player Speed
    [SerializeField] float m_playerSpeed = 3;

    //�ǂ�������Ƃ��̑��x
    [SerializeField] float m_chaseSpeed = 1.1f;

    //�����Ă���Ƃ��̈ړ����x
    [SerializeField] float m_shotMoveSpeed = 0.7f;

    //��������
    [SerializeField] public GameObject m_time;

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(m_playerSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        if(m_time.GetComponent<GameTime>().GetIsFinish() ||
            this.GetComponent<PlayerActions>().isStunting)
        {
            this.GetComponent<Rigidbody>().velocity = Vector2.zero;
            return;
        }
        KeyInput();
    }

    //-------------------------------------
    //�L�[���́@
    //KeyInput
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    private void KeyInput()
    {
        //�ړ� Move
        MoveKeyInput();

        //�U���@Attack
        AttackKeyInput();
    }

    //-------------------------------------
    //�L�[���͂ɂ��ړ��@
    //Movement by key input
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    private void MoveKeyInput()
    {
        //���x Velocity
        Vector3 vel = Vector3.zero;
        //��L�[�������ꂽ��
        if (Input.GetKey(KeyCode.W))
        {

            //��ɐi��
            vel += Vector3.forward;

        }

        //���L�[�������ꂽ��
        if (Input.GetKey(KeyCode.S))
        {

            //���ɐi��
            vel += Vector3.back;

        }

        //���L�[�������ꂽ��
        if (Input.GetKey(KeyCode.A))
        {

            //���ɐi��
            vel += Vector3.left;
        }

        //�E�L�[�������ꂽ��
        if (Input.GetKey(KeyCode.D))
        {

            //�E�ɐi��
            vel += Vector3.right;
        }



        //���x�𔽉f������ Reflect the velocity
        this.GetComponent<Rigidbody>().velocity = (vel.normalized * GetSpeed());

        //�����Ă���Ԃ͈ړ����x�𗎂Ƃ�
        if (this.GetComponentInChildren<TestPlayerWaterGun>().GetIsShotWaterGun())
        {
            this.GetComponent<Rigidbody>().velocity *= m_shotMoveSpeed;
        }
        else
        //�ǂ�������l�Ȃ瑬�x��������
        if (this.GetComponent<PlayerActions>().isChase)
        {
            this.GetComponent<Rigidbody>().velocity*=m_chaseSpeed;
        }
      

        //�v���C���[���ǂ̕����ɐi��ł��邩���킩��悤�ɁA�����ʒu�ƌ��ݒn�̍��W�������擾
        //Obtain the coordinate difference between the initial position and the current location so that the player can see which direction he is moving.
        Vector3 diff = transform.position + ((vel.normalized * GetSpeed()) - transform.position);


        //�x�N�g���̏���Quaternion.FromToRotation�Ɉ����n����]�ʂ��擾���v���C���[����]������
        //Pass the vector information to Quaternion.FromToRotation to get the rotation amount and rotate the player.
        if (diff.magnitude >= 0.01f)
        {
            this.transform.rotation = Quaternion.FromToRotation(Vector3.forward, diff.normalized);
        }

    }

    //-------------------------------------
    //�L�[���͂ɂ��U���@
    //Attack by keystrokes
    //����     :�Ȃ��@None
    //�߂�l   :�Ȃ��@None
    //-------------------------------------
    private void AttackKeyInput()
    {
        //���S�C����
        this.GetComponentInChildren<TestPlayerWaterGun>().ShotWater();
    }
}
