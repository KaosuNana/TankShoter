using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UbhShowcaseCtrl : MonoBehaviour
{
	private sealed class _StartShot_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _cntTimer___0;

		internal UbhShotCtrl _shotCtrl___0;

		internal UbhShowcaseCtrl _this;

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

		public _StartShot_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._cntTimer___0 = 0f;
				break;
			case 1u:
				break;
			case 2u:
				this._shotCtrl___0 = this._this.m_goShotCtrlList[this._this.m_nowIndex].GetComponent<UbhShotCtrl>();
				if (this._shotCtrl___0 != null)
				{
					this._shotCtrl___0.StartShotRoutine();
				}
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this._cntTimer___0 >= 1f)
			{
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 2;
				}
			}
			else
			{
				this._cntTimer___0 += UbhSingletonMonoBehavior<UbhTimer>.instance.deltaTime;
				this._current = null;
				if (!this._disposing)
				{
					this._PC = 1;
				}
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

	[FormerlySerializedAs("_GoShotCtrlList"), SerializeField]
	private GameObject[] m_goShotCtrlList;

	[SerializeField]
	private Text m_shotNameText;

	private int m_nowIndex;

	private string m_nowGoName;

	private void Start()
	{
		if (this.m_goShotCtrlList != null)
		{
			for (int i = 0; i < this.m_goShotCtrlList.Length; i++)
			{
				this.m_goShotCtrlList[i].SetActive(false);
			}
		}
		this.m_nowIndex = -1;
		this.ChangeShot(true);
	}

	public void ChangeShot(bool toNext)
	{
		if (this.m_goShotCtrlList == null)
		{
			return;
		}
		base.StopAllCoroutines();
		if (0 <= this.m_nowIndex && this.m_nowIndex < this.m_goShotCtrlList.Length)
		{
			this.m_goShotCtrlList[this.m_nowIndex].SetActive(false);
		}
		if (toNext)
		{
			this.m_nowIndex = (int)Mathf.Repeat((float)this.m_nowIndex + 1f, (float)this.m_goShotCtrlList.Length);
		}
		else
		{
			this.m_nowIndex--;
			if (this.m_nowIndex < 0)
			{
				this.m_nowIndex = this.m_goShotCtrlList.Length - 1;
			}
		}
		if (0 <= this.m_nowIndex && this.m_nowIndex < this.m_goShotCtrlList.Length)
		{
			this.m_goShotCtrlList[this.m_nowIndex].SetActive(true);
			this.m_nowGoName = this.m_goShotCtrlList[this.m_nowIndex].name;
			this.m_shotNameText.text = "No." + (this.m_nowIndex + 1).ToString() + " : " + this.m_nowGoName;
			base.StartCoroutine(this.StartShot());
		}
	}

	private IEnumerator StartShot()
	{
		UbhShowcaseCtrl._StartShot_c__Iterator0 _StartShot_c__Iterator = new UbhShowcaseCtrl._StartShot_c__Iterator0();
		_StartShot_c__Iterator._this = this;
		return _StartShot_c__Iterator;
	}
}
