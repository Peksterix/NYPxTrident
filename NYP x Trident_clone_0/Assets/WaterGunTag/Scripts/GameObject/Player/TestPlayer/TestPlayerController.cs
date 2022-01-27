//完成品では使用しない
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : GameObjectBase
{
    //プレイヤーの速度 Player Speed
    [SerializeField] float m_playerSpeed = 3;

    //追いかけるときの速度
    [SerializeField] float m_chaseSpeed = 1.1f;

    //撃っているときの移動速度
    [SerializeField] float m_shotMoveSpeed = 0.7f;

    //制限時間
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
    //キー入力　
    //KeyInput
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    private void KeyInput()
    {
        //移動 Move
        MoveKeyInput();

        //攻撃　Attack
        AttackKeyInput();
    }

    //-------------------------------------
    //キー入力による移動　
    //Movement by key input
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    private void MoveKeyInput()
    {
        //速度 Velocity
        Vector3 vel = Vector3.zero;
        //上キーが押されたら
        if (Input.GetKey(KeyCode.W))
        {

            //上に進む
            vel += Vector3.forward;

        }

        //下キーが押されたら
        if (Input.GetKey(KeyCode.S))
        {

            //下に進む
            vel += Vector3.back;

        }

        //左キーが押されたら
        if (Input.GetKey(KeyCode.A))
        {

            //左に進む
            vel += Vector3.left;
        }

        //右キーが押されたら
        if (Input.GetKey(KeyCode.D))
        {

            //右に進む
            vel += Vector3.right;
        }



        //速度を反映させる Reflect the velocity
        this.GetComponent<Rigidbody>().velocity = (vel.normalized * GetSpeed());

        //撃っている間は移動速度を落とす
        if (this.GetComponentInChildren<TestPlayerWaterGun>().GetIsShotWaterGun())
        {
            this.GetComponent<Rigidbody>().velocity *= m_shotMoveSpeed;
        }
        else
        //追いかける人なら速度をあげる
        if (this.GetComponent<PlayerActions>().isChase)
        {
            this.GetComponent<Rigidbody>().velocity*=m_chaseSpeed;
        }
      

        //プレイヤーがどの方向に進んでいるかがわかるように、初期位置と現在地の座標差分を取得
        //Obtain the coordinate difference between the initial position and the current location so that the player can see which direction he is moving.
        Vector3 diff = transform.position + ((vel.normalized * GetSpeed()) - transform.position);


        //ベクトルの情報をQuaternion.FromToRotationに引き渡し回転量を取得しプレイヤーを回転させる
        //Pass the vector information to Quaternion.FromToRotation to get the rotation amount and rotate the player.
        if (diff.magnitude >= 0.01f)
        {
            this.transform.rotation = Quaternion.FromToRotation(Vector3.forward, diff.normalized);
        }

    }

    //-------------------------------------
    //キー入力による攻撃　
    //Attack by keystrokes
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    private void AttackKeyInput()
    {
        //水鉄砲発射
        this.GetComponentInChildren<TestPlayerWaterGun>().ShotWater();
    }
}
