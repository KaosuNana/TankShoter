using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class UbhUtil
{
	public enum AXIS
	{
		X_AND_Y,
		X_AND_Z
	}

	private sealed class _WaitForSeconds_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _elapsedTime___0;

		internal float waitTime;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _WaitForSeconds_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._elapsedTime___0 = 0f;
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (this._elapsedTime___0 < this.waitTime)
			{
				this._elapsedTime___0 += UbhSingletonMonoBehavior<UbhTimer>.instance.deltaTime;
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public static readonly Vector3 VECTOR3_ZERO = Vector3.zero;

	public static readonly Vector3 VECTOR3_ONE = Vector3.one;

	public static readonly Vector3 VECTOR3_HALF = new Vector3(0.5f, 0.5f, 0.5f);

	public static readonly Vector2 VECTOR2_ZERO = Vector2.zero;

	public static readonly Vector2 VECTOR2_ONE = Vector2.one;

	public static readonly Vector2 VECTOR2_HALF = new Vector2(0.5f, 0.5f);

	public static readonly Quaternion QUATERNION_IDENTITY = Quaternion.identity;

	public static bool IsMobilePlatform()
	{
		return true;
	}

	public static IEnumerator WaitForSeconds(float waitTime)
	{
		UbhUtil._WaitForSeconds_c__Iterator0 _WaitForSeconds_c__Iterator = new UbhUtil._WaitForSeconds_c__Iterator0();
		_WaitForSeconds_c__Iterator.waitTime = waitTime;
		return _WaitForSeconds_c__Iterator;
	}

	public static Transform GetTransformFromTagName(string tagName, bool randomSelect)
	{
		if (string.IsNullOrEmpty(tagName))
		{
			return null;
		}
		GameObject gameObject = null;
		if (randomSelect)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tagName);
			if (array != null && array.Length > 0)
			{
				gameObject = array[UnityEngine.Random.Range(0, array.Length)];
			}
		}
		else
		{
			gameObject = GameObject.FindWithTag(tagName);
		}
		if (gameObject == null)
		{
			return null;
		}
		return gameObject.transform;
	}

	public static float GetShiftedAngle(int wayIndex, float baseAngle, float betweenAngle)
	{
		return (wayIndex % 2 != 0) ? (baseAngle + betweenAngle * Mathf.Ceil((float)wayIndex / 2f)) : (baseAngle - betweenAngle * (float)wayIndex / 2f);
	}

	public static float GetNormalizedAngle(float angle)
	{
		while (angle < 0f)
		{
			angle += 360f;
		}
		while (360f < angle)
		{
			angle -= 360f;
		}
		return angle;
	}

	public static float GetAngleFromTwoPosition(Transform fromTrans, Transform toTrans, UbhUtil.AXIS axisMove)
	{
		if (axisMove == UbhUtil.AXIS.X_AND_Y)
		{
			return UbhUtil.GetZangleFromTwoPosition(fromTrans, toTrans);
		}
		if (axisMove != UbhUtil.AXIS.X_AND_Z)
		{
			return 0f;
		}
		return UbhUtil.GetYangleFromTwoPosition(fromTrans, toTrans);
	}

	private static float GetZangleFromTwoPosition(Transform fromTrans, Transform toTrans)
	{
		if (fromTrans == null || toTrans == null)
		{
			return 0f;
		}
		float x = toTrans.position.x - fromTrans.position.x;
		float y = toTrans.position.y - fromTrans.position.y;
		float angle = Mathf.Atan2(y, x) * 57.29578f - 90f;
		return UbhUtil.GetNormalizedAngle(angle);
	}

	private static float GetYangleFromTwoPosition(Transform fromTrans, Transform toTrans)
	{
		if (fromTrans == null || toTrans == null)
		{
			return 0f;
		}
		float x = toTrans.position.x - fromTrans.position.x;
		float y = toTrans.position.z - fromTrans.position.z;
		float angle = Mathf.Atan2(y, x) * 57.29578f - 90f;
		return UbhUtil.GetNormalizedAngle(angle);
	}
}
