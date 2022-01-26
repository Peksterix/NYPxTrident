//==============================================
//Day           :10/26
//Creator       :HashizumeAtsuki
//Description   :プレイヤーの操作
//               Player control
//==============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class WGTPlayerController : GameObjectBase
{
    //プレイヤーの速度 Player Speed
    [SerializeField] float m_playerSpeed = 3;

    //追いかけるときの速度
    [SerializeField] float m_chaseSpeed = 1.1f;

    //制限時間
    public GameObject m_time;

    //撃っているときの移動速度
    [SerializeField] float m_shotMoveSpeed = 0.7f;

    //マウスカーソルオブジェクト
    private GameObject m_mouseCursor;

    //ポーズUI
    private GameObject m_pauseUI;

    //操作不能状態にする
    public bool m_isInoperable;

    //ミニマップ用のカメラ
    private GameObject m_miniMapCamera;

    //ゲームマネージャー
    public GameObject m_wgtGameManager;

    //プレイヤーの番号
    public int m_playerNum;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
            return;

        SetSpeed(m_playerSpeed);

        m_isInoperable = false;

        m_time = GameObject.Find("Time");

        m_miniMapCamera = GameObject.Find("MiniMapCamera");

        m_mouseCursor = GameObject.Find("MouseCursor");

        m_pauseUI = GameObject.Find("PauseUI");
        m_pauseUI.GetComponent<PauseUI>().SetPlayer(this.gameObject);

        GameObject playerManager = GameObject.Find("PlayerManager");
        playerManager.GetComponent<PlayerManager>().GetPlayerList().Add(this);

        GameObject waterGaugeUI = GameObject.Find("WaterGauge");
        waterGaugeUI.GetComponent<WaterGauge>().SetPlayerWaterGun(this.transform.Find("WaterGun3D").gameObject);

        GameObject pointUI = GameObject.Find("Point");
        pointUI.GetComponent<PointUI>().SetPlayer(this.gameObject);

        GameObject playerNum = GameObject.Find("PlayerNumUI");
        playerNum.GetComponent<PlayerNumUI>().SetPlayer(this.gameObject);

        m_wgtGameManager = GameObject.Find("WGTGameManager");

        //カメラを追従させる
        //Camera.main.transform.position = new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(0, Camera.main.transform.position.y, -3.6f);
        CameraManager.Instance.AssignCameraTarget(transform, transform);

        //ミニマップをスクロールさせる
        m_miniMapCamera.GetComponent<Camera>().transform.position = new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(0, m_miniMapCamera.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer || m_isInoperable)
            return;

        //if (m_wgtGameManager.GetComponent<WGTGameManager>().GetIsStopGame() ||
        //    this.GetComponent<PlayerActions>().GetIsStunting()
        //    )
        if (WGTGameManager.GetCurrGameState() == WGTGameManager.GameState.Ended ||
            this.GetComponent<PlayerActions>().isStunting
            )
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
        //AttackKeyInput();

        if (Input.GetMouseButtonUp(0))
        {
            GetComponent<AudioSource>().Stop();
            CmdAttack(false);
        }
        //for Audio
        else if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
            CmdAttack(true);
        }
        else if (Input.GetMouseButton(0))
        {

            CmdAttack(true);
        }

        if(!this.transform.Find("WaterGun3D").GetComponent<WaterGun>().m_isShotWaterGun)
        {
            GetComponent<AudioSource>().Stop();
        }

        //ポーズ
        PasueKeyInput();
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

        //KeyInput=========================================
        ////上キーが押されたら
        //if(Input.GetKey(KeyCode.UpArrow))
        //{

        //    //上に進む
        //    vel += Vector3.forward;

        //}

        ////下キーが押されたら
        //if (Input.GetKey(KeyCode.DownArrow))
        //{

        //    //下に進む
        //    vel += Vector3.back;

        //}

        ////左キーが押されたら
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{

        //    //左に進む
        //    vel += Vector3.left;
        //}

        ////右キーが押されたら
        //if (Input.GetKey(KeyCode.RightArrow))
        //{

        //    //右に進む
        //    vel += Vector3.right;
        //}


        //Mouse==========================================
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        foreach (RaycastHit raycastHit in Physics.RaycastAll(ray))
        {

            if (raycastHit.collider.CompareTag("RayCastPlane"))
            {

                Vector3 mouse = raycastHit.point;
                mouse.y = 0.0f;

                m_mouseCursor.transform.position = mouse;




            }

        }

        vel = m_mouseCursor.transform.position - transform.position;

        vel.y = 0.0f;

        if (vel.magnitude <= 0.1f)
        {
            this.GetComponent<Rigidbody>().velocity = (vel.normalized * GetSpeed() * 0.0f);
            return;
        }

        else if (vel.magnitude >= 1.0f)
        {
            //速度を反映させる Reflect the velocity
            this.GetComponent<Rigidbody>().velocity = (vel.normalized * GetSpeed());
        }
        else
        {
            //速度を反映させる Reflect the velocity
            this.GetComponent<Rigidbody>().velocity = (vel.normalized * GetSpeed() * vel.magnitude);
        }


        //撃っている間は移動速度を落とす
        if (this.GetComponentInChildren<WaterGun>().GetIsShotWaterGun())
        {
            this.GetComponent<Rigidbody>().velocity *= m_shotMoveSpeed;
        }else
        //追いかける人なら速度をあげる
        if (this.GetComponent<PlayerActions>().isStunting)
        {
            this.GetComponent<Rigidbody>().velocity *= m_chaseSpeed;
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


        //カメラを追従させる
        //Camera.main.transform.position = new Vector3(transform.position.x,0, transform.position.z) + new Vector3(0, Camera.main.transform.position.y, -3.6f);

        //ミニマップをスクロールさせる
        m_miniMapCamera.GetComponent<Camera>().transform.position = new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(0, m_miniMapCamera.transform.position.y,0);
    }

    //-------------------------------------
    //キー入力による攻撃　
    //Attack by keystrokes
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    //private void AttackKeyInput()
    //{
    //    //水鉄砲発射
    //    this.GetComponentInChildren<WaterGun>().ShotWater();
    //}

    //-------------------------------------
    //キー入力によるポーズ画面の表示
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    private void PasueKeyInput()
    {
        m_pauseUI.GetComponent<PauseUI>().InputKeyPause();
    }

    //-------------------------------------
    //他のプレイヤーの視認判定(実装するか考え)
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------
    private void IsSeeOtherPlayer()
    {

    }

    //-------------------------------------
    //他のプレイヤーのいる方向を教える
    //
    //引数     :なし　None
    //戻り値   :なし　None
    //-------------------------------------

    #region Network Commands
    [Command] [ContextMenu("Seppuku")]
    public void CmdSeppuku()
    {
        GetComponent<PlayerActions>().ChangeChase();
    }


    [Command]
    void CmdAttack(bool isAttacking)
    {
        WaterGun waterGun = GetComponentInChildren<WaterGun>();

        bool prevState = waterGun.m_isShotWaterGun;
        bool currState = waterGun.ShootWaterGun(isAttacking);

        if (prevState != currState && isAttacking)
            RpcAttack();
        else if (prevState != currState && !isAttacking)
            RpcStopAttack();
    }

    [ClientRpc]
    void RpcAttack()
    {
        WaterGun waterGun = GetComponentInChildren<WaterGun>();
        waterGun.FireWaterGunAnimation();
    }

    [ClientRpc]
    void RpcStopAttack()
    {
        WaterGun waterGun = GetComponentInChildren<WaterGun>();
        waterGun.HoldWaterGunAnimation();
    }
    #endregion
}
