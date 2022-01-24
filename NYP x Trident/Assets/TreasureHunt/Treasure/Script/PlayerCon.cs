using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerCon : NetworkBehaviour
{
	const float GRAVITY = 19.6f;
	public float speed = 10.0f;
	public bool cameraFlag = false;
	public bool naviFlag = false;

	//プレイヤー情報格納用
	PlayerPos playerPosScript;
	//カウントダウン情報格納用
	CountDown countDownScript;
	GameObject countDownText;

	/// <summary>
	/// //カメラ関係
	public GameObject camera;
	public GameObject cameraPos;
	/// </summary>


	//移動量保存変数
	Vector3 motion;

	GameObject door;
	GameObject wall;

	void Start()
	{
		transform.Find("CameraPos").gameObject.SetActive(isLocalPlayer);
		transform.Find("Camera").gameObject.SetActive(isLocalPlayer);
		transform.Find("PointText").gameObject.SetActive(isLocalPlayer);

		GameObject.Find("PointManager").GetComponent<PointManager>().PlayerList.Add(this.gameObject);

		door = GameObject.FindGameObjectWithTag("Door");
		wall = GameObject.FindGameObjectWithTag("Wall");
		SetUpServer();

		//スクリプトのPlayerPosの登録
		playerPosScript = GetComponent<PlayerPos>();

		countDownText = GameObject.Find("CountDownObject");
		countDownScript = countDownText.GetComponent<CountDown>();
	}

	void Update()
	{
		if (!isLocalPlayer) return;

		//カメラが回転中でなければ処理する
		if (playerPosScript.coroutineBool == false && countDownScript.countDownFlag)
		{
			//CmdPlayerMove();

			float x = Input.GetAxisRaw("Horizontal");
			float z = Input.GetAxisRaw("Vertical");
			float num;
			switch (playerPosScript.direction)
			{
				case 1:
					num = x;
					x = z;
					z = -num;
					break;
				case 2:
					x = -x;
					z = -z;
					break;
				case 3:
					num = x;
					x = -z;
					z = num;
					break;
			}
			motion = new Vector3(x * speed * Time.deltaTime, -GRAVITY, z * speed * Time.deltaTime);
			CmdMove(motion);

		}
	}

	[Command]
	void CmdMove(Vector3 motion)
	{

		transform.GetComponent<CharacterController>().Move(motion);
	}

	void SetCameraFlag(bool flag)
	{
		cameraFlag = flag;
	}

	[ServerCallback]
	void SetUpServer()
	{
		if (door != null && !door.GetComponent<HiddenDoor>().hasAuthority)
			door.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
		if (wall != null && !wall.GetComponent<HiddenWall>().hasAuthority)
			wall.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
	}
}
