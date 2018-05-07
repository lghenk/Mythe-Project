using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GodRays : MonoBehaviour
{
	public float speed = 0.2f;
	private Material mat;
	
	void Awake () 
	{
		mat = new Material(Shader.Find("Custom/Light rays"));
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		mat.SetFloat("_speed", speed);
		Graphics.Blit(src, dest, mat);
	}
}
