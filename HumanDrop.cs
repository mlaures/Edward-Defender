using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanDrop : MonoBehaviour {

	private GameObject manager;
	private Target script;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("GameController");
		script = manager.GetComponent<Target> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y != -2 && transform.parent == null) {
			if (transform.position.y > -2) {
				transform.position += Vector3.down * Time.deltaTime;
			}
			if (transform.position.y <-2) {
				transform.position += Vector3.up * Time.deltaTime;
			}
		}
	}

}
