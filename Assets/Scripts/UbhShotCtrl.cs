using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Controller/Shot Controller")]
public sealed class UbhShotCtrl : UbhMonoBehaviour
{
	[Serializable]
	public class ShotInfo
	{
		[FormerlySerializedAs("_ShotObj")]
		public UbhBaseShot m_shotObj;

		[FormerlySerializedAs("_AfterDelay")]
		public float m_afterDelay;
	}

	private sealed class _Start_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal UbhShotCtrl _this;

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

		public _Start_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (!this._this.m_startOnAwake)
				{
					goto IL_7B;
				}
				if (0f < this._this.m_startOnAwakeDelay)
				{
					this._current = UbhUtil.WaitForSeconds(this._this.m_startOnAwakeDelay);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				break;
			case 1u:
				break;
			default:
				return false;
			}
			this._this.StartShotRoutine();
			IL_7B:
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

	private sealed class _ShotCoroutine_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal bool _enableShot___0;

		internal bool _enableDelay___0;

		internal int _nowIndex___0;

		internal UbhShotCtrl _this;

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

		public _ShotCoroutine_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this._this.m_shotList == null || this._this.m_shotList.Count <= 0)
				{
					return false;
				}
				this._enableShot___0 = false;
				for (int i = 0; i < this._this.m_shotList.Count; i++)
				{
					if (this._this.m_shotList[i].m_shotObj != null)
					{
						this._enableShot___0 = true;
						break;
					}
				}
				if (!this._enableShot___0)
				{
					return false;
				}
				this._enableDelay___0 = false;
				for (int j = 0; j < this._this.m_shotList.Count; j++)
				{
					if (0f < this._this.m_shotList[j].m_afterDelay)
					{
						this._enableDelay___0 = true;
						break;
					}
				}
				if (this._this.m_loop && !this._enableDelay___0)
				{
					return false;
				}
				if (this._this.m_shooting)
				{
					return false;
				}
				this._this.m_shooting = true;
				this._this.m_workShotInfoList.AddRange(this._this.m_shotList);
				this._nowIndex___0 = 0;
				break;
			case 1u:
			//IL_25F:
				if (this._this.m_atRandom)
				{
					this._this.m_workShotInfoList.RemoveAt(this._nowIndex___0);
					if (this._this.m_workShotInfoList.Count <= 0)
					{
						if (!this._this.m_loop)
						{
							goto IL_34B;
						}
						this._this.m_workShotInfoList.AddRange(this._this.m_shotList);
					}
				}
				else
				{
					if (!this._this.m_loop && this._this.m_workShotInfoList.Count - 1 <= this._nowIndex___0)
					{
						goto IL_34B;
					}
					this._nowIndex___0 = (int)Mathf.Repeat((float)this._nowIndex___0 + 1f, (float)this._this.m_workShotInfoList.Count);
				}
				if (this._this.m_shooting)
				{
					break;
				}
				IL_34B:
				this._this.m_workShotInfoList.Clear();
				this._this.m_shotRoutineFinishedCallbackEvents.Invoke();
				this._this.m_shooting = false;
				return false;
			default:
				return false;
			}
			if (this._this.m_atRandom)
			{
				this._nowIndex___0 = UnityEngine.Random.Range(0, this._this.m_workShotInfoList.Count);
			}
			if (this._this.m_workShotInfoList[this._nowIndex___0].m_shotObj != null)
			{
				this._this.m_workShotInfoList[this._nowIndex___0].m_shotObj.SetShotCtrl(this._this);
				this._this.m_workShotInfoList[this._nowIndex___0].m_shotObj.Shot();
			}
			if (0f < this._this.m_workShotInfoList[this._nowIndex___0].m_afterDelay)
			{
				this._current = UbhUtil.WaitForSeconds(this._this.m_workShotInfoList[this._nowIndex___0].m_afterDelay);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
            //			goto IL_25F;
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

	[FormerlySerializedAs("_AxisMove")]
	public UbhUtil.AXIS m_axisMove;

	public bool m_inheritAngle;

	[FormerlySerializedAs("_StartOnAwake")]
	public bool m_startOnAwake = true;

	[FormerlySerializedAs("_StartOnAwakeDelay")]
	public float m_startOnAwakeDelay = 1f;

	[FormerlySerializedAs("_Loop")]
	public bool m_loop = true;

	[FormerlySerializedAs("_AtRandom")]
	public bool m_atRandom;

	[FormerlySerializedAs("_ShotList")]
	public List<UbhShotCtrl.ShotInfo> m_shotList = new List<UbhShotCtrl.ShotInfo>();

	[SerializeField, Space(10f)]
	private UnityEvent m_shotRoutineFinishedCallbackEvents = new UnityEvent();

	private bool m_shooting;

	private List<UbhShotCtrl.ShotInfo> m_workShotInfoList = new List<UbhShotCtrl.ShotInfo>(32);

	public bool shooting
	{
		get
		{
			return this.m_shooting;
		}
	}

	private IEnumerator Start()
	{
		UbhShotCtrl._Start_c__Iterator0 _Start_c__Iterator = new UbhShotCtrl._Start_c__Iterator0();
		_Start_c__Iterator._this = this;
		return _Start_c__Iterator;
	}

	public void ResetToTankBuddiesDefault(bool shouldLoop)
	{
		this.m_axisMove = UbhUtil.AXIS.X_AND_Z;
		this.m_startOnAwake = shouldLoop;
		this.m_loop = shouldLoop;
	}

	public void SetShotPattern(UbhBaseShot baseShot, float delay)
	{
		UbhShotCtrl.ShotInfo shotInfo = new UbhShotCtrl.ShotInfo();
		shotInfo.m_shotObj = baseShot;
		shotInfo.m_afterDelay = delay;
		this.m_shotList.Add(shotInfo);
	}

	private void OnDisable()
	{
		this.m_shooting = false;
	}

	public void StartShotRoutine()
	{
		for (int i = 0; i < this.m_workShotInfoList.Count; i++)
		{
			this.m_workShotInfoList[i].m_shotObj.gameObject.SetActive(true);
		}
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		UbhShotCtrl._ShotCoroutine_c__Iterator1 _ShotCoroutine_c__Iterator = new UbhShotCtrl._ShotCoroutine_c__Iterator1();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}

	public void StopShotRoutine()
	{
		base.StopAllCoroutines();
		for (int i = 0; i < this.m_workShotInfoList.Count; i++)
		{
			this.m_workShotInfoList[i].m_shotObj.gameObject.SetActive(false);
		}
		this.m_shooting = false;
	}
}
