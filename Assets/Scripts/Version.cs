using I2.Loc;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Version : MonoBehaviour
{
	private void Start()
	{
		this.OnChangeLanguage();
	}

	private void Awake()
	{
		LocalizationManager.OnLocalizeEvent += new LocalizationManager.OnLocalizeCallback(this.OnChangeLanguage);
	}

	private void OnDestroy()
	{
		LocalizationManager.OnLocalizeEvent -= new LocalizationManager.OnLocalizeCallback(this.OnChangeLanguage);
	}

	private void OnDisable()
	{
		LocalizationManager.OnLocalizeEvent -= new LocalizationManager.OnLocalizeCallback(this.OnChangeLanguage);
	}

	private void OnChangeLanguage()
	{
		base.GetComponent<Text>().text = ScriptLocalization.Get("Tank Buddies") + " v" + Application.version;
	}
}
