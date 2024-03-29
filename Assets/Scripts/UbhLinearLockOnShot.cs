using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Linear Shot (Lock On)")]
public class UbhLinearLockOnShot : UbhLinearShot
{
	private sealed class _AimingCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal UbhLinearLockOnShot _this;

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

		public _AimingCoroutine_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (this._this.m_aiming)
			{
				if (this._this.m_shooting)
				{
					this._this.AimTarget();
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
			}
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

	[Header("===== LinearLockOnShot Settings ====="), FormerlySerializedAs("_SetTargetFromTag")]
	public bool m_setTargetFromTag = true;

	[FormerlySerializedAs("_TargetTagName")]
	public string m_targetTagName = "Player";

	public bool m_randomSelectTagTarget;

	[FormerlySerializedAs("_TargetTransform")]
	public Transform m_targetTransform;

	[FormerlySerializedAs("_Aiming")]
	public bool m_aiming;

	public override bool lockOnShot
	{
		get
		{
			return true;
		}
	}

	public override void Shot()
	{
		if (this.m_shooting)
		{
			return;
		}
		this.AimTarget();
		if (this.m_targetTransform == null)
		{
			return;
		}
		base.Shot();
		if (this.m_aiming)
		{
			base.StartCoroutine(this.AimingCoroutine());
		}
	}

	private void AimTarget()
	{
		if (this.m_targetTransform == null && this.m_setTargetFromTag)
		{
			this.m_targetTransform = UbhUtil.GetTransformFromTagName(this.m_targetTagName, this.m_randomSelectTagTarget);
		}
		if (this.m_targetTransform != null)
		{
			this.m_angle = UbhUtil.GetAngleFromTwoPosition(base.transform, this.m_targetTransform, base.shotCtrl.m_axisMove);
		}
	}

	private IEnumerator AimingCoroutine()
	{
		UbhLinearLockOnShot._AimingCoroutine_c__Iterator0 _AimingCoroutine_c__Iterator = new UbhLinearLockOnShot._AimingCoroutine_c__Iterator0();
		_AimingCoroutine_c__Iterator._this = this;
		return _AimingCoroutine_c__Iterator;
	}
}
