using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VacuumShaders.CurvedWorld
{
	[AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Scene Manager")]
	public class Runner_SceneManager : MonoBehaviour
	{
		public static Runner_SceneManager get;

		public float speed = 1f;

		public GameObject[] chunks;

		public GameObject[] cars;

		public static float chunkSize = 60f;

		public static Vector3 moveVector = new Vector3(0f, 0f, -1f);

		public static GameObject lastChunk;

		private List<Material> listMaterials;

		private void Awake()
		{
			Runner_SceneManager.get = this;
			for (int i = 0; i < this.chunks.Length; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.chunks[i]);
				gameObject.transform.position = new Vector3(0f, 0f, (float)i * Runner_SceneManager.chunkSize);
				Runner_SceneManager.lastChunk = gameObject;
			}
			for (int j = 0; j < this.cars.Length; j++)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.cars[j]);
			}
		}

		private void Start()
		{
			Renderer[] array = UnityEngine.Object.FindObjectsOfType(typeof(Renderer)) as Renderer[];
			this.listMaterials = new List<Material>();
			Renderer[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Renderer renderer = array2[i];
				this.listMaterials.AddRange(renderer.sharedMaterials);
			}
			this.listMaterials = this.listMaterials.Distinct<Material>().ToList<Material>();
		}

		public void DestroyChunk(Runner_Chunk moveElement)
		{
			Vector3 position = Runner_SceneManager.lastChunk.transform.position;
			position.z += Runner_SceneManager.chunkSize;
			Runner_SceneManager.lastChunk = moveElement.gameObject;
			Runner_SceneManager.lastChunk.transform.position = position;
		}

		public void DestroyCar(Runner_Car car)
		{
			UnityEngine.Object.Destroy(car.gameObject);
			UnityEngine.Object.Instantiate<GameObject>(this.cars[UnityEngine.Random.Range(0, this.cars.Length)]);
		}
	}
}
