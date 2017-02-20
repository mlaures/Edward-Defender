using UnityEngine;
using System.Collections;

public class VampireShooter : MonoBehaviour {
	public GameObject magic;
	private float timeFromLastShot = 0;
	public float rateOfFire;
	private int direction;
	private Transform player;
	private float distance;
	public float range;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	
	}
	
	// Update is called once per frame
	void Update () {
		timeFromLastShot += Time.deltaTime;
		distance = Vector3.Distance (transform.position, player.position);
		if (distance <= 10) {
			
			if (Mathf.Round (timeFromLastShot) == rateOfFire) {
				fire ();
				timeFromLastShot = 0;
			}
		}
	}

		

	
	void fire () {
		//Instatiate buller magic thing Attribute rotation towards the player
		Instantiate (magic, transform.position, Quaternion.identity);
	}
}
