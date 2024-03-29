// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Runtime.CompilerServices;
// using UnityEngine;
// // using UnityEngine.Purchasing;
// // using UnityEngine.Purchasing.Extension;
// // using UnityEngine.Purchasing.Security;
//
// public class Purchaser : MonoBehaviour/*, IStoreListener*/
// {
// 	public delegate void CallBackFunction();
//
// 	private sealed class _StartActivityIndicator_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
// 	{
// 		internal object _current;
//
// 		internal bool _disposing;
//
// 		internal int _PC;
//
// 		object IEnumerator<object>.Current
// 		{
// 			get
// 			{
// 				return this._current;
// 			}
// 		}
//
// 		object IEnumerator.Current
// 		{
// 			get
// 			{
// 				return this._current;
// 			}
// 		}
//
// 		public _StartActivityIndicator_c__Iterator0()
// 		{
// 		}
//
// 		public bool MoveNext()
// 		{
// 			uint num = (uint)this._PC;
// 			this._PC = -1;
// 			switch (num)
// 			{
// 			case 0u:
// 				this._current = new WaitForSeconds(3f);
// 				if (!this._disposing)
// 				{
// 					this._PC = 1;
// 				}
// 				return true;
// 			case 1u:
// 				this._PC = -1;
// 				break;
// 			}
// 			return false;
// 		}
//
// 		public void Dispose()
// 		{
// 			this._disposing = true;
// 			this._PC = -1;
// 		}
//
// 		public void Reset()
// 		{
// 			throw new NotSupportedException();
// 		}
// 	}
//
// 	private static Purchaser instance;
//
// 	// private static IStoreController m_StoreController;
// 	//
// 	// private static IExtensionProvider m_StoreExtensionProvider;
//
// 	private bool isRestoring;
//
// 	public string[] inAppProductID;
//
// 	public string[] coinsProductID;
//
// 	public string[] boltsProductID;
//
// 	protected Action<string> callbackAction;
//
// 	protected Action<string> failCallbackAction;
//
// 	protected Purchaser.CallBackFunction callbackFct;
//
// 	protected Purchaser.CallBackFunction failCallbackFct;
//
// 	private static Action<bool> __f__am_cache0;
//
// 	private void Awake()
// 	{
// 		if (Purchaser.instance != null)
// 		{
// 			UnityEngine.Object.Destroy(base.gameObject);
// 			return;
// 		}
// 		UnityEngine.Object.DontDestroyOnLoad(this);
// 		Purchaser.instance = this;
// 		// if (Purchaser.m_StoreController == null)
// 		// {
// 		// 	this.InitializePurchasing();
// 		// }
// 	}
//
// 	public void InitializePurchasing()
// 	{
// 		// if (this.IsInitialized())
// 		// {
// 		// 	return;
// 		// }
// 		// ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
// 		// for (int i = 0; i < this.inAppProductID.Length; i++)
// 		// {
// 		// 	configurationBuilder.AddProduct(this.inAppProductID[i], ProductType.NonConsumable);
// 		// }
// 		// for (int j = 0; j < this.coinsProductID.Length; j++)
// 		// {
// 		// 	configurationBuilder.AddProduct(this.coinsProductID[j], ProductType.Consumable);
// 		// }
// 		// for (int k = 0; k < this.boltsProductID.Length; k++)
// 		// {
// 		// 	configurationBuilder.AddProduct(this.boltsProductID[k], ProductType.Consumable);
// 		// }
// 		// UnityPurchasing.Initialize(this, configurationBuilder);
// 	}
//
// 	// private bool IsInitialized()
// 	// {
// 	// 	// return Purchaser.m_StoreController != null && Purchaser.m_StoreExtensionProvider != null;
// 	// }
//
// 	private IEnumerator StartActivityIndicator()
// 	{
// 		return new Purchaser._StartActivityIndicator_c__Iterator0();
// 	}
//
// 	private void StopActivityIndicator()
// 	{
// 		Handheld.StopActivityIndicator();
// 	}
//
// 	public static void BuyInAppWithCallBack(string productId, Action<string> inputCallback, Action<string> inputFailCallback)
// 	{
// 		Purchaser.instance.callbackAction = inputCallback;
// 		Purchaser.instance.failCallbackAction = inputFailCallback;
// 		Purchaser.instance.BuyProductID(productId);
// 	}
//
// 	public static void BuyInAppProductIDWithCallBack(string productId, Purchaser.CallBackFunction myCallBack, Purchaser.CallBackFunction failCallBack)
// 	{
// 		Purchaser.instance.callbackFct = myCallBack;
// 		Purchaser.instance.failCallbackFct = failCallBack;
// 		Purchaser.instance.BuyProductID(productId);
// 	}
//
// 	public void BuyInAppProductID(string productId)
// 	{
// 		this.isRestoring = false;
// 		this.BuyProductID(productId);
// 	}
//
// 	private void BuyProductID(string productId)
// 	{
// 		/*if (this.IsInitialized())
// 		{
// 			Product product = Purchaser.m_StoreController.products.WithID(productId);
// 			if (product != null && product.availableToPurchase)
// 			{
// 				Purchaser.m_StoreController.InitiatePurchase(product);
// 			}
// 			else if (this.failCallbackFct != null)
// 			{
// 				this.failCallbackFct();
// 				this.ResetCallBack();
// 			}
// 			else if (this.failCallbackAction != null)
// 			{
// 				this.failCallbackAction("Product is not ready. Please try again later");
// 				this.ResetCallBack();
// 			}
// 		}
// 		else */if (this.failCallbackFct != null)
// 		{
// 			this.failCallbackFct();
// 			this.ResetCallBack();
// 		}
// 		else if (this.failCallbackAction != null)
// 		{
// 			this.failCallbackAction("Purchaser is not initialised. Please try again later");
// 			this.ResetCallBack();
// 		}
// 	}
//
// 	public static void RestorePurchases()
// 	{
// 		SoundManager.PlayTapSFX();
// 		Purchaser.instance.isRestoring = true;
// 		if (!Purchaser.instance.IsInitialized())
// 		{
// 			if (Purchaser.instance.failCallbackFct != null)
// 			{
// 				Purchaser.instance.failCallbackFct();
// 				Purchaser.instance.ResetCallBack();
// 			}
// 			else if (Purchaser.instance.failCallbackAction != null)
// 			{
// 				Purchaser.instance.failCallbackAction("Purchaser is not initialised. Please try again later");
// 				Purchaser.instance.ResetCallBack();
// 			}
// 			return;
// 		}
// 		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
// 		{
// 			// IAppleExtensions extension = Purchaser.m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
// 			// extension.RestoreTransactions(delegate(bool result)
// 			// {
// 			// });
// 		}
// 		else if (Purchaser.instance.failCallbackFct != null)
// 		{
// 			Purchaser.instance.failCallbackFct();
// 			Purchaser.instance.ResetCallBack();
// 		}
// 		else if (Purchaser.instance.failCallbackAction != null)
// 		{
// 			Purchaser.instance.failCallbackAction(string.Empty);
// 			Purchaser.instance.ResetCallBack();
// 		}
// 	}
//
// 	// public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
// 	// {
// 	// 	Purchaser.m_StoreController = controller;
// 	// 	Purchaser.m_StoreExtensionProvider = extensions;
// 	// }
//
// 	// public void OnInitializeFailed(InitializationFailureReason error)
// 	// {
// 	// }
//
// 	private void ResetCallBack()
// 	{
// 		this.callbackFct = null;
// 		this.failCallbackFct = null;
// 		this.callbackAction = null;
// 		this.failCallbackAction = null;
// 	}
//
// 	// public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
// 	// {
// 	// 	bool flag = true;
// 	// 	bool flag2 = false;
// 	// 	CrossPlatformValidator crossPlatformValidator=null /*= new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier)*/;
// 	// 	try
// 	// 	{
// 	// 		IPurchaseReceipt[] array = crossPlatformValidator.Validate(args.purchasedProduct.receipt);
// 	// 		IPurchaseReceipt[] array2 = array;
// 	// 		for (int i = 0; i < array2.Length; i++)
// 	// 		{
// 	// 			IPurchaseReceipt purchaseReceipt = array2[i];
// 	// 			for (int j = 0; j < this.coinsProductID.Length; j++)
// 	// 			{
// 	// 				if (purchaseReceipt.productID == this.coinsProductID[j])
// 	// 				{
// 	// 					flag2 = true;
// 	// 					break;
// 	// 				}
// 	// 			}
// 	// 			if (!flag2)
// 	// 			{
// 	// 				for (int k = 0; k < this.boltsProductID.Length; k++)
// 	// 				{
// 	// 					if (purchaseReceipt.productID == this.boltsProductID[k])
// 	// 					{
// 	// 						flag2 = true;
// 	// 						break;
// 	// 					}
// 	// 				}
// 	// 			}
// 	// 			if (!flag2)
// 	// 			{
// 	// 				for (int l = 0; l < this.inAppProductID.Length; l++)
// 	// 				{
// 	// 					if (purchaseReceipt.productID == this.inAppProductID[l])
// 	// 					{
// 	// 						flag2 = true;
// 	// 						break;
// 	// 					}
// 	// 				}
// 	// 			}
// 	// 			flag = flag2;
// 	// 		}
// 	// 	}
// 	// 	catch (IAPSecurityException)
// 	// 	{
// 	// 		flag = false;
// 	// 	}
// 	// 	if (flag)
// 	// 	{
// 	// 		if (this.callbackAction != null)
// 	// 		{
// 	// 			this.callbackAction(args.purchasedProduct.definition.id);
// 	// 			this.ResetCallBack();
// 	// 		}
// 	// 		else if (this.callbackFct != null)
// 	// 		{
// 	// 			this.callbackFct();
// 	// 			this.ResetCallBack();
// 	// 		}
// 	// 		else if (string.Equals(args.purchasedProduct.definition.id, "com.kisekigames.tankbuddies.freecontinue", StringComparison.Ordinal))
// 	// 		{
// 	// 			GameManager.PurchaseFreeContinue();
// 	// 		}
// 	// 	}
// 	// 	else if (this.failCallbackFct != null)
// 	// 	{
// 	// 		this.failCallbackFct();
// 	// 		this.ResetCallBack();
// 	// 	}
// 	// 	else if (this.failCallbackAction != null)
// 	// 	{
// 	// 		this.failCallbackAction("Invalid Purchase. Please try again");
// 	// 		this.ResetCallBack();
// 	// 	}
// 	// 	return PurchaseProcessingResult.Complete;
// 	// }
//
// 	private void OnApplicationFocus(bool hasFocus)
// 	{
// 		Purchaser.instance = this;
// 	}
//
// 	// public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
// 	// {
// 	// 	if (this.failCallbackFct != null)
// 	// 	{
// 	// 		this.failCallbackFct();
// 	// 		this.ResetCallBack();
// 	// 	}
// 	// 	else if (this.failCallbackAction != null)
// 	// 	{
// 	// 		this.failCallbackAction(failureReason.ToString());
// 	// 		this.ResetCallBack();
// 	// 	}
// 	// 	this.StopActivityIndicator();
// 	// }
//
// 	// public static string GetPriceForProductID(string productID)
// 	// {
// 	// 	if (Purchaser.m_StoreController == null)
// 	// 	{
// 	// 		Purchaser.instance.InitializePurchasing();
// 	// 		return string.Empty;
// 	// 	}
// 	// 	return Purchaser.m_StoreController.products.WithID(productID).metadata.localizedPriceString;
// 	// }
// }
