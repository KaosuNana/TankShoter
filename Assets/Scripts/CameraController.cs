using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public PlayerController playerInstance;

	public Vector3 offsetPlayerAndCamera;

	public Transform menuMode;

	public Transform gameplayMode;

	private bool isRotating;

	private bool isMenuMode;

	private bool shouldStopFollowingPlayer;

	public float minX;

	public float maxX;

	private Vector3[] pathArray;

	private void Awake()
	{
		this.offsetPlayerAndCamera = base.transform.position - this.playerInstance.transform.position;
		this.FocusOnTankModeWithDelay(0f);
		this.pathArray = new Vector3[]
		{
			new Vector3(7.310169f, 5.763213f, -1.286343f),
			new Vector3(5.897448f, 9.0662f, -5.963159f),
			new Vector3(1.058426f, 15.33568f, -5.892921f),
			new Vector3(0f, 20f, -5f)
		};
	}

	private void Start()
	{
		if (GameSingleton.IsiPhoneX)
		{
			this.minX = -2.75f;
			this.maxX = 2.75f;
		}
	}

	public void ShakeCamera(float amount)
	{
		base.transform.DOShakePosition(1f, 1f * amount, 50, 0f, false, true).SetUpdate(true);
	}

	public void ShakeCameraForHurt()
	{
		base.transform.DOShakePosition(0.5f, new Vector3(0f, 1f, 0f), 20, 0f, false, true).SetUpdate(true);
	}

	public void FocusOnTankModeWithDelay(float delay)
	{
		this.shouldStopFollowingPlayer = true;
		base.transform.DOMove(this.menuMode.position, 0.5f, false).SetDelay(delay);
		base.transform.DORotate(this.menuMode.rotation.eulerAngles, 0.5f, RotateMode.Fast).SetDelay(delay);
	}

	public void StartGameMode()
	{
		base.transform.DORotate(this.gameplayMode.rotation.eulerAngles, 1f, RotateMode.Fast).SetEase(Ease.Linear);
		base.transform.DOPath(this.pathArray, 1f, PathType.CatmullRom, PathMode.Full3D, 10, null).SetEase(Ease.Linear).OnComplete(delegate
		{
			this.shouldStopFollowingPlayer = false;
		});
	}

	private void Update()
	{
		if (!this.shouldStopFollowingPlayer)
		{
			base.transform.position = new Vector3(Mathf.Lerp(base.transform.position.x, Mathf.Clamp(this.playerInstance.transform.position.x + this.offsetPlayerAndCamera.x, this.minX, this.maxX), 0.1f), base.transform.position.y, base.transform.position.z);
		}
	}
}
