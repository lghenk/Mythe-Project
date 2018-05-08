using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour {

	[SerializeField] private Vector3 _rotationSpeed;

	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + _rotationSpeed * Time.deltaTime);
	}
}
