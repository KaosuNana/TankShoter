using DG.Tweening;
using I2.Loc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PrestigeScreenController : MonoBehaviour
{
	private sealed class _PrestigeSequence_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal PrestigeScreenController _this;

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

		public _PrestigeSequence_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				UnityEngine.Object.Instantiate<GameObject>(this._this.boltBurstParticle);
				SoundManager.PlaySFXInArray(this._this.coinBurstSFX, this._this.transform.position, 1f);
				GameManager.ExtraBolt(this._this.rewardBolt);
				GameManager.ResetCoin();
				EventManager.TriggerEvent("EventCoinChanges");
				EventManager.TriggerEvent("EventBoltChanges");
				this._current = new WaitForSeconds(3f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				GameManager.PlayerPrestige();
				this._PC = -1;
				break;
			}
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

	public ParticleSystem prestigeParticle;

	public Text prestigeInfo;

	public Text currentLevelText;

	public int baseRewardBolt;

	private int rewardBolt;

	public int levelRequirement;

	public Text rewardBoltText;

	public Text prestigeRequirementText;

	public GameObject boltBurstParticle;

	public AudioClip coinBurstSFX;

	public AudioClip prestigingSFX;

	private bool isPrestiging;

	private void Awake()
	{
		this.rewardBolt = this.baseRewardBolt * (GameManager.NumberOfPrestige + 1);
		this.rewardBolt += (int)((float)Mathf.Max(GameManager.CurrentLevel - this.levelRequirement, 0) * 0.1f * (float)this.rewardBolt);
		this.rewardBoltText.text = GameManager.CurrencyToString((float)this.rewardBolt);
		this.prestigeInfo.text = string.Format(ScriptLocalization.Get("You have prestiged {0} times."), GameManager.NumberOfPrestige);
		this.prestigeRequirementText.text = string.Format(ScriptLocalization.Get("LEVEL {0} is required to Prestige"), this.levelRequirement);
		this.currentLevelText.text = ScriptLocalization.Get("LEVEL").ToUpper() + " " + GameManager.CurrentLevel.ToString();
		if (GameManager.CurrentLevel > this.levelRequirement)
		{
			this.prestigeRequirementText.gameObject.SetActive(false);
		}
	}

	public void OnPressOkToPrestige()
	{
		SoundManager.PlayTapSFX();
		if (this.isPrestiging)
		{
			return;
		}
		if (GameManager.CurrentLevel >= this.levelRequirement)
		{
			this.prestigeParticle.Play();
			SoundManager.PlaySFXInArray(this.prestigingSFX, base.transform.position, 1f);
			this.isPrestiging = true;
			base.StartCoroutine("PrestigeSequence");
		}
		else
		{
			this.prestigeRequirementText.transform.DOKill(false);
			this.prestigeRequirementText.transform.DOScale(Vector3.one * 1.2f, 0.1f).OnComplete(delegate
			{
				this.prestigeRequirementText.transform.DOShakeRotation(1f, new Vector3(0f, 0f, 30f), 10, 10f, true);
				this.prestigeRequirementText.transform.DOScale(Vector3.one, 0.2f).SetDelay(1f);
			});
		}
	}

	private IEnumerator PrestigeSequence()
	{
		PrestigeScreenController._PrestigeSequence_c__Iterator0 _PrestigeSequence_c__Iterator = new PrestigeScreenController._PrestigeSequence_c__Iterator0();
		_PrestigeSequence_c__Iterator._this = this;
		return _PrestigeSequence_c__Iterator;
	}

	public void OnPressNotToPrestige()
	{
		SoundManager.PlayCancelSFX();
		GameManager.ShowVehiclePage();
	}

	private void OnEnable()
	{
		EventManager.StartListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButtonAndroid));
	}

	private void OnDisable()
	{
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButtonAndroid));
	}

	private void OnPressBackButtonAndroid()
	{
		this.OnPressNotToPrestige();
	}
}
