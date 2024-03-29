using DG.Tweening;
using I2.Loc;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopPageManager : MonoBehaviour
{
	public RectTransform verticalHolder;

	public Text[] boltInAppCostsText;

	public Button[] boltInAppButton;

	public Text[] boltInAppRewardsText;

	public Text[] coinInAppCostsText;

	public Button[] coinInAppButton;

	public Text[] coinInAppRewardsText;

	public Button buyFreeContinueButton;

	public GameObject tickFreeContinue;

	public Text freeContinueCostText;

	public GameObject coinBurstParticle;

	public AudioClip coinBurstSFX;

	public GameObject coinObject;

	public GameObject boltBurstParticle;

	public GameObject boltObject;

	public CanvasGroup notEnoughCoins;

	public Text notEnoughCoinsText;

	public AudioClip countingUpSFX;

	public Text purchaseFailReasonText;

	public GameObject purchaseFailObj;

	public GameObject okFromFailButtonObj;

	public Image purchaseOverlay;

	private int[] coinAmount;

	private int[] boltAmount;

	private void Start()
	{
		this.coinAmount = new int[]
		{
			1000,
			4800,
			12000
		};
		this.boltAmount = new int[]
		{
			100,
			480,
			900,
			2000
		};
		this.coinInAppRewardsText[0].text = GameManager.CurrencyToString((float)this.coinAmount[0]);
		this.coinInAppRewardsText[1].text = GameManager.CurrencyToString((float)this.coinAmount[1]);
		this.coinInAppRewardsText[2].text = GameManager.CurrencyToString((float)this.coinAmount[2]);
		this.boltInAppRewardsText[0].text = GameManager.CurrencyToString((float)this.boltAmount[0]);
		this.boltInAppRewardsText[1].text = GameManager.CurrencyToString((float)this.boltAmount[1]);
		this.boltInAppRewardsText[2].text = GameManager.CurrencyToString((float)this.boltAmount[2]);
		this.boltInAppRewardsText[3].text = GameManager.CurrencyToString((float)this.boltAmount[3]);
		if (GameSingleton.IsiPhoneX)
		{
			this.verticalHolder.offsetMax = new Vector2(this.verticalHolder.offsetMax.x, this.verticalHolder.offsetMax.y - 80f);
		}
	}

	private void FetchPrice()
	{
		// this.coinInAppCostsText[0].text = Purchaser.GetPriceForProductID("com.kisekigames.tankbuddies.coins.tier1");
		// this.coinInAppCostsText[1].text = Purchaser.GetPriceForProductID("com.kisekigames.tankbuddies.coins.tier2");
		// this.coinInAppCostsText[2].text = Purchaser.GetPriceForProductID("com.kisekigames.tankbuddies.coins.tier3");
		// this.boltInAppCostsText[0].text = Purchaser.GetPriceForProductID("com.kisekigames.tankbuddies.bolts.tier1");
		// this.boltInAppCostsText[1].text = Purchaser.GetPriceForProductID("com.kisekigames.tankbuddies.bolts.tier2");
		// this.boltInAppCostsText[2].text = Purchaser.GetPriceForProductID("com.kisekigames.tankbuddies.bolts.tier3");
		// this.boltInAppCostsText[3].text = Purchaser.GetPriceForProductID("com.kisekigames.tankbuddies.bolts.tier4a");
		// this.freeContinueCostText.text = Purchaser.GetPriceForProductID("com.kisekigames.tankbuddies.freecontinue");
	}

	private void OnEnable()
	{
		this.FetchPrice();
		this.Reset();
		EventManager.StartListening("EventAndroidBackButton", new UnityAction(this.OnPressCloseButton));
	}

	private void OnDisable()
	{
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressCloseButton));
	}

	private void Reset()
	{
		this.coinObject.SetActive(false);
		this.coinBurstParticle.SetActive(false);
		this.UpdateButtons();
		this.purchaseOverlay.gameObject.SetActive(false);
	}

	private void UpdateButtons()
	{
		this.buyFreeContinueButton.gameObject.SetActive(!GameManager.HasPurchasedFreeContinue);
		this.tickFreeContinue.SetActive(GameManager.HasPurchasedFreeContinue);
	}

	public void PurchaseCoinSuccessful(string productID)
	{
		this.SetAllButtonsEnable(true);
		int num = 0;
		if (productID == "com.kisekigames.tankbuddies.coins.tier1")
		{
			num = this.coinAmount[0];
		}
		else if (productID == "com.kisekigames.tankbuddies.coins.tier2")
		{
			num = this.coinAmount[1];
		}
		else if (productID == "com.kisekigames.tankbuddies.coins.tier3")
		{
			num = this.coinAmount[2];
		}
		else if (productID == "com.kisekigames.tankbuddies.freecontinue")
		{
			GameManager.PurchaseFreeContinue();
		}
		if (num > 0)
		{
			this.coinBurstParticle.SetActive(true);
			SoundManager.PlaySFXInArray(this.coinBurstSFX, base.transform.position, 1f);
			this.coinObject.SetActive(true);
			this.coinObject.GetComponent<CoinUIAnimationController>().AnimateAmountWithScaleDown(num, true);
			GameManager.ExtraCoinExcludeThisGameCoin(num);
			EventManager.TriggerEvent("EventCoinChanges");
		}
		this.UpdateButtons();
	}

	public void PurchaseBoltSuccessful(string productID)
	{
		this.SetAllButtonsEnable(true);
		int num = 0;
		if (productID == "com.kisekigames.tankbuddies.bolts.tier1")
		{
			num = this.boltAmount[0];
		}
		else if (productID == "com.kisekigames.tankbuddies.bolts.tier2")
		{
			num = this.boltAmount[1];
		}
		else if (productID == "com.kisekigames.tankbuddies.bolts.tier3")
		{
			num = this.boltAmount[2];
		}
		else if (productID == "com.kisekigames.tankbuddies.bolts.tier4a")
		{
			num = this.boltAmount[3];
		}
		this.boltBurstParticle.SetActive(true);
		SoundManager.PlaySFXInArray(this.coinBurstSFX, base.transform.position, 1f);
		this.boltObject.SetActive(true);
		this.boltObject.GetComponent<CoinUIAnimationController>().AnimateAmountWithScaleDown(num, true);
		GameManager.ExtraBoltExcludeThisGameBolt(num);
		EventManager.TriggerEvent("EventBoltChanges");
		this.UpdateButtons();
	}

	public void PurchaseFail(string reason)
	{
		this.purchaseFailObj.SetActive(true);
		this.okFromFailButtonObj.SetActive(true);
		this.purchaseFailReasonText.text = reason;
	}

	public void OnPressOkButtonFromPurchaseFail()
	{
		SoundManager.PlayTapSFX();
		this.purchaseFailObj.SetActive(false);
		this.okFromFailButtonObj.SetActive(false);
		this.SetAllButtonsEnable(true);
	}

	private void SetAllButtonsEnable(bool input)
	{
		if (input)
		{
			this.UpdateButtons();
		}
		if (input)
		{
			this.purchaseOverlay.DOFade(0f, 0.1f).OnComplete(delegate
			{
				this.purchaseOverlay.gameObject.SetActive(false);
			});
		}
		else
		{
			Color color = this.purchaseOverlay.color;
			color.a = 0f;
			this.purchaseOverlay.color = color;
			this.purchaseOverlay.DOFade(0.4f, 0.1f).OnComplete(delegate
			{
				this.purchaseOverlay.gameObject.SetActive(true);
			});
		}
	}

	public void AnimateNotEnoughCoinsOrBolts(bool needMoreCoins, bool needMoreBolts)
	{
		if (needMoreBolts)
		{
			this.notEnoughCoinsText.text = ScriptLocalization.Get("Not enough bolts");
		}
		else if (needMoreCoins)
		{
			this.notEnoughCoinsText.text = ScriptLocalization.Get("Not enough coins");
		}
		this.notEnoughCoins.gameObject.SetActive(true);
		this.notEnoughCoins.alpha = 0f;
		this.notEnoughCoins.DOFade(1f, 0.2f).OnComplete(delegate
		{
			this.notEnoughCoins.DOFade(0f, 0.2f).SetDelay(1f);
		});
	}

	public void OnPressPurchaseFreeContinue()
	{
		SoundManager.PlayTapSFX();
		this.SetAllButtonsEnable(false);
		// Purchaser.BuyInAppWithCallBack("com.kisekigames.tankbuddies.freecontinue", new Action<string>(this.PurchaseCoinSuccessful), new Action<string>(this.PurchaseFail));
		PurchaseCoinSuccessful("com.kisekigames.tankbuddies.freecontinue");
	}

	public void OnPressBuyCoinButton(int tier)
	{
		SoundManager.PlayTapSFX();
		this.SetAllButtonsEnable(false);
		if (tier == 0)
		{
			// Purchaser.BuyInAppWithCallBack("com.kisekigames.tankbuddies.coins.tier1", new Action<string>(this.PurchaseCoinSuccessful), new Action<string>(this.PurchaseFail));
			PurchaseCoinSuccessful("com.kisekigames.tankbuddies.coins.tier1");
		}
		else if (tier == 1)
		{
			// Purchaser.BuyInAppWithCallBack("com.kisekigames.tankbuddies.coins.tier2", new Action<string>(this.PurchaseCoinSuccessful), new Action<string>(this.PurchaseFail));
			PurchaseCoinSuccessful("com.kisekigames.tankbuddies.coins.tier2");
		}
		else if (tier == 2)
		{
			// Purchaser.BuyInAppWithCallBack("com.kisekigames.tankbuddies.coins.tier3", new Action<string>(this.PurchaseCoinSuccessful), new Action<string>(this.PurchaseFail));
			PurchaseCoinSuccessful("com.kisekigames.tankbuddies.coins.tier3");
		}
	}

	public void OnPressBuyBoltButton(int tier)
	{
		SoundManager.PlayTapSFX();
		this.SetAllButtonsEnable(false);
		if (tier == 0)
		{
			// Purchaser.BuyInAppWithCallBack("com.kisekigames.tankbuddies.bolts.tier1", new Action<string>(this.PurchaseBoltSuccessful), new Action<string>(this.PurchaseFail));
			PurchaseBoltSuccessful("com.kisekigames.tankbuddies.bolts.tier1");
		}
		else if (tier == 1)
		{
			// Purchaser.BuyInAppWithCallBack("com.kisekigames.tankbuddies.bolts.tier2", new Action<string>(this.PurchaseBoltSuccessful), new Action<string>(this.PurchaseFail));
			PurchaseBoltSuccessful("com.kisekigames.tankbuddies.bolts.tier2");
		}
		else if (tier == 2)
		{
			// Purchaser.BuyInAppWithCallBack("com.kisekigames.tankbuddies.bolts.tier3", new Action<string>(this.PurchaseBoltSuccessful), new Action<string>(this.PurchaseFail));
			PurchaseBoltSuccessful("com.kisekigames.tankbuddies.bolts.tier3");
		}
		else if (tier == 3)
		{
			// Purchaser.BuyInAppWithCallBack("com.kisekigames.tankbuddies.bolts.tier4a", new Action<string>(this.PurchaseBoltSuccessful), new Action<string>(this.PurchaseFail));
			PurchaseBoltSuccessful("com.kisekigames.tankbuddies.bolts.tier4a");
		}
	}

	public void OnPressCloseButton()
	{
		SoundManager.PlayCancelSFX();
		GameManager.RestartGame();
		EventManager.TriggerEvent("EventResultListenToAndroidBackButton");
	}
}
