using System;
using UnityEngine;

namespace VacuumShaders.CurvedWorld
{
	[AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Car")]
	public class Runner_Car : MonoBehaviour
	{
		private Rigidbody rigidBody;

		public float speed = 1f;

		private void Start()
		{
			this.rigidBody = base.GetComponent<Rigidbody>();
			base.transform.position = new Vector3(UnityEngine.Random.Range(-3.5f, 3.5f), 1f, (float)UnityEngine.Random.Range(140, 240));
			this.speed = UnityEngine.Random.Range(2f, 6f);
		}

		private void FixedUpdate()
		{
			this.rigidBody.MovePosition(base.transform.position + Runner_SceneManager.moveVector * Runner_SceneManager.get.speed * Time.deltaTime * this.speed);
			if (base.transform.position.y < -10f)
			{
				Runner_SceneManager.get.DestroyCar(this);
			}
		}
	}
}
