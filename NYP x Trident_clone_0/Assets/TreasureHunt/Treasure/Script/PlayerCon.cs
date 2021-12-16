using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerCon : NetworkBehaviour
{
	const float GRAVITY = 19.6f;
	public float speed = 10.0f;


	//プレイヤー情報格納用
	PlayerPos playerPosScript;

	//移動量保存変数
	Vector3 motion;

	GameObject door;
	GameObject wall;

	void Start()
	{
		
		door = GameObject.FindGameObjectWithTag("Door");
		wall = GameObject.FindGameObjectWithTag("Wall");
		SetUpServer();

		//スクリプトのPlayerPosの登録
		playerPosScript =GetComponent<PlayerPos>();
		
	}

	void Update()
	{
		if (!isLocalPlayer) return;

		//カメラが回転中でなければ処理する
		if (playerPosScript.coroutineBool == false)
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

	[ServerCallback]
	void SetUpServer()
	{
		if(door!=null&&!door.GetComponent<HiddenDoor>().hasAuthority)
			door.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
		if (wall != null && !wall.GetComponent<HiddenWall>().hasAuthority)
			wall.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
	}
}
