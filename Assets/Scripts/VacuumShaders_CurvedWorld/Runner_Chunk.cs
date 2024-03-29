using System;
using UnityEngine;

namespace VacuumShaders.CurvedWorld
{
	[AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Move Element")]
	public class Runner_Chunk : MonoBehaviour
	{
		private void Update()
		{
			base.transform.Translate(Runner_SceneManager.moveVector * Runner_SceneManager.get.speed * Time.deltaTime);
		}

		private void FixedUpdate()
		{
			if (base.transform.position.z < -100f)
			{
				Runner_SceneManager.get.DestroyChunk(this);
			}
		}
	}
}
