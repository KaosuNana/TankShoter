using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Spiral Shot")]
public class UbhSpiralShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal UbhBullet _bullet___2;

		internal float _angle___2;

		internal UbhSpiralShot _this;

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
					UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed is not set.");
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
				//IL_D9:
				this._bullet___2 = this._this.GetBullet(this._this.transform.position, false);
				if (this._bullet___2 == null)
				{
					goto IL_18E;
				}
				this._angle___2 = this._this.m_startAngle + this._this.m_shiftAngle * (float)this._i___1;
				this._this.ShotBullet(this._bullet___2, this._this.m_bulletSpeed, this._angle___2, false, null, 0f, false, 0f, 0f);
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
//				goto IL_D9;
			}
			IL_18E:
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

	[Header("===== SpiralShot Settings ====="), Range(0f, 360f), FormerlySerializedAs("_StartAngle")]
	public float m_startAngle = 180f;

	[Range(-360f, 360f), FormerlySerializedAs("_ShiftAngle")]
	public float m_shiftAngle = 5f;

	[FormerlySerializedAs("_BetweenDelay")]
	public float m_betweenDelay = 0.2f;

	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		UbhSpiralShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhSpiralShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}
}
