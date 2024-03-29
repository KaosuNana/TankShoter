using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Sin Wave Bullet nWay Shot")]
public class UbhSinWaveBulletNwayShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _wayIndex___0;

		internal int _i___1;

		internal UbhBullet _bullet___2;

		internal float _baseAngle___2;

		internal float _angle___2;

		internal UbhSinWaveBulletNwayShot _this;

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
				if (this._this.m_bulletNum <= 0 || this._this.m_bulletSpeed <= 0f || this._this.m_wayNum <= 0)
				{
					UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed or WayNum is not set.");
					return false;
				}
				if (this._this.m_shooting)
				{
					return false;
				}
				this._this.m_shooting = true;
				this._wayIndex___0 = 0;
				this._i___1 = 0;
				break;
			case 1u:
				//IL_102:
				this._bullet___2 = this._this.GetBullet(this._this.transform.position, false);
				if (this._bullet___2 == null)
				{
					goto IL_213;
				}
				this._baseAngle___2 = ((this._this.m_wayNum % 2 != 0) ? this._this.m_centerAngle : (this._this.m_centerAngle - this._this.m_betweenAngle / 2f));
				this._angle___2 = UbhUtil.GetShiftedAngle(this._wayIndex___0, this._baseAngle___2, this._this.m_betweenAngle);
				this._this.ShotBullet(this._bullet___2, this._this.m_bulletSpeed, this._angle___2, false, null, 0f, true, this._this.m_waveSpeed, this._this.m_waveRangeSize);
				this._wayIndex___0++;
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < this._this.m_bulletNum)
			{
				if (this._this.m_wayNum > this._wayIndex___0)
				{
//					goto IL_102;
				}
				this._wayIndex___0 = 0;
				if (0f < this._this.m_nextLineDelay)
				{
					this._this.FiredShot();
					this._current = UbhUtil.WaitForSeconds(this._this.m_nextLineDelay);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
//				goto IL_102;
			}
			IL_213:
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

	[Header("===== SinWaveBulletNwayShot Settings ====="), FormerlySerializedAs("_WayNum")]
	public int m_wayNum = 4;

	[Range(0f, 360f), FormerlySerializedAs("_CenterAngle")]
	public float m_centerAngle = 180f;

	[Range(0f, 360f), FormerlySerializedAs("_WaveRangeSize")]
	public float m_waveRangeSize = 40f;

	[Range(0f, 30f), FormerlySerializedAs("_WaveSpeed")]
	public float m_waveSpeed = 10f;

	[Range(0f, 360f), FormerlySerializedAs("_BetweenAngle")]
	public float m_betweenAngle = 10f;

	[FormerlySerializedAs("_NextLineDelay")]
	public float m_nextLineDelay = 0.1f;

	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		UbhSinWaveBulletNwayShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhSinWaveBulletNwayShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}
}
