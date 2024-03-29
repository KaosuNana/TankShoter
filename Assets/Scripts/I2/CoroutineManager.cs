using System;
using System.Collections;
using UnityEngine;

namespace I2
{
	public class CoroutineManager : MonoBehaviour
	{
		private static CoroutineManager mInstance;

		public static Coroutine Start(IEnumerator coroutine)
		{
			if (CoroutineManager.mInstance == null)
			{
				GameObject gameObject = new GameObject("_Coroutiner");
				gameObject.hideFlags |= HideFlags.HideAndDontSave;
				CoroutineManager.mInstance = gameObject.AddComponent<CoroutineManager>();
				if (Application.isPlaying)
				{
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
				}
			}
			return CoroutineManager.mInstance.StartCoroutine(coroutine);
		}
	}
}
