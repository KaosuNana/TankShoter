using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
	private Dictionary<string, UnityEvent> eventDictionary;

	private Dictionary<string, PowerUpEvent> powerUpEventDictionary;

	private static EventManager eventManager;

	public static EventManager instance
	{
		get
		{
			if (!EventManager.eventManager)
			{
				EventManager.eventManager = (UnityEngine.Object.FindObjectOfType(typeof(EventManager)) as EventManager);
				if (!EventManager.eventManager)
				{
					UnityEngine.Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
				}
				else
				{
					EventManager.eventManager.Init();
				}
			}
			return EventManager.eventManager;
		}
	}

	private void Init()
	{
		if (this.eventDictionary == null)
		{
			this.eventDictionary = new Dictionary<string, UnityEvent>();
		}
		if (this.powerUpEventDictionary == null)
		{
			this.powerUpEventDictionary = new Dictionary<string, PowerUpEvent>();
		}
	}

	public static void StartListeningEventPowerUp(string eventName, UnityAction<PowerUp.PowerUpType> listener)
	{
		PowerUpEvent powerUpEvent = null;
		if (EventManager.instance.powerUpEventDictionary.TryGetValue(eventName, out powerUpEvent))
		{
			powerUpEvent.AddListener(listener);
		}
		else
		{
			powerUpEvent = new PowerUpEvent();
			powerUpEvent.AddListener(listener);
			EventManager.instance.powerUpEventDictionary.Add(eventName, powerUpEvent);
		}
	}

	public static void StopListeningEventPowerUp(string eventName, UnityAction<PowerUp.PowerUpType> listener)
	{
		if (EventManager.eventManager == null)
		{
			return;
		}
		PowerUpEvent powerUpEvent = null;
		if (EventManager.instance.powerUpEventDictionary.TryGetValue(eventName, out powerUpEvent))
		{
			powerUpEvent.RemoveListener(listener);
		}
	}

	public static void TriggerEventPowerUp(string eventName, PowerUp.PowerUpType powerUpType)
	{
		PowerUpEvent powerUpEvent = null;
		if (EventManager.instance.powerUpEventDictionary.TryGetValue(eventName, out powerUpEvent))
		{
			powerUpEvent.Invoke(powerUpType);
		}
	}

	public static void StartListening(string eventName, UnityAction listener)
	{
		UnityEvent unityEvent = null;
		if (EventManager.instance.eventDictionary.TryGetValue(eventName, out unityEvent))
		{
			unityEvent.AddListener(listener);
		}
		else
		{
			unityEvent = new UnityEvent();
			unityEvent.AddListener(listener);
			EventManager.instance.eventDictionary.Add(eventName, unityEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction listener)
	{
		if (EventManager.eventManager == null)
		{
			return;
		}
		UnityEvent unityEvent = null;
		if (EventManager.instance.eventDictionary.TryGetValue(eventName, out unityEvent))
		{
			unityEvent.RemoveListener(listener);
		}
	}

	public static void TriggerEvent(string eventName)
	{
		UnityEvent unityEvent = null;
		if (EventManager.instance.eventDictionary.TryGetValue(eventName, out unityEvent))
		{
			unityEvent.Invoke();
		}
	}
}
