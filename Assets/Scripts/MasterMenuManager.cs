using System;
using UnityEngine;
using UnityEngine.UI;

public class MasterMenuManager : MonoBehaviour
{
	public enum Page
	{
		VehiclePage,
		PowerUpPage,
		ShopPage,
		SettingPage,
		PrestigePage,
		IdleIncomePage
	}

	public GameObject[] pages;

	public Toggle[] toggles;

	public GameObject toggleObj;

	private void Start()
	{
		if (GameSingleton.IsiPhoneX)
		{
			base.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;
			Vector2 anchoredPosition = this.toggleObj.GetComponent<RectTransform>().anchoredPosition;
			anchoredPosition.y = 60f;
			this.toggleObj.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
		}
		else
		{
			base.GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;
		}
	}

	public void ShowPage(MasterMenuManager.Page page)
	{
		if (page == MasterMenuManager.Page.IdleIncomePage)
		{
			this.toggleObj.GetComponent<ToggleGroup>().allowSwitchOff = true;
			for (int i = 0; i < this.toggles.Length; i++)
			{
				this.toggles[i].isOn = false;
			}
			this.toggleObj.SetActive(false);
			for (int j = 0; j < this.pages.Length; j++)
			{
				this.pages[j].SetActive(j == 5);
			}
			return;
		}
		this.toggleObj.GetComponent<ToggleGroup>().allowSwitchOff = false;
		this.toggleObj.SetActive(true);
		for (int k = 0; k < this.pages.Length; k++)
		{
			bool active = page == (MasterMenuManager.Page)k;
			if (k < this.toggles.Length)
			{
				this.toggles[k].isOn = (page == (MasterMenuManager.Page)k);
			}
			this.pages[k].SetActive(active);
		}
	}

	public void HidePages()
	{
		for (int i = 0; i < this.pages.Length; i++)
		{
			this.pages[i].SetActive(false);
		}
	}

	public void OnPressTabButton(int index)
	{
		SoundManager.PlayTapSFX();
		if (index == 4)
		{
			if (this.pages[0].activeSelf)
			{
				this.pages[0].GetComponent<VehiclePageController>().RestartGame();
			}
			else
			{
				GameManager.RestartGame();
			}
		}
		else
		{
			for (int i = 0; i < this.pages.Length; i++)
			{
				if (i == index)
				{
					this.pages[i].SetActive(true);
				}
				else
				{
					this.pages[i].SetActive(false);
				}
			}
		}
	}
}
