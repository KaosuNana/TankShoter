using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PillarController : MonoBehaviour
{
	private sealed class _GoDown_c__AnonStorey0
	{
		internal Pillar firstPillarToGoDown;

		internal void __m__0()
		{
			this.firstPillarToGoDown.gameObject.SetActive(false);
		}
	}

	public List<Pillar> pillarList;

	public List<Material> pillarMaterialList;

	private static TweenCallback __f__am_cache0;

	private void Start()
	{
		this.InitialisePillars();
		this.RandomisePillar();
		this.AnimatePillarUp();
	}

	private void InitialisePillars()
	{
		for (int i = 0; i < this.pillarList.Count; i++)
		{
			this.pillarList[i].SetPillarController(this);
			this.pillarList[i].transform.localPosition = new Vector3(this.pillarList[i].transform.position.x, -5.5f, 0f);
		}
	}

	private void RandomisePillar()
	{
		List<int> list = new List<int>
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6
		};
		int num = UnityEngine.Random.Range(4, 7);
		for (int i = 0; i < 7; i++)
		{
			int num2 = 3;
			if (i > num - 1)
			{
				num2 = UnityEngine.Random.Range(0, 3);
			}
			int index = UnityEngine.Random.Range(0, list.Count);
			this.pillarList[list[index]].SetPillarType((Pillar.PillarType)num2, this.pillarMaterialList[num2]);
			list.RemoveAt(index);
		}
	}

	private void AnimatePillarUp()
	{
		float num = 0.05f;
		for (int i = 0; i < this.pillarList.Count; i++)
		{
			this.pillarList[i].transform.DOLocalMoveY(0f, 0.25f, false).SetDelay(num * (float)(i + 1));
		}
	}

	public void GoDown(Pillar firstPillarToGoDown)
	{
		float num = 0.1f;
		firstPillarToGoDown.transform.DOLocalMoveY(-5.5f, 0.25f, false).OnComplete(delegate
		{
			firstPillarToGoDown.gameObject.SetActive(false);
		});
		for (int i = 0; i < this.pillarList.Count; i++)
		{
			this.pillarList[i].transform.DOLocalMoveY(-1.5f, 0.25f, false).SetDelay(num * (float)(i + 1)).OnComplete(delegate
			{
			});
		}
	}
}
