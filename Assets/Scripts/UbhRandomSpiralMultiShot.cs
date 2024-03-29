using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Random Spiral Multi Shot")]
public class UbhRandomSpiralMultiShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _wayAngle___0;

		internal int _wayIndex___0;

		internal int _i___1;

		internal float _waitTime___2;

		internal UbhBullet _bullet___3;

		internal float _bulletSpeed___3;

		internal float _centerAngle___3;

		internal float _minAngle___3;

		internal float _maxAngle___3;

		internal float _angle___3;

		internal UbhRandomSpiralMultiShot _this;

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
				if (this._this.m_bulletNum <= 0 || this._this.m_randomSpeedMin <= 0f || this._this.m_randomSpeedMax <= 0f || this._this.m_spiralWayNum <= 0)
				{
					UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or RandomSpeedMin or RandomSpeedMax or SpiralWayNum is not set.");
					return false;
				}
				if (this._this.m_shooting)
				{
					return false;
				}
				this._this.m_shooting = true;
				this._wayAngle___0 = 360f / (float)this._this.m_spiralWayNum;
				this._wayIndex___0 = 0;
				this._i___1 = 0;
				break;
			case 1u:
				//IL_160:
				this._bullet___3 = this._this.GetBullet(this._this.transform.position, false);
				if (this._bullet___3 == null)
				{
					goto IL_2B2;
				}
				this._bulletSpeed___3 = UnityEngine.Random.Range(this._this.m_randomSpeedMin, this._this.m_randomSpeedMax);
				this._centerAngle___3 = this._this.m_startAngle + this._wayAngle___0 * (float)this._wayIndex___0 + this._this.m_shiftAngle * Mathf.Floor((float)(this._i___1 / this._this.m_spiralWayNum));
				this._minAngle___3 = this._centerAngle___3 - this._this.m_randomRangeSize / 2f;
				this._maxAngle___3 = this._centerAngle___3 + this._this.m_randomRangeSize / 2f;
				this._angle___3 = UnityEngine.Random.Range(this._minAngle___3, this._maxAngle___3);
				this._this.ShotBullet(this._bullet___3, this._bulletSpeed___3, this._angle___3, false, null, 0f, false, 0f, 0f);
				this._wayIndex___0++;
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < this._this.m_bulletNum)
			{
				if (this._this.m_spiralWayNum > this._wayIndex___0)
				{
//					goto IL_160;
				}
				this._wayIndex___0 = 0;
				if (0f <= this._this.m_randomDelayMin && 0f < this._this.m_randomDelayMax)
				{
					this._this.FiredShot();
					this._waitTime___2 = UnityEngine.Random.Range(this._this.m_randomDelayMin, this._this.m_randomDelayMax);
					this._current = UbhUtil.WaitForSeconds(this._waitTime___2);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
//				goto IL_160;
			}
			IL_2B2:
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

	[Header("===== RandomSpiralMultiShot Settings ====="), FormerlySerializedAs("_SpiralWayNum")]
	public int m_spiralWayNum = 4;

	[Range(0f, 360f), FormerlySerializedAs("_StartAngle")]
	public float m_startAngle = 180f;

	[Range(-360f, 360f), FormerlySerializedAs("_ShiftAngle")]
	public float m_shiftAngle = 5f;

	[Range(0f, 360f), FormerlySerializedAs("_RandomRangeSize")]
	public float m_randomRangeSize = 30f;

	[FormerlySerializedAs("_RandomSpeedMin")]
	public float m_randomSpeedMin = 1f;

	[FormerlySerializedAs("_RandomSpeedMax")]
	public float m_randomSpeedMax = 3f;

	[FormerlySerializedAs("_RandomDelayMin")]
	public float m_randomDelayMin = 0.01f;

	[FormerlySerializedAs("_RandomDelayMax")]
	public float m_randomDelayMax = 0.1f;

	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		UbhRandomSpiralMultiShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhRandomSpiralMultiShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}
}
