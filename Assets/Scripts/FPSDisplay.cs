using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
	private sealed class _DisplayFPS_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _fps___1;

		internal FPSDisplay _this;

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

		public _DisplayFPS_c__Iterator0()
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
				this._fps___1 = (int)(1f / this._this.deltaTime);
				this._this.FPSLabel.text = this._fps___1.ToString();
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

	private float deltaTime;

	private Text FPSLabel;

	private void Awake()
	{
		this.FPSLabel = base.GetComponent<Text>();
	}

	private void Start()
	{
		base.StartCoroutine("DisplayFPS");
	}

	private IEnumerator DisplayFPS()
	{
		FPSDisplay._DisplayFPS_c__Iterator0 _DisplayFPS_c__Iterator = new FPSDisplay._DisplayFPS_c__Iterator0();
		_DisplayFPS_c__Iterator._this = this;
		return _DisplayFPS_c__Iterator;
	}

	private void Update()
	{
		this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
	}
}
