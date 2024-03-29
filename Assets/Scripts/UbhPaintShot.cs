using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Paint Shot")]
public class UbhPaintShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal List<List<int>> _paintData___0;

		internal float _paintStartAngle___1;

		internal int _lineCnt___2;

		internal List<int> _line___3;

		internal UbhPaintShot _this;

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
				if (this._this.m_bulletSpeed <= 0f || this._this.m_paintDataText == null)
				{
					UnityEngine.Debug.LogWarning("Cannot shot because BulletSpeed or PaintDataText is not set.");
					return false;
				}
				if (this._this.m_shooting)
				{
					return false;
				}
				this._this.m_shooting = true;
				this._paintData___0 = this._this.LoadPaintData();
				if (this._paintData___0 == null)
				{
					goto IL_27C;
				}
				this._paintStartAngle___1 = this._this.m_paintCenterAngle;
				if (0 < this._paintData___0.Count)
				{
					this._paintStartAngle___1 -= ((this._paintData___0[0].Count % 2 != 0) ? (this._this.m_betweenAngle * Mathf.Floor((float)this._paintData___0[0].Count / 2f)) : (this._this.m_betweenAngle * (float)this._paintData___0[0].Count / 2f + this._this.m_betweenAngle / 2f));
				}
				this._lineCnt___2 = 0;
				break;
			case 1u:
			//	IL_1BC:
				for (int i = 0; i < this._line___3.Count; i++)
				{
					if (this._line___3[i] == 1)
					{
						UbhBullet bullet = this._this.GetBullet(this._this.transform.position, false);
						if (bullet == null)
						{
							break;
						}
						float angle = this._paintStartAngle___1 + this._this.m_betweenAngle * (float)i;
						this._this.ShotBullet(bullet, this._this.m_bulletSpeed, angle, false, null, 0f, false, 0f, 0f);
					}
				}
				this._lineCnt___2++;
				break;
			default:
				return false;
			}
			if (this._lineCnt___2 < this._paintData___0.Count)
			{
				this._line___3 = this._paintData___0[this._lineCnt___2];
				if (0 < this._lineCnt___2 && 0f < this._this.m_nextLineDelay)
				{
					this._this.FiredShot();
					this._current = UbhUtil.WaitForSeconds(this._this.m_nextLineDelay);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
//				goto IL_1BC;
			}
			IL_27C:
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

	private static readonly string[] SPLIT_VAL = new string[]
	{
		"\n",
		"\r",
		"\r\n"
	};

	[Header("===== PaintShot Settings ====="), FormerlySerializedAs("_PaintDataText")]
	public TextAsset m_paintDataText;

	[Range(0f, 360f), FormerlySerializedAs("_PaintCenterAngle")]
	public float m_paintCenterAngle = 180f;

	[Range(0f, 360f), FormerlySerializedAs("_BetweenAngle")]
	public float m_betweenAngle = 3f;

	[FormerlySerializedAs("_NextLineDelay")]
	public float m_nextLineDelay = 0.1f;

	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		UbhPaintShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhPaintShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}

	private List<List<int>> LoadPaintData()
	{
		if (string.IsNullOrEmpty(this.m_paintDataText.text))
		{
			UnityEngine.Debug.LogWarning("Cannot load paint data because PaintDataText file is empty.");
			return null;
		}
		string[] array = this.m_paintDataText.text.Split(UbhPaintShot.SPLIT_VAL, StringSplitOptions.RemoveEmptyEntries);
		List<List<int>> list = new List<List<int>>(array.Length);
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].StartsWith("#"))
			{
				list.Add(new List<int>(array[i].Length));
				for (int j = 0; j < array[i].Length; j++)
				{
					list[list.Count - 1].Add((array[i][j] != '*') ? 0 : 1);
				}
			}
		}
		list.Reverse();
		return list;
	}
}
