using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

public class RocketController : MonoBehaviour
{
	public GameObject rocketParticle;

	public GameObject smokeParticle;

	public void RocketArrive()
	{
		this.smokeParticle.SetActive(false);
		base.transform.DOKill(false);
		base.transform.localPosition = new Vector3(0f, 1f, -10f);
		base.transform.localRotation = Quaternion.Euler(Vector3.zero);
		base.transform.DOLocalMoveZ(0f, 0.35f, false).SetEase(Ease.Linear).OnComplete(delegate
		{
			base.GetComponent<Collider>().enabled = true;
			this.rocketParticle.GetComponent<ParticleSystem>().Play();
			base.Invoke("TurnOnSmoke", 0.25f);
		});
	}

	public void TurnOnSmoke()
	{
		this.smokeParticle.SetActive(true);
		this.smokeParticle.GetComponent<ParticleSystem>().Play();
	}

	public void RocketFinish()
	{
		base.GetComponent<Collider>().enabled = false;
		Vector3[] path = new Vector3[]
		{
			new Vector3(0f, 1f, 10f),
			new Vector3(0f, 10f, 10f)
		};
		base.transform.DOLocalRotate(new Vector3(-90f, 0f, 0f), 0.5f, RotateMode.Fast);
		base.transform.DOLocalPath(path, 0.5f, PathType.CatmullRom, PathMode.Full3D, 10, null).OnComplete(delegate
		{
			base.transform.localPosition = new Vector3(0f, 1f, -10f);
			base.transform.localRotation = Quaternion.Euler(Vector3.zero);
			this.smokeParticle.GetComponent<ParticleSystem>().Stop();
			base.gameObject.SetActive(false);
		});
	}
}
