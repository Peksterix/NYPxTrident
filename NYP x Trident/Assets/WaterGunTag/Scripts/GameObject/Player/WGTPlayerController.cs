//==============================================
//Day           :10/26
//Creator       :HashizumeAtsuki
//Description   :�v���C���[�̑���
//               Player control
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WGTPlayerController : GameObjectBase
{
    //�v���C���[�̑��x Player Speed
    [SerializeField] float m_playerSpeed = 3;

    //�ǂ�������Ƃ��̑��x
    [SerializeField] float m_chaseSpeed = 1.1f;

    //��������
    [SerializeField] public GameObject m_time;

    //�����Ă���Ƃ��̈ړ����x
    [SerializeField] float m_shotMoveSpeed = 0.7f;
    //�}�E�X�J�[�\���I�u�W�F�N�g
    [SerializeField] private GameObject m_mouseCursor;

    //����s�\��Ԃɂ���
    public bool m_isInoperable;

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(m_playerSpeed);

        m_isInoperable = false;

        m_time = GameObject.Find("Time");


        m_mouseCursor = GameObject.Find("MouseCursor");

        GameObject playerManager = GameObject.Find("PlayerManager");
        playerManager.GetComponent<PlayerManager>().GetPlayerList().Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_time.GetComponent<GameTime>().GetIsFinish()||
            this.GetComponent<PlayerActions>().GetIsStunting())
        {
            this.GetComponent<Rigidbody>().velocity = Vector2.zero;
            return;
        }
        if (m_isInoperable)
        {
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

        //KeyInput=========================================
        ////��L�[�������ꂽ��
        //if(Input.GetKey(KeyCode.UpArrow))
        //{

        //    //��ɐi��
        //    vel += Vector3.forward;

        //}

        ////���L�[�������ꂽ��
        //if (Input.GetKey(KeyCode.DownArrow))
        //{

        //    //���ɐi��
        //    vel += Vector3.back;

        //}

        ////���L�[�������ꂽ��
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{

        //    //���ɐi��
        //    vel += Vector3.left;
        //}

        ////�E�L�[�������ꂽ��
        //if (Input.GetKey(KeyCode.RightArrow))
        //{

        //    //�E�ɐi��
        //    vel += Vector3.right;
        //}


        //Mouse==========================================
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        Physics.Raycast(ray, out raycastHit);

        vel = raycastHit.point;
        vel.y = 0.0f;

        m_mouseCursor.transform.position = vel;

        vel = m_mouseCursor.transform.position - transform.position;

        vel.y = 0.0f;

        if (vel.magnitude <= 0.1f)
        {
            this.GetComponent<Rigidbody>().velocity = (vel.normalized * GetSpeed() * 0.0f);
            return;
        }

        else if (vel.magnitude >= 1.0f)
        {
            //���x�𔽉f������ Reflect the velocity
            this.GetComponent<Rigidbody>().velocity = (vel.normalized * GetSpeed());
        }
        else
        {
            //���x�𔽉f������ Reflect the velocity
            this.GetComponent<Rigidbody>().velocity = (vel.normalized * GetSpeed() * vel.magnitude);
        }


        //�����Ă���Ԃ͈ړ����x�𗎂Ƃ�
        if (this.GetComponentInChildren<WaterGun>().GetIsShotWaterGun())
        {
            this.GetComponent<Rigidbody>().velocity *= m_shotMoveSpeed;
        }else
        //�ǂ�������l�Ȃ瑬�x��������
        if (this.GetComponent<PlayerActions>().GetIsChase())
        {
            this.GetComponent<Rigidbody>().velocity *= m_chaseSpeed;
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


        //�J������Ǐ]������
        Camera.main.transform.position = transform.position + new Vector3(0, Camera.main.transform.position.y, -3.6f);




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
        this.GetComponentInChildren<WaterGun>().ShotWater();
    }
}
