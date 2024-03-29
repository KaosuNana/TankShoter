using DG.Tweening;
using System;
using UnityEngine;

public class CameraControllerOld : MonoBehaviour
{
	public PlayerController playerInstance;

	public Vector3 offsetPlayerAndCamera;

	public Transform jetpackMode;

	public Transform giantMode;

	public Transform normalMode;

	public Transform menuMode;

	public Transform gameOverMode;

	private float currentRotation;

	private bool isRotating;

	private bool isGameOver;

	private bool isMenuMode;

	private void Start()
	{
		this.isMenuMode = true;
		this.offsetPlayerAndCamera = base.transform.position - this.playerInstance.transform.position;
		base.transform.position = this.menuMode.transform.position;
		base.transform.DORotate(this.menuMode.transform.rotation.eulerAngles, 0f, RotateMode.Fast);
	}

	public void FollowPlayer(Transform playerTransform)
	{
		base.transform.DOMoveZ(playerTransform.position.z + this.offsetPlayerAndCamera.z, 0.2f, false);
	}

	public void JetpackMode()
	{
		base.transform.DOMove(this.jetpackMode.position, 0.25f, false);
		base.transform.DORotate(this.jetpackMode.transform.rotation.eulerAngles, 0.25f, RotateMode.Fast);
		this.isGameOver = false;
	}

	public void GiantMode()
	{
		base.transform.DOMove(this.giantMode.position, 0.25f, false);
		base.transform.DORotate(this.giantMode.transform.rotation.eulerAngles, 0.25f, RotateMode.Fast);
		this.isGameOver = false;
	}

	public void StartGameMode()
	{
		base.transform.DORotate(this.normalMode.transform.rotation.eulerAngles, 0.5f, RotateMode.Fast);
		base.transform.DOMove(this.normalMode.position * 1.2f, 0.3f, false).OnComplete(delegate
		{
			base.transform.DOMove(this.normalMode.position, 0.25f, false).OnComplete(delegate
			{
				this.isMenuMode = false;
				this.isGameOver = false;
				base.transform.DORotate(this.normalMode.transform.rotation.eulerAngles, 0.75f, RotateMode.Fast);
			});
		});
	}

	public void NormalMode()
	{
		base.transform.DOMove(this.normalMode.position, 0.25f, false);
	}

	public void GameOverMode()
	{
		base.transform.DOMove(new Vector3(base.transform.position.x, this.gameOverMode.position.y, this.gameOverMode.position.z), 3f, false);
		this.isGameOver = true;
	}

	public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
	{
		Vector3 vector = point - pivot;
		vector = Quaternion.Euler(angles) * vector;
		point = vector + pivot;
		return point;
	}
}
