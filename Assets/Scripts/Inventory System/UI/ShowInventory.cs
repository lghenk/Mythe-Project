using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInventory : MonoBehaviour {
	private GameObject _childPanel;
	
	private void Start() {
		_childPanel = transform.GetChild(0).gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			_childPanel.SetActive(!_childPanel.gameObject.active);
		}
	}
}
