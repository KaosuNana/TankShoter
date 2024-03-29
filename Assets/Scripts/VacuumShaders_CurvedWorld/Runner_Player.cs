using System;
using UnityEngine;

namespace VacuumShaders.CurvedWorld
{
	[AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Player")]
	public class Runner_Player : MonoBehaviour
	{
		public enum SIDE
		{
			Left,
			Right
		}

		public static Runner_Player get;

		private Vector3 newPos;

		private Runner_Player.SIDE side;

		private Animation animationComp;

		public AnimationClip moveLeft;

		public AnimationClip moveRight;

		private void Awake()
		{
			Runner_Player.get = this;
		}

		private void Start()
		{
			this.side = Runner_Player.SIDE.Left;
			base.transform.position = new Vector3(-3.5f, 0f, 0f);
			this.newPos = base.transform.position;
			this.animationComp = base.GetComponent<Animation>();
			Physics.gravity = new Vector3(0f, -50f, 0f);
		}

		private void OnDisable()
		{
			Physics.gravity = new Vector3(0f, -9.8f, 0f);
		}

		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
			{
				if (this.side == Runner_Player.SIDE.Right)
				{
					this.newPos = new Vector3(-3.5f, 0f, 0f);
					this.side = Runner_Player.SIDE.Left;
					this.animationComp.Play(this.moveLeft.name);
				}
				else if (this.side == Runner_Player.SIDE.Left)
				{
					this.newPos = new Vector3(3.5f, 0f, 0f);
					this.side = Runner_Player.SIDE.Right;
					this.animationComp.Play(this.moveRight.name);
				}
			}
			base.transform.position = Vector3.Lerp(base.transform.position, this.newPos, Time.deltaTime * 10f);
		}

		private void OnCollisionEnter(Collision collision)
		{
			Vector3 force = (Vector3.forward + Vector3.up + UnityEngine.Random.insideUnitSphere).normalized * (float)UnityEngine.Random.Range(100, 150);
			collision.rigidbody.AddForce(force, ForceMode.Impulse);
			Runner_Car component = collision.gameObject.GetComponent<Runner_Car>();
			if (component != null)
			{
				component.speed = 1f;
			}
		}
	}
}
