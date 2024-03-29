using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Random Shot")]
public class UbhRandomShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal List<int> _numList___0;

		internal int _index___1;

		internal UbhBullet _bullet___1;

		internal float _bulletSpeed___1;

		internal float _minAngle___1;

		internal float _maxAngle___1;

		internal float _angle___1;

		internal float _waitTime___2;

		internal UbhRandomShot _this;

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
				if (this._this.m_bulletNum <= 0 || this._this.m_randomSpeedMin <= 0f || this._this.m_randomSpeedMax <= 0f)
				{
					UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or RandomSpeedMin or RandomSpeedMax is not set.");
					return false;
				}
				if (this._this.m_shooting)
				{
					return false;
				}
				this._this.m_shooting = true;
				this._numList___0 = new List<int>(this._this.m_bulletNum);
				for (int i = 0; i < this._this.m_bulletNum; i++)
				{
					this._numList___0.Add(i);
				}
				break;
			case 1u:
				break;
			default:
				return false;
			}
			while (0 < this._numList___0.Count)
			{
				this._index___1 = UnityEngine.Random.Range(0, this._numList___0.Count);
				this._bullet___1 = this._this.GetBullet(this._this.transform.position, false);
				if (this._bullet___1 == null)
				{
					break;
				}
				this._bulletSpeed___1 = UnityEngine.Random.Range(this._this.m_randomSpeedMin, this._this.m_randomSpeedMax);
				this._minAngle___1 = this._this.m_randomCenterAngle - this._this.m_randomRangeSize / 2f;
				this._maxAngle___1 = this._this.m_randomCenterAngle + this._this.m_randomRangeSize / 2f;
				this._angle___1 = 0f;
				if (this._this.m_evenlyDistribute)
				{
					float num2 = Mathf.Floor((float)this._this.m_bulletNum / 4f);
					float num3 = Mathf.Floor((float)this._numList___0[this._index___1] / num2);
					float num4 = Mathf.Abs(this._maxAngle___1 - this._minAngle___1) / 4f;
					this._angle___1 = UnityEngine.Random.Range(this._minAngle___1 + num4 * num3, this._minAngle___1 + num4 * (num3 + 1f));
				}
				else
				{
					this._angle___1 = UnityEngine.Random.Range(this._minAngle___1, this._maxAngle___1);
				}
				this._this.ShotBullet(this._bullet___1, this._bulletSpeed___1, this._angle___1, false, null, 0f, false, 0f, 0f);
				this._numList___0.RemoveAt(this._index___1);
				if (0 < this._numList___0.Count && 0f <= this._this.m_randomDelayMin && 0f < this._this.m_randomDelayMax)
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
			}
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

	[Header("===== RandomShot Settings ====="), Range(0f, 360f), FormerlySerializedAs("_RandomCenterAngle")]
	public float m_randomCenterAngle = 180f;

	[Range(0f, 360f), FormerlySerializedAs("_RandomRangeSize")]
	public float m_randomRangeSize = 360f;

	[FormerlySerializedAs("_RandomSpeedMin")]
	public float m_randomSpeedMin = 1f;

	[FormerlySerializedAs("_RandomSpeedMax")]
	public float m_randomSpeedMax = 3f;

	[FormerlySerializedAs("_RandomDelayMin")]
	public float m_randomDelayMin = 0.01f;

	[FormerlySerializedAs("_RandomDelayMax")]
	public float m_randomDelayMax = 0.1f;

	[FormerlySerializedAs("_EvenlyDistribute")]
	public bool m_evenlyDistribute = true;

	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		UbhRandomShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhRandomShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}
}
