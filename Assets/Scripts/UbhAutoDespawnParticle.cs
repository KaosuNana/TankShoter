using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class UbhAutoDespawnParticle : UbhMonoBehaviour
{
	private sealed class _CheckIfAliveCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal ParticleSystem _pSystem___0;

		internal UbhAutoDespawnParticle _this;

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

		public _CheckIfAliveCoroutine_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._pSystem___0 = this._this.GetComponent<ParticleSystem>();
				break;
			case 1u:
				if (!this._pSystem___0.IsAlive(true))
				{
					UnityEngine.Object.Destroy(this._this.gameObject);
				}
				break;
			default:
				return false;
			}
			this._current = new WaitForSeconds(1f);
			if (!this._disposing)
			{
				this._PC = 1;
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

	private void OnEnable()
	{
		base.StartCoroutine(this.CheckIfAliveCoroutine());
	}

	private IEnumerator CheckIfAliveCoroutine()
	{
		UbhAutoDespawnParticle._CheckIfAliveCoroutine_c__Iterator0 _CheckIfAliveCoroutine_c__Iterator = new UbhAutoDespawnParticle._CheckIfAliveCoroutine_c__Iterator0();
		_CheckIfAliveCoroutine_c__Iterator._this = this;
		return _CheckIfAliveCoroutine_c__Iterator;
	}
}
