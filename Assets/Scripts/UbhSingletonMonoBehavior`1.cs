using System;
using UnityEngine;

public class UbhSingletonMonoBehavior<T> : UbhMonoBehaviour where T : UbhMonoBehaviour
{
	private static T m_instance;

	public static T instance
	{
		get
		{
			if (UbhSingletonMonoBehavior<T>.m_instance == null)
			{
				UbhSingletonMonoBehavior<T>.m_instance = UnityEngine.Object.FindObjectOfType<T>();
				if (UbhSingletonMonoBehavior<T>.m_instance == null)
				{
					UnityEngine.Debug.Log("Created " + typeof(T).Name);
					UbhSingletonMonoBehavior<T>.m_instance = new GameObject(typeof(T).Name).AddComponent<T>();
				}
			}
			return UbhSingletonMonoBehavior<T>.m_instance;
		}
	}

	protected virtual void Awake()
	{
		if (this != UbhSingletonMonoBehavior<T>.instance)
		{
			GameObject gameObject = base.gameObject;
			UnityEngine.Object.Destroy(this);
			UnityEngine.Object.Destroy(gameObject);
			return;
		}
	}
}
