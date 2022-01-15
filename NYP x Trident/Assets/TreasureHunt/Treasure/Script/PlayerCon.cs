using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCon : MonoBehaviour
{
	//�v���C���[���i�[�p
	[SerializeField] GameObject playerPos;
	PlayerPos playerPosScript;

	//�ړ���
	Vector3 movingDirecion;
	//�X�s�[�h�����p�ϐ�
	public float speedMagnification;
	//���W�b�h�{�f�B
	Rigidbody rb;
	//�ړ������p�̕ϐ�
	Vector3 movingVelocity;
	//�v���C���[�������Ă�����������m����ϐ�
	bool playerDirFlag;
	//�B�����ɐڂ��Ă��邩���f����ϐ�
	public bool hallFlag;
	//�C���[�W��UI�̏��i�[�p�ϐ�
	public Image image;

	void Start()
	{

		image.enabled = false;
		//�X�N���v�g��PlayerPos�̓o�^
		playerPosScript = playerPos.GetComponent<PlayerPos>();
		//���W�b�h�{�f�B�̓o�^
		rb = GetComponent<Rigidbody>();
		//bool�ϐ��̏�����
		playerDirFlag = false;
		hallFlag = false;
	}

	void Update()
	{
		//�J��������]���łȂ���Ώ�������
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
			//�΂߂̋����������Ȃ�̂�h��
			movingDirecion.Normalize();
			//�ړ��ʂ���
			movingVelocity = movingDirecion * speedMagnification;
		}
	}

	private void FixedUpdate()
	{
		//�ړ�����
		rb.velocity = new Vector3(movingVelocity.x, rb.velocity.y, movingVelocity.z);
	}

	public void SetPlayerDir()
	{
		//�v���C���[�̌����Ă�������t���O�̔��]
		playerDirFlag = !playerDirFlag;
	}
	public void SetPlayerRot(int direction)
	{
		//�����Ă�������ɍ��킹�ăJ�����̈ʒu�𒲐�
		if(direction==0||direction==2)
			transform.rotation = Quaternion.Euler(0, 90, 0);
		else
			transform.rotation = Quaternion.Euler(0, 70, 0);
	}

	private void OnCollisionStay(Collision collision)
	{
		//�G��Ă���I�u�W�F�N�g�ɉ����ăt���O��ς���
		if (collision.gameObject.tag == "TrasureFloor")
		{
			hallFlag = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		//�G��Ă���I�u�W�F�N�g�ɉ����ăt���O��ς���
		if (collision.gameObject.tag == "TrasureFloor")
		{
			hallFlag = false;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		//�G��Ă���I�u�W�F�N�g�ɉ����ăt���O��ς���
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
		//�G��Ă���I�u�W�F�N�g�ɉ����ăt���O��ς���
		if (other.gameObject.tag == "Pit")
		{
			if (Input.GetKey(KeyCode.Space))
				transform.Translate(0, 0.02f, 0);
		}
	}
}
