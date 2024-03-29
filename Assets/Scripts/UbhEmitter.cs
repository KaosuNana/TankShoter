using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class UbhEmitter : UbhMonoBehaviour
{
	private sealed class _Start_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GameObject _wave___1;

		internal Transform _waveTrans___1;

		internal UbhEmitter _this;

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
				if (this._this.m_waves.Length == 0)
				{
					return false;
				}
				this._this.m_manager = UnityEngine.Object.FindObjectOfType<UbhGameManager>();
				break;
			case 1u:
				//IL_6C:
				if (this._this.m_manager.IsPlaying())
				{
					this._wave___1 = UnityEngine.Object.Instantiate<GameObject>(this._this.m_waves[this._this.m_currentWave], this._this.transform);
					this._waveTrans___1 = this._wave___1.transform;
					this._waveTrans___1.position = this._this.transform.position;
					goto IL_FA;
				}
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 2u:
				goto IL_FA;
			default:
				return false;
			}
			IL_4C:
			//goto IL_6C;
			IL_FA:
			if (0 >= this._waveTrans___1.childCount)
			{
				UnityEngine.Object.Destroy(this._wave___1);
				this._this.m_currentWave = (int)Mathf.Repeat((float)this._this.m_currentWave + 1f, (float)this._this.m_waves.Length);
				goto IL_4C;
			}
			this._current = null;
			if (!this._disposing)
			{
				this._PC = 2;
			}
			return true;
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

	[FormerlySerializedAs("_Waves"), SerializeField]
	private GameObject[] m_waves;

	private int m_currentWave;

	private UbhGameManager m_manager;

	private IEnumerator Start()
	{
		UbhEmitter._Start_c__Iterator0 _Start_c__Iterator = new UbhEmitter._Start_c__Iterator0();
		_Start_c__Iterator._this = this;
		return _Start_c__Iterator;
	}
}
