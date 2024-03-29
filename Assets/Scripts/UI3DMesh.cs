using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(CanvasRenderer))]
public class UI3DMesh : MonoBehaviour
{
	public Mesh TheMesh;

	public List<Material> Materials;

	private void ResetData()
	{
		CanvasRenderer component = base.GetComponent<CanvasRenderer>();
		component.SetMesh(this.TheMesh);
		component.materialCount = this.Materials.Count;
		for (int i = 0; i < this.Materials.Count; i++)
		{
			component.SetMaterial(this.Materials[i], i);
		}
	}

	private void OnEnable()
	{
		this.ResetData();
	}

	private void OnValidate()
	{
		this.ResetData();
	}
}
