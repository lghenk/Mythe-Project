using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary author="Timo Heijne">
/// This scales a plane to the camera's size, depending on the distance (makes it available for all screens). Used for the videoplayer
/// </summary>
public class PlaneToCameraScale : MonoBehaviour {
	void Start () {
		float distance = Vector3.Distance(Camera.main.transform.position, gameObject.transform.position);
		float height =  2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad) * distance;
		float width = height * Screen.width / Screen.height;
		gameObject.transform.localScale = new Vector3(width / 10f, 1.0f, height / 10f);
	}
}
