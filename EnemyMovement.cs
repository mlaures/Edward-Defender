using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour {

	private GameObject manager;
	private Transform get;
	private Transform away;
	private Target script;
	private int num;
	public float movespeed;
	private GameObject toDestroy;
	private bool isCaptured = false;
	public AudioSource splat;
	public AudioSource male; 
	public GameObject head;
	public AudioSource poof;
	public bool dead = false;
	public Animator die;
	public bool child = false;

	// Use this for initialization
	void Start () {
		//use the game manager's list
		manager = GameObject.FindGameObjectWithTag ("GameController");
		away = GameObject.FindGameObjectWithTag ("Mothership").GetComponent<Transform>();
		script = manager.GetComponent<Target> ();
		//choose a random human
		num = (Random.Range (0, script.targetList.Count));
		die.SetBool ("dead", false);

	}
	
	// Update is called once per frame
	void Update () {
		if(script.targetList.Count == 0){
			GetComponent<IdleBob>().enabled = true;
		}
		//check if a human has been reached
		if (isCaptured == true) {
			//carry human away
			if (transform.position != away.position) {
				if (transform.position.y < away.position.y) {
					transform.position += Vector3.up * Time.deltaTime;
				} else {
					transform.position = Vector3.MoveTowards (transform.position, away.position, movespeed * Time.deltaTime);
				}
			}
		} else {
			//behavior if no more humans
			if (script.targetList.Count == 0) {
				Debug.Log ("you failed");
			}
			//move to human
			if (script.targetList.Count > 0) {
				goTo ();
			}
		}
	}

	//what to do if in contact with another object
	void OnTriggerEnter2D(Collider2D other) {
		//if the object is a stake
		if (other.gameObject.tag == "Stake" && dead == false) {
			if (child == true) {
				transform.DetachChildren();
				script.targetList.Add (toDestroy);
			}
			StartCoroutine(death());
		}
		//if the object is a human, remove from human kidnap list, make a child, and go to captured behavior (update)
		if (other.gameObject.tag == "Human") {
			male.Play ();
			script.targetList.Remove (toDestroy);
			toDestroy.transform.parent = this.transform;
			child = true;
			isCaptured = true;
		}
		if (other.gameObject.tag == "Mothership") {
			//finish off the human and pick a new one
			Destroy (toDestroy);
			num = (Random.Range (0, script.targetList.Count));
			isCaptured = false;
			Instantiate (head, transform.position, Quaternion.identity);
			poof.Play();
		}
	}

	//movement behavior
	void goTo () {
		//specify the human thats is targeted
		try {
			get = script.targetList [num].GetComponent<Transform> ();
			toDestroy = script.targetList [num];
		}
		catch (MissingReferenceException e) {
			script.targetList.RemoveAt (num);
		}
		//move horizontally
		if (get.position.x < transform.position.x) {
			transform.position += Vector3.left * Time.deltaTime;
		} 
		if (get.position.x > transform.position.x) {
			transform.position += Vector3.right * Time.deltaTime;
		}
		//move to human
		transform.position = Vector3.MoveTowards (transform.position, get.position, movespeed * Time.deltaTime);
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.gameObject.tag == "Bomb" && other.GetComponent<BombCollider> ().dentonating) {
			Destroy (this.gameObject);
		}
	}
	IEnumerator death(){
		die.SetBool ("dead", true);
		splat.Play();
		yield return new WaitForSeconds (0.47f);
		Destroy (this.gameObject);
		
	}
}