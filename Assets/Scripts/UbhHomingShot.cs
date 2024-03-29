using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Homing Shot")]
public class UbhHomingShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal UbhBullet _bullet___2;

		internal float _angle___2;

		internal UbhHomingShot _this;

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

		public _ShotCoroutine_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this._this.m_bulletNum <= 0 || this._this.m_bulletSpeed <= 0f)
				{
					return false;
				}
				if (this._this.m_shooting)
				{
					return false;
				}
				this._this.m_shooting = true;
				this._i___1 = 0;
				break;
			case 1u:
				//IL_CF:
				this._bullet___2 = this._this.GetBullet(this._this.transform.position, false);
				if (this._bullet___2 == null)
				{
					goto IL_1EC;
				}
				if (this._this.m_targetTransform == null && this._this.m_setTargetFromTag)
				{
					this._this.m_targetTransform = UbhUtil.GetTransformFromTagName(this._this.m_targetTagName, this._this.m_randomSelectTagTarget);
				}
				this._angle___2 = UbhUtil.GetAngleFromTwoPosition(this._this.transform, this._this.m_targetTransform, this._this.shotCtrl.m_axisMove);
				this._this.ShotBullet(this._bullet___2, this._this.m_bulletSpeed, this._angle___2, true, this._this.m_targetTransform, this._this.m_homingAngleSpeed, false, 0f, 0f);
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < this._this.m_bulletNum)
			{
				if (0 < this._i___1 && 0f < this._this.m_betweenDelay)
				{
					this._this.FiredShot();
					this._current = UbhUtil.WaitForSeconds(this._this.m_betweenDelay);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
//				goto IL_CF;
			}
			IL_1EC:
			this._this.FiredShot();
			this._this.FinishedShot();
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

	[Header("===== HomingShot Settings ====="), FormerlySerializedAs("_BetweenDelay")]
	public float m_betweenDelay = 0.1f;

	[FormerlySerializedAs("_HomingAngleSpeed")]
	public float m_homingAngleSpeed = 20f;

	[FormerlySerializedAs("_SetTargetFromTag")]
	public bool m_setTargetFromTag = true;

	[FormerlySerializedAs("_TargetTagName")]
	public string m_targetTagName = "Player";

	public bool m_randomSelectTagTarget;

	[FormerlySerializedAs("_TargetTransform")]
	public Transform m_targetTransform;

	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		UbhHomingShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhHomingShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}
}
