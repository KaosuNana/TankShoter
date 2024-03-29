using System;
using UnityEngine;

public class UniAndroidPermission : MonoBehaviour
{
	private static Action permitCallBack;

	private static Action notPermitCallBack;

	private const string PackageClassName = "net.sanukin.PermissionManager";

	private AndroidJavaClass permissionManager;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public static bool IsPermitted(AndroidPermission permission)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.sanukin.PermissionManager");
		return androidJavaClass.CallStatic<bool>("hasPermission", new object[]
		{
			UniAndroidPermission.GetPermittionStr(permission)
		});
	}

	public static void RequestPremission(AndroidPermission permission, Action onPermit = null, Action notPermit = null)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.sanukin.PermissionManager");
		androidJavaClass.CallStatic("requestPermission", new object[]
		{
			UniAndroidPermission.GetPermittionStr(permission)
		});
		UniAndroidPermission.permitCallBack = onPermit;
		UniAndroidPermission.notPermitCallBack = notPermit;
	}

	private static string GetPermittionStr(AndroidPermission permittion)
	{
		return "android.permission." + permittion.ToString();
	}

	private void OnPermit()
	{
		if (UniAndroidPermission.permitCallBack != null)
		{
			UniAndroidPermission.permitCallBack();
		}
		this.ResetCallBacks();
	}

	private void NotPermit()
	{
		if (UniAndroidPermission.notPermitCallBack != null)
		{
			UniAndroidPermission.notPermitCallBack();
		}
		this.ResetCallBacks();
	}

	private void ResetCallBacks()
	{
		UniAndroidPermission.notPermitCallBack = null;
		UniAndroidPermission.permitCallBack = null;
	}
}
