using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCon : MonoBehaviour
{
	//プレイヤー情報格納用
	[SerializeField] GameObject playerPos;
	PlayerPos playerPosScript;

	//移動量
	Vector3 movingDirecion;
	//スピード調整用変数
	public float speedMagnification;
	//リジッドボディ
	Rigidbody rb;
	//移動処理用の変数
	Vector3 movingVelocity;
	//プレイヤーが向いている方向を感知する変数
	bool playerDirFlag;
	//隠し扉に接しているか判断する変数
	public bool hallFlag;
	//イメージのUIの情報格納用変数
	public Image image;

	void Start()
	{

		image.enabled = false;
		//スクリプトのPlayerPosの登録
		playerPosScript = playerPos.GetComponent<PlayerPos>();
		//リジッドボディの登録
		rb = GetComponent<Rigidbody>();
		//bool変数の初期化
		playerDirFlag = false;
		hallFlag = false;
	}

	void Update()
	{
		//カメラが回転中でなければ処理する
		if (playerPosScript.coroutineBool == false)
		{

			float x = Input.GetAxisRaw("Horizontal");
			float z = Input.GetAxisRaw("Vertical");
			switch (playerPosScript.direction)
			{
				case 0:
					//movingDirecion = new Vector3(x, 0, 0);
					movingDirecion = new Vector3(x, 0, z);
					break;
				case 1:
					//movingDirecion = new Vector3(0, 0, -x);
					movingDirecion = new Vector3(z, 0, -x);
					break;
				case 2:
					//movingDirecion = new Vector3(-x, 0, 0);
					movingDirecion = new Vector3(-x, 0, -z);
					break;
				case 3:
					//movingDirecion = new Vector3(0, 0, x);
					movingDirecion = new Vector3(-z, 0, x);
					break;

			}
			//斜めの距離が長くなるのを防ぐ
			movingDirecion.Normalize();
			//移動量を代入
			movingVelocity = movingDirecion * speedMagnification;
		}
	}

	private void FixedUpdate()
	{
		//移動処理
		rb.velocity = new Vector3(movingVelocity.x, rb.velocity.y, movingVelocity.z);
	}

	public void SetPlayerDir()
	{
		//プレイヤーの向いている方向フラグの反転
		playerDirFlag = !playerDirFlag;
	}
	public void SetPlayerRot(int direction)
	{
		//向いている方向に合わせてカメラの位置を調整
		if(direction==0||direction==2)
			transform.rotation = Quaternion.Euler(0, 90, 0);
		else
			transform.rotation = Quaternion.Euler(0, 70, 0);
	}

	private void OnCollisionStay(Collision collision)
	{
		//触れているオブジェクトに応じてフラグを変える
		if (collision.gameObject.tag == "TrasureFloor")
		{
			hallFlag = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		//触れているオブジェクトに応じてフラグを変える
		if (collision.gameObject.tag == "TrasureFloor")
		{
			hallFlag = false;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		//触れているオブジェクトに応じてフラグを変える
		if (other.gameObject.tag == "TrasureFloor")
		{
			hallFlag = false;
		}
		if(other.gameObject.tag=="Pit")
		{
			Physics.gravity = new Vector3(0, -19.6f, 0);
			image.enabled = false;
		}
		if (other.gameObject.tag == "PitGravity")
		{
			image.enabled = true;
			Physics.gravity = new Vector3(0, 0f, 0);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		//触れているオブジェクトに応じてフラグを変える
		if (other.gameObject.tag == "Pit")
		{
			if (Input.GetKey(KeyCode.Space))
				transform.Translate(0, 0.02f, 0);
		}
	}
}
