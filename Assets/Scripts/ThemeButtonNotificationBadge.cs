using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ThemeButtonNotificationBadge : MonoBehaviour
{
	public GameObject notificationObj;

	public Text notificationText;

	private void Start()
	{
		this.UpdateThemeButtonNotif();
	}

	private void OnEnable()
	{
		EventManager.StartListening("EventNewAreaUnlocked", new UnityAction(this.UpdateThemeButtonNotif));
	}

	private void OnDisable()
	{
		EventManager.StopListening("EventNewAreaUnlocked", new UnityAction(this.UpdateThemeButtonNotif));
	}

	public void UpdateThemeButtonNotif()
	{
	}
}
